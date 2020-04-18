using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleToKill : Obstacle
{
    [SerializeField] float health = default;

    public void GetDamage(float damage)
    {
        //get damage
        health -= damage;

        //check if dead
        if(health <= 0)
        {
            Die();
        }
    }    
}
