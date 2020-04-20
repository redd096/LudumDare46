using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    public Button resumeButton;
    public Button exitButton;

    void Start()
    {
        //add functions to buttons
        resumeButton.onClick.AddListener(Resume);
        exitButton.onClick.AddListener(MainMenu);

        Resume();
    }

    void Update()
    {
        //if press back button
        if (Input.GetKeyDown(KeyCode.Escape))
            PressedPause();
    }

    void PressedPause()
    {
        bool paused = menu.activeSelf;

        if(paused == false)
            Pause();
    }

    void Pause()
    {
        Time.timeScale = 0;
        menu.SetActive(true);
    }

    void Resume()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
    }

    void MainMenu()
    {
        Time.timeScale = 1;
        FindObjectOfType<ScreenLoader>().LoadMainMenu();
    }
}
