using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace LudumDare46
{

    [RequireComponent(typeof(SpriteRenderer))]
    public class FlipGraphics : MonoBehaviour
    {

        SpriteRenderer spriteRenderer;
        AIPath aiPath;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            aiPath = GetComponentInParent<AIPath>();
        }

        void Update()
        {                        
            float speed = aiPath.velocity.x;

            if (speed > 0.5f)
            {
                spriteRenderer.flipX = true;
            }
            else if (speed < -0.5f)
            {
                spriteRenderer.flipX  = false;
            }

        }
    }

}