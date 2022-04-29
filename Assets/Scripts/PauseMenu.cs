using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    bool isPaused = false;
    public TheGame game;
    void Start()
    {
        game = GameObject.Find("GameStuff").GetComponent<TheGame>();
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && !game.gameOver)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            //unpause
            Time.timeScale = 1.0f;
            pauseUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            //pause
            Time.timeScale = 0.0f;
            pauseUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        isPaused = !isPaused;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
