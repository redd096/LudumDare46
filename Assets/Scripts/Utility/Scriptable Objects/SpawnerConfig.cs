using UnityEngine;

namespace LudumDare46
{

    [CreateAssetMenu(menuName = "Ant Spawner Config")]
    public class SpawnerConfig : ScriptableObject
    {

        [SerializeField] private GameObject antPrefab = default;

        [SerializeField] private int numberOfAnts = 10;
        [SerializeField] private float timeBetweenSpawns = 0.5f;
        [SerializeField] private float spawnRandomFactor = 0.3f;
        [SerializeField] private float antSpeed = 10f;

        public int NumberOfAnts { get { return numberOfAnts; } }
        public float TimeBetweenSpawns { get { return timeBetweenSpawns; } }
        public float SpawnRandomFactor { get { return spawnRandomFactor; } }
        public float AntSpeed { get { return antSpeed; } }

        public GameObject AntPrefab { get { return antPrefab; } }

    }

}