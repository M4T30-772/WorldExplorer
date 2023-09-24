using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public Button HomeButton;
    public Button RetryButton;

    private void Start()
    {
        HomeButton.onClick.AddListener(LoadMainMenu);
        RetryButton.onClick.AddListener(LoadGameAgain);

        // Unlock the mouse cursor and make it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void LoadGameAgain()
    {
        SceneManager.LoadScene(PauseMenu.currentSceneName);
    }
}




