using UnityEngine;

namespace LudumDare46
{
    [CreateAssetMenu(menuName = "Level Parameters")]
    public class LevelParametersConfig : ScriptableObject
    {
        [Header("Level timer in seconds")]
        [SerializeField] [Min(0f)] private float baseTime = 20f;
        [SerializeField] [Min(0f)] private float preparationTime = 5f;

        [Header("Ants Spawned in the game")]
        [SerializeField] [Min(0)] private int antsToSpawn = 500;

        [Header("Percentage of how many Ants to save")]
        [SerializeField] [Range(0, 100)] private float antsToSave = 50f;

        /// <summary>
        /// Are the anthills constantly spawning?
        /// </summary>
        [SerializeField] private bool infiniteSpawning = false;

        public float LevelTime { get { return baseTime; } }
       
        public float LevelPreparationTime { get { return preparationTime; } }

        public bool Loop { get { return infiniteSpawning; } }
        public int AntsToSpawn { get { return antsToSpawn; } }
        public float AntsToSave { get { return antsToSave; } }
    }

}