using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    [System.Serializable]
    public struct StaticStruct
    {
        [Range(0, 100)]
        public float percentage;
        public int numberLetters;
        public float timeKeepPressed;
    }

    [System.Serializable]
    public struct DinamicStruct
    {
        [Range(0, 100)]
        public float percentage;
        public int numberClicks;
        public float speed;
    }

    [System.Serializable]
    public struct AliveStruct
    {
        [Range(0, 100)]
        public float percentage;
        public int numberLetters;
        public float speed;
    }

    [CreateAssetMenu(menuName = "Trap Spawner Config")]
    public class TrapSpawnerConfig : ScriptableObject
    {
        [Header("Spawn Delay")]
        public float minDelayBetweenSpawns = 1;
        public float maxDelayBetweenSpawns = 2;


        [Header("General")]
        [Range(0, 100)]
        public float percentageStaticPrefab = default;
        public TrapMovement staticPrefab = default;
        [Range(0, 100)]
        public float percentageDinamicPrefab = default;
        public TrapMovement dinamicPrefab = default;
        [Range(0, 100)]
        public float percentageAlivePrefab = default;
        public TrapMovement alivePrefab = default;


        [Header("Statico")]
        public StaticStruct[] statico;

        [Header("Dinamico")]
        public DinamicStruct[] dinamico;

        [Header("Vivo")]
        public AliveStruct[] vivo;
    }
}