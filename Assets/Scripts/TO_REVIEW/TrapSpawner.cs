﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    public class TrapSpawner : MonoBehaviour
    {
        [SerializeField] TrapSpawnerConfig trapSpawnerConfig;

        Coroutine spawn;

        Pooling staticoPool = new Pooling();
        Pooling dinamicoPool = new Pooling();
        Pooling vivoPool = new Pooling();

        IEnumerator Spawn()
        {
            //select prefab
            float randomPrefab = Random.Range(0f, 100f);
            if(randomPrefab <= trapSpawnerConfig.percentageStaticPrefab)
            {
                Statico();
            }
            else if(randomPrefab <= trapSpawnerConfig.percentageStaticPrefab + trapSpawnerConfig.percentageDinamicPrefab)
            {
                Dinamico();
            }
            else
            {
                Vivo();
            }

            //wait between spawns
            yield return new WaitForSeconds(Random.Range(trapSpawnerConfig.minDelayBetweenSpawns, trapSpawnerConfig.maxDelayBetweenSpawns));
            spawn = StartCoroutine(Spawn());
        }

        void Statico()
        {
            if (trapSpawnerConfig.staticPrefab == null)
            {
                Debug.LogWarning("manca static prefab");
                return;
            }

            int numberLetters = 0;
            float timeKeepPressed = 0;

            float randomLvl = Random.Range(0f, 100f);
            float lvlPercentage = 0;
            for(int i = 0; i < trapSpawnerConfig.statico.Length; i++)
            {
                lvlPercentage += trapSpawnerConfig.statico[i].percentage;

                if (randomLvl <= lvlPercentage)
                {
                    //set lvl
                    numberLetters = trapSpawnerConfig.statico[i].numberLetters;
                    timeKeepPressed = trapSpawnerConfig.statico[i].timeKeepPressed;
                    break;
                }
            }

            GameObject trapInstantiated = staticoPool.Instantiate(trapSpawnerConfig.staticPrefab.gameObject);
            trapInstantiated.GetComponent<TrapKeepPressed>().Set(numberLetters, timeKeepPressed);
        }

        void Dinamico()
        {
            if (trapSpawnerConfig.dinamicPrefab == null)
            {
                Debug.LogWarning("manca dinamic prefab");
                return;
            }

            int numberClicks = 0;
            float speed = 0;

            float randomLvl = Random.Range(0f, 100f);
            float lvlPercentage = 0;
            for (int i = 0; i < trapSpawnerConfig.dinamico.Length; i++)
            {
                lvlPercentage += trapSpawnerConfig.dinamico[i].percentage;

                if (randomLvl <= lvlPercentage)
                {
                    //set lvl
                    numberClicks = trapSpawnerConfig.dinamico[i].numberClicks;
                    speed = trapSpawnerConfig.dinamico[i].speed;
                    break;
                }
            }

            GameObject trapInstantiated = dinamicoPool.Instantiate(trapSpawnerConfig.dinamicPrefab.gameObject);
            trapInstantiated.GetComponent<TrapClick>().Set(numberClicks, speed);
        }

        void Vivo()
        {
            if (trapSpawnerConfig.alivePrefab == null)
            {
                Debug.LogWarning("manca vivo prefab");
                return;
            }

            int numberLetters = 0;
            float speed = 0;

            float randomLvl = Random.Range(0f, 100f);
            float lvlPercentage = 0;
            for (int i = 0; i < trapSpawnerConfig.vivo.Length; i++)
            {
                lvlPercentage += trapSpawnerConfig.vivo[i].percentage;

                if (randomLvl <= lvlPercentage)
                {
                    //set lvl
                    numberLetters = trapSpawnerConfig.vivo[i].numberLetters;
                    speed = trapSpawnerConfig.vivo[i].speed;
                    break;
                }
            }

            GameObject trapInstantiated = vivoPool.Instantiate(trapSpawnerConfig.alivePrefab.gameObject);
            trapInstantiated.GetComponent<TrapDigit>().Set(numberLetters, speed);
            
        }

        public void StartSpawning()
        {
            if (spawn != null)
                StopCoroutine(spawn);

            spawn = StartCoroutine(Spawn());
        }

        public void StopSpawning()
        {
            if (spawn != null)
                StopCoroutine(spawn);
        }
    }
}