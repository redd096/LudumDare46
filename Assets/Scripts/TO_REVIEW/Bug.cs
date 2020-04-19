using System;
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

        //[SerializeField] private Color targetColor;

        //// Cached Reference
        //private SpriteRenderer sprite;
        //private Color initialColor;

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
            //target.transform.position = Utils.GetRandomWalkableNode();
            //aIDestinationSetter.target = target.transform;
        }

        private void Update()
        {
            //        FlipSprite();
            //aILerp.
        }

        private void FlipSprite()
        {
            Vector2 direction = aILerp.destination - transform.position;
            Debug.Log(direction);
            Vector3 ls = transform.localScale;
            if (direction.y < Mathf.Epsilon)
            {
                transform.localScale = new Vector3(-ls.x, ls.y, ls.z);
            }

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
            //initial color and set active false instead of destroy
            //sprite.color = initialColor;
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

    }

}