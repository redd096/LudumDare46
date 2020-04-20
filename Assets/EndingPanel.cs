﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace LudumDare46
{

    public class EndingPanel : MonoBehaviour
    {
        [SerializeField] private Sprite[] outcomeSprites = default;
        [SerializeField] private Image outcomeText = default;
        [SerializeField] private Image[] stars = default;
        [SerializeField] private TextMeshProUGUI scoreText = default;
        [SerializeField] private Sprite[] starsSprites = default;

        [SerializeField] private int score = 10000000;
        [SerializeField] private int starCount = 3;

        // Start is called before the first frame update
        void Start()
        {
            scoreText.text = string.Format("{0:#,#}", score);
            foreach (var ir in stars)
            {
                ir.sprite = starsSprites[0];
            }
            if(starCount == 0)
            {
                outcomeText.sprite = outcomeSprites[0];
            }
            else
            {
                outcomeText.sprite = outcomeSprites[1];
            }

            switch (starCount)
            {
                case 1:
                {
                    stars[0].sprite = starsSprites[1];
                    break;
                }
                case 2:
                {
                    stars[0].sprite = starsSprites[1];
                    stars[1].sprite = starsSprites[1];
                    break;
                }
                case 3:
                {
                    foreach (var ir in stars)
                    {
                        ir.sprite = starsSprites[1];
                    }
                    break;
                }
            }

        }

    }

}