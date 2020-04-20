using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace LudumDare46
{
    public enum TypeOfTrap
    {
        statico,
        dinamico,
        vivo,
    }

    public enum TypeOfVivo
    {
        findAlwaysNearest,
        findNearestAndKill,
        findAlwaysRandom,
        findRandomAndKill,
    }

    [RequireComponent(typeof(AIPath), typeof(AIDestinationSetter))]
    public class TrapMovement : MonoBehaviour
    {
        [Header("Trap Movement")]
        [SerializeField] TypeOfTrap typeOfTrap = default;

        [Header("Pericolo Vivo")]
        [SerializeField] TypeOfVivo typeOfVivo = default;

        [Header("Debug Movement")]
        [SerializeField] protected float speed = 1;
        [SerializeField] protected GameObject[] patrolMovements = default;

        int patrolIndex;

        Coroutine patrolCoroutine;

        Transform target;

        private AILerp aILerp;
        private AIDestinationSetter aIDestinationSetter;

        private void Awake()
        {
            aILerp = GetComponent<AILerp>();
            aIDestinationSetter = GetComponent<AIDestinationSetter>();
        }

        private void OnEnable()
        {
            if (typeOfTrap == TypeOfTrap.dinamico)
            {
                //two random points to move
                patrolMovements[0] = new GameObject();
                patrolMovements[0].transform.position = Utils.GetRandomWalkableNode();
                patrolMovements[1] = new GameObject();
                patrolMovements[1].transform.position = this.transform.position; // la seconda posizione sarà sempre dove sono spawnato
                patrolIndex = 0;
                patrolCoroutine = null;
            }
        }

        void Start()
        {
            if (typeOfTrap == TypeOfTrap.dinamico)
            {
                if (patrolMovements.Length < 2)
                {
                    Debug.LogWarning("Ammo' che me li metti 2 punti dove andare?");
                }
                else
                {
                    patrolMovements[1].transform.position = this.transform.position;
                }
            }
        }

        protected virtual void Update()
        {
            if (typeOfTrap == TypeOfTrap.dinamico)
                Dinamico();
            else if (typeOfTrap == TypeOfTrap.vivo)
                Vivo();
        }

        public void Set(float speed)
        {
            this.speed = speed;
        }


        protected virtual void UpdateUI()
        {
            //mostra come disattivare la trappola
        }

        #region movements

        #region dinamico

        void Dinamico()
        {
            if (patrolCoroutine != null) { return; }

            if (patrolMovements.Length < 1)
                return;

            aIDestinationSetter.target = patrolMovements[patrolIndex].transform;
            aILerp.SearchPath();
            aILerp.speed = speed;

            patrolCoroutine = StartCoroutine(CheckReachedPatrolPoint());
        }

        IEnumerator CheckReachedPatrolPoint()
        {

            // sostituire con le info dal Path.
            //reached
            while (aILerp.pathPending || !aILerp.reachedEndOfPath)
            {
                yield return null;
            }

            patrolIndex++;

            //reset
            if (patrolIndex >= patrolMovements.Length)
                patrolIndex = 0;

            patrolCoroutine = null;
        }

        #endregion

        #region vivo

        void Vivo()
        {
            CheckValid();

            switch (typeOfVivo)
            {
                case TypeOfVivo.findAlwaysNearest:
                    FindAlwaysNearest();
                    break;
                case TypeOfVivo.findNearestAndKill:
                    FindNearestAndKill();
                    break;
                case TypeOfVivo.findAlwaysRandom:
                    FindAlwaysRandom();
                    break;
                case TypeOfVivo.findRandomAndKill:
                    FindRandomAndKill();
                    break;
                default:
                    Debug.LogError("Se sei arrivato qui qualche divinità canina ti odia");
                    break;
            }

            //move only if there is a target
            if (target == null) return;

            // Sostituire il movimento verso il target con questa operazione:
            aIDestinationSetter.target = target.transform;
            aILerp.SearchPath();
            aILerp.speed = speed;

            //Vector3 direction = (target.position - transform.position).normalized;

            //transform.position += direction * speed * Time.deltaTime; // va sostituita andando a cambiare il parametro Speed del componente AILERP
        }

        void CheckValid()
        {
            //set null if not active
            if (target != null && target.gameObject.activeSelf == false)
                target = null;
        }

        #region get target

        void FindAlwaysNearest()
        {
            Bug[] listOfBugs = FindActivesObjectsOfType<Bug>();
            target = FindNearest(listOfBugs);
        }

        void FindNearestAndKill()
        {
            if (target == null)
            {
                FindAlwaysNearest();
            }
        }

        void FindAlwaysRandom()
        {
            Bug[] listOfBugs = FindActivesObjectsOfType<Bug>();
            target = listOfBugs[Random.Range(0, listOfBugs.Length)].transform;
        }

        void FindRandomAndKill()
        {
            if (target == null)
            {
                FindAlwaysRandom();
            }
        }

        #endregion

        T[] FindActivesObjectsOfType<T>() where T : Component
        {
            List<T> listActives = new List<T>();

            //find all
            T[] listObjects = FindObjectsOfType<T>();

            //add only actives
            foreach (T child in listObjects)
            {
                if (child.gameObject.activeInHierarchy)
                    listActives.Add(child);
            }

            return listActives.ToArray();
        }

        Transform FindNearest<T>(T[] listObjects) where T : Component
        {
            Transform nearestObject = null;
            float distance = Mathf.Infinity;

            foreach (T component in listObjects)
            {
                //maybe another turret destroyed enemy while we are trying to get it
                if (component == null)
                    continue;

                float newDistance = Vector3.Distance(component.transform.position, transform.position);

                //if nearest then last one, then set new nearest enemy
                if (newDistance <= distance)
                {
                    distance = newDistance;
                    nearestObject = component.transform;
                }
            }

            return nearestObject;
        }

        #endregion

        #endregion
    }
}