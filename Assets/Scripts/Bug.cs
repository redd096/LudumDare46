using UnityEngine;
using Pathfinding;
using System.Collections;

namespace LudumDare46
{
    [RequireComponent(typeof(AIPath), typeof(AIDestinationSetter))]
    public class Bug : MonoBehaviour
    {

        #region Properties
        public int BugId
        {
            get
            {
                return bugId;
            }
            set
            {
                if (bugId == int.MinValue && value != int.MinValue)
                {
                    bugId = value;
                }
                else
                {
                    Debug.LogError("Not allowed to change bugId");
                }
            }
        }

        public int MaterialId { get; private set; }

        #endregion

        #region Variables
        private int bugId = int.MinValue;
        private float speed;
        [SerializeField] private GameObject target;
        [SerializeField] private float pauseBetweenTargetHops;

        private AIPath aiPath;
        private AIDestinationSetter aIDestinationSetter;

        #endregion

        private void Awake()
        {
            //sprite = GetComponent<SpriteRenderer>();
            //initialColor = sprite.color;
            aiPath = GetComponent<AIPath>();
            aIDestinationSetter = GetComponent<AIDestinationSetter>();
        }

        private void Start()
        {
            aiPath.maxSpeed = speed;
            StartCoroutine(ChangeDestination());
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //if (LayerMask.LayerToName(collision.gameObject.layer) == "Hazards")
            //{
            TrapMovement trap = collision.gameObject.GetComponent<TrapMovement>();

            //check if is active
            if (trap && trap.isActive)
            {
                //Destroy();
                FindObjectOfType<GameManager>().OnAntKilled();
                Destroy(gameObject);
            }
            //}
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            //if (LayerMask.LayerToName(collision.gameObject.layer) == "Hazards")
            //{
            TrapMovement trap = collision.gameObject.GetComponent<TrapMovement>();

            //check if is active
            if (trap && trap.isActive)
            {
                //Destroy();
                FindObjectOfType<GameManager>().OnAntKilled();
                Destroy(gameObject);

            }
            //}
        }


        public void Destroy()
        {
            // set active false instead of destroy
            gameObject.SetActive(false);
        }

        public void SetTarget(Transform target)
        {
            this.target = target.gameObject;
            aiPath.SearchPath();
        }

        public void SetSpeed(float antSpeed)
        {
            speed = antSpeed;
        }

        private IEnumerator ChangeDestination()
        {
            while (true)
            {
                target.transform.position = Utils.GetRandomWalkableNode();
                aIDestinationSetter.target = target.transform;
                aiPath.SearchPath();
                yield return new WaitForSeconds(pauseBetweenTargetHops);
            }
        }

        private void OnEnable()
        {
            FindObjectOfType<GameManager>().OnAntSpawned();
        }

    }

}