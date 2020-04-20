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
        AIDestinationSetter setter;
        // Start is called before the first frame update
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            setter = GetComponentInParent<AIDestinationSetter>();
        }

        // Update is called once per frame
        void Update()
        {
            if ((setter.target.position.x - transform.position.x) > Mathf.Epsilon)
            {
                spriteRenderer.flipX = true;
            }
            else if ((setter.target.position.x - transform.position.x) < -Mathf.Epsilon)
            {
                spriteRenderer.flipX  = false;
            }

        }
    }

}