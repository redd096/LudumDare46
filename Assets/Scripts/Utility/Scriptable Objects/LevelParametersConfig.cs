using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    [CreateAssetMenu(menuName = "Level Parameters")]
    public class LevelParametersConfig : ScriptableObject
    {
        [Header("Level timer in seconds")]
        [SerializeField] [Min(0f)] private float baseTime = 20f;
        [SerializeField] [Min(0f)] private float preparationTime = 5f;
        
        [Header("How many Ants to save")]
        [SerializeField] [Range(0, 100)] private float antsToSave = 50f;

        /// <summary>
        /// Are the anthills constantly spawning?
        /// </summary>
        [SerializeField] private bool infiniteSpawning = false;

        public float LevelTime { get { return baseTime; } }
        public float LevelPreparationTime { get { return preparationTime; } }

        public bool Loop { get { return infiniteSpawning; } }

    }

}