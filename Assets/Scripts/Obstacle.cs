using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETypeOfObstacle
{
    nothing,
    water,
    fire,
    bomb,
}

public class Obstacle : MonoBehaviour
{
    [SerializeField] protected ETypeOfObstacle typeOfObstacle = default;
    [SerializeField] protected ETypeOfObstacle[] killedBy = default;

    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }

    public virtual void DestroyByDrag(ObstacleToDrag draggedObstacle)
    {
        //when something is dragged on this - check if this dragged object can kill 
        bool canKill = false;

        //check every element that can kill, if == dragged
        for(int i = 0; i < killedBy.Length; i++)
        {
            if(draggedObstacle.typeOfObstacle == killedBy[i])
            {
                canKill = true;
                break;
            }
        }

        //if can, kill this and dragged
        if (canKill)
        {
            Die();
            draggedObstacle.Die();
        }
    }
}
