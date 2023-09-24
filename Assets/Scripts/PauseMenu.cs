using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameHud;

    public GameObject infoCanvas;
    public float displayDuration = 6f;

    public static bool isPaused;

    public static string currentSceneName = "";

    void Start()
    {
        pauseMenu.SetActive(false);
        ShowInfoCanvas();
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        // Unlock the mouse cursor and make it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        pauseMenu.SetActive(true);
        gameHud.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        // Hide the mouse cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pauseMenu.SetActive(false);
        gameHud.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowInfoCanvas()
    {
        // Hide the mouse cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        infoCanvas.SetActive(true);

        StartCoroutine(HideInfoCanvasAfterDelay());
    }

    private IEnumerator HideInfoCanvasAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);

        infoCanvas.SetActive(false);
    }
}
