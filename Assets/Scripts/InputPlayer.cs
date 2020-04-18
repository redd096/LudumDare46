using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    [SerializeField] float damageOnClick = default;

    ObstacleToDrag draggedObject;

    void Start()
    {
        
    }

    void Update()
    {
        //check input pressed or released
        if (Input.GetKeyDown(KeyCode.Mouse0))
            PressedInput();
        else if (Input.GetKeyUp(KeyCode.Mouse0))
            ReleasedInput();
    }

    #region inputs

    void PressedInput()
    {
        RaycastHit2D hit = GetClickHit();

        //if hit something - check if ToKill or ToDrag
        if (hit)
        {
            ObstacleToKill obsToKill = hit.transform.GetComponentInParent<ObstacleToKill>();
            ObstacleToDrag obsToDrag = hit.transform.GetComponentInParent<ObstacleToDrag>();

            //try to kill or drag
            if (obsToKill)
            {
                KillObstacle(obsToKill);
            }
            else if (obsToDrag)
            {
                DragObstacle(obsToDrag);
            }
        }
    }

    void ReleasedInput()
    {
        RaycastHit2D hit = GetClickHit();

        //if hit something - check if obstacle
        if (hit)
        {
            Obstacle obsHitted = hit.transform.GetComponentInParent<Obstacle>();

            //try to drop on it
            if(obsHitted)
            {
                DropObstacle(obsHitted);
            }
        }
    }

    #endregion

    void KillObstacle(ObstacleToKill obsToKill)
    {
        //add damage
        obsToKill.GetDamage(damageOnClick);
    }

    void DragObstacle(ObstacleToDrag obsToDrag)
    {
        //reference to this
        draggedObject = obsToDrag;

        //drag
        obsToDrag.Dragged();
    }

    void DropObstacle(Obstacle obsHitted)
    {
        //try destroy hitted
        obsHitted.DestroyByDrag(draggedObject);

        //release
        draggedObject.Released();

        //remove reference
        draggedObject = null;
    }

    #region static

    public static RaycastHit2D GetClickHit()
    {
        Vector3 mousePosition = Input.mousePosition;
        return Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Camera.main.transform.forward);
    }

    #endregion
}
