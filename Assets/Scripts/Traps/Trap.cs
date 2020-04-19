using System.Collections;
using System.Collections.Generic;
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

    public class Trap : MonoBehaviour
    {
        [Header("Trap Movement")]
        [SerializeField] TypeOfTrap typeOfTrap = default;

        [Header("Pericolo Vivo")]
        [SerializeField] TypeOfVivo typeOfVivo = default;

        [Header("Debug Movement")]
        [SerializeField] protected float speed = 1;
        [SerializeField] protected Vector3[] patrolMovements = default;
        [SerializeField] float approx = 0.1f;

        int patrolIndex;

        Transform target;

        void Start()
        {
            if (typeOfTrap == TypeOfTrap.dinamico && patrolMovements.Length < 2)
            {
                Debug.LogWarning("Ammo' che me li metti 2 punti dove andare?");
            }
        }

        protected virtual void Update()
        {
            if (typeOfTrap == TypeOfTrap.dinamico)
                Dinamico();
            else if (typeOfTrap == TypeOfTrap.vivo)
                Vivo();
        }

        protected virtual void OnMouseDown()
        {
            Debug.Log("clicked");
        }

        protected void Die()
        {
            gameObject.SetActive(false);
        }

        protected virtual void UpdateUI()
        {
            //mostra come disattivare la trappola
        }

        #region movements

        #region dinamico

        void Dinamico()
        {
            if (patrolMovements.Length < 1)
                return;

            Vector3 direction = (patrolMovements[patrolIndex] - transform.position).normalized;

            transform.position += direction * speed * Time.deltaTime;

            CheckReachedPatrolPoint();
        }

        void CheckReachedPatrolPoint()
        {
            //reached
            if(Vector3.Distance(transform.position, patrolMovements[patrolIndex]) <= speed * Time.deltaTime + approx)
            {
                patrolIndex++;

                //reset
                if (patrolIndex >= patrolMovements.Length)
                    patrolIndex = 0;
            }
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

            Vector3 direction = (target.position - transform.position).normalized;

            transform.position += direction * speed * Time.deltaTime;
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
            if(target == null)
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