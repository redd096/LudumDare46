using System;
using UnityEngine;
using Pathfinding;

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
        private Transform target;

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
            aIDestinationSetter.target = target;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (LayerMask.LayerToName(collision.gameObject.layer) == "Hazards")
            {
                Destroy();
            }
        }

        public void Destroy()
        {
            //initial color and set active false instead of destroy
            //sprite.color = initialColor;
            gameObject.SetActive(false);
        }

        public void SetTarget(GameObject target)
        {
            this.target = target.transform;
        }

        public void SetSpeed(float antSpeed)
        {
            speed = antSpeed;
        }
    }
}