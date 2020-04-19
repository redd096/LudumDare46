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

        [Header("Debug Movement")]
        [SerializeField] protected float speed = 1;
        [SerializeField] protected Vector3[] patrolMovements = default;
        [SerializeField] float approx = 0.1f;

        int patrolIndex;

        private void Start()
        {
            if (typeOfTrap == TypeOfTrap.dinamico && patrolMovements.Length < 2)
            {
                Debug.LogWarning("Ammo' che me li metti 2 punti dove andare?");
            }
        }

        protected virtual void Update()
        {
            if (typeOfTrap == TypeOfTrap.dinamico)
                Dinamico();
            else if (typeOfTrap == TypeOfTrap.vivo)
                Vivo();
        }

        void Dinamico()
        {
            if (patrolMovements.Length < 1)
                return;

            Vector3 direction = (patrolMovements[patrolIndex] - transform.position).normalized;

            transform.position += direction * speed * Time.deltaTime;

            CheckReachedPatrolPoint();
        }

        void CheckReachedPatrolPoint()
        {
            //reached
            if(Vector3.Distance(transform.position, patrolMovements[patrolIndex]) <= speed * Time.deltaTime + approx)
            {
                patrolIndex++;

                //reset
                if (patrolIndex >= patrolMovements.Length)
                    patrolIndex = 0;
            }
        }

        void Vivo()
        {
            //TODO trova la formica più vicina e inizia a inseguirla
            //una volta catturata cerca l'altra più vicina

            //OPPURE DEVE SEMPRE CERCARE QUELLA PIù VICINA? ANCHE SE HA GIà UN TARGET?

            Transform target = null;
            Vector3 direction = (target.position - transform.position).normalized;

            transform.position += direction * speed * Time.deltaTime;
        }
    }
}