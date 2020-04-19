using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    public enum TypeOfTrap
    {
        statico,
        dinamico,
        vivo,
    }

    public class Trap : MonoBehaviour
    {
        [Header("Trap Movement")]
        [SerializeField] TypeOfTrap typeOfTrap = default;
        [SerializeField] float speed = 1;

        [Header("Dinamico")]
        [SerializeField] Transform[] patrolMovements = default;
        [SerializeField] float approx = 0.1f;

        int patrolIndex;

        protected virtual void Update()
        {
            if (typeOfTrap == TypeOfTrap.dinamico)
                Dinamico();
            else if (typeOfTrap == TypeOfTrap.vivo)
                Vivo();
        }

        void OnTriggerEnter(Collider other)
        {
            
        }

        void Dinamico()
        {
            Vector3 direction = (patrolMovements[patrolIndex].position - transform.position).normalized;

            transform.position += direction * speed * Time.deltaTime;

            CheckReachedPatrolPoint();
        }

        void CheckReachedPatrolPoint()
        {
            //reached
            if(Vector3.Distance(transform.position, patrolMovements[patrolIndex].position) <= speed * Time.deltaTime + approx)
            {
                patrolIndex++;

                //reset
                if (patrolIndex >= patrolMovements.Length)
                    patrolIndex = 0;
            }
        }

        void Vivo()
        {
            //trova la formica più vicina e inizia a inseguirla
            //una volta catturata cerca l'altra più vicina

            //OPPURE DEVE SEMPRE CERCARE QUELLA PIù VICINA? ANCHE SE HA GIà UN TARGET?

            Transform target = null;
            Vector3 direction = (target.position - transform.position).normalized;

            transform.position += direction * speed * Time.deltaTime;
        }
    }
}