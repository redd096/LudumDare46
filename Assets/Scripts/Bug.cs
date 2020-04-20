using UnityEngine;
using Pathfinding;
using System.Collections;

namespace LudumDare46
{
    [RequireComponent(typeof(AILerp), typeof(AIDestinationSetter))]
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

        private AILerp aILerp;
        private AIDestinationSetter aIDestinationSetter;

        #endregion

        private void Awake()
        {
            //sprite = GetComponent<SpriteRenderer>();
            //initialColor = sprite.color;
            aILerp = GetComponent<AILerp>();
            aIDestinationSetter = GetComponent<AIDestinationSetter>();
        }

        private void Start()
        {
            aILerp.speed = speed;
            StartCoroutine(ChangeDestination());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (LayerMask.LayerToName(collision.gameObject.layer) == "Hazards")
            {
                //Destroy();
                Destroy(gameObject);
                
            }
        }

        public void Destroy()
        {
            // set active false instead of destroy
            gameObject.SetActive(false);
        }

        public void SetTarget(Transform target)
        {
            this.target = target.gameObject;
            aILerp.SearchPath();
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
                aILerp.SearchPath();
                yield return new WaitForSeconds(pauseBetweenTargetHops);
            }
        }

        private void OnEnable()
        {
            FindObjectOfType<GameManager>().OnAntSpawned();
        }

        private void OnDestroy()
        {
            FindObjectOfType<GameManager>().OnAntKilled();
        }

    }

}