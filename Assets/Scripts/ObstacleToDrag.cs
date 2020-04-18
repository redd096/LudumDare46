using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleToDrag : Obstacle
{
    [SerializeField] float dragSpeed = 5;
    [SerializeField] float comeBackSpeed = 10;

    Vector3 startPosition;
    bool dragged;

    void Start()
    {
        //set start position
        startPosition = transform.position;
    }

    void Update()
    {
        if(dragged)
        {
            //when dragged - move to new position
            RaycastHit2D hit = InputPlayer.GetClickHit();

            if (hit)
                transform.position = Vector3.Lerp(transform.position, hit.transform.position, dragSpeed * Time.deltaTime);
        }
        else
        {
            //when released - come back to start position
            transform.position = Vector3.Lerp(transform.position, startPosition, comeBackSpeed * Time.deltaTime);
        }
    }

    public void Dragged()
    {
        dragged = true;
    }

    public void Released()
    {
        dragged = false;
    }
}
