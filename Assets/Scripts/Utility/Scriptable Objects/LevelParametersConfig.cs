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

        [Header("Stars Conditions")]
        [SerializeField] [Range(0f, 1f)] private float antsToSave = .5f;
        [SerializeField] [Range(0f, 1f)] private float antsForSecondStar = 0.6f;
        [SerializeField] [Min(0)] private int minimumTrapsForThirdStar = 10;

        /// <summary>
        /// Are the anthills constantly spawning?
        /// </summary>
        [SerializeField] private bool infiniteSpawning = false;

        public float LevelTime { get { return baseTime; } }
       
        public float LevelPreparationTime { get { return preparationTime; } }

        public bool Loop { get { return infiniteSpawning; } }
        public int AntsToSpawn { get { return antsToSpawn; } }
        public float AntsToSave { get { return antsToSave; } }
        public float AntsForSecondStar { get { return antsForSecondStar; } }
        public int MinimumTrapsForThirdStar {  get { return minimumTrapsForThirdStar; } }
    }

}