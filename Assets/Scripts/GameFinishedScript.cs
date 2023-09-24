using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFinishedScript : MonoBehaviour
{
    public Button HomeButton;
    public Button NextLevelButton;

    private void Start()
    {
        HomeButton.onClick.AddListener(LoadMainMenu);
        NextLevelButton.onClick.AddListener(NextLevel);
        Time.timeScale = 0f;
        // Unlock the mouse cursor and make it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void NextLevel()
    {
        if(SceneLoader.LastLevelLoaded == "RomeLevel1") { SceneManager.LoadScene("RomeLevel2"); SceneLoader.LastLevelLoaded = "RomeLevel2"; }
        else if(SceneLoader.LastLevelLoaded == "RomeLevel2") { SceneManager.LoadScene("RomeLevel3"); SceneLoader.LastLevelLoaded = "RomeLevel3"; }
        else if(SceneLoader.LastLevelLoaded == "RomeLevel3") { SceneManager.LoadScene("RomeLevel4"); SceneLoader.LastLevelLoaded = "RomeLevel4"; }
        else if(SceneLoader.LastLevelLoaded == "RomeLevel4") { SceneManager.LoadScene("RomeLevel5"); SceneLoader.LastLevelLoaded = "RomeLevel5"; }
        else if(SceneLoader.LastLevelLoaded == "Labirint1") { SceneManager.LoadScene("Labirint2"); SceneLoader.LastLevelLoaded = "Labirint2"; }
        else if(SceneLoader.LastLevelLoaded == "Labirint2") { SceneManager.LoadScene("Labirint3"); SceneLoader.LastLevelLoaded = "Labirint3"; }
        else if(SceneLoader.LastLevelLoaded == "GreeceLevel1") { SceneManager.LoadScene("GreeceLevel2"); SceneLoader.LastLevelLoaded = "GreeceLevel2"; }
        else if(SceneLoader.LastLevelLoaded == "GreeceLevel2") { SceneManager.LoadScene("GreeceLevel3"); SceneLoader.LastLevelLoaded = "GreeceLevel3"; }
        else if(SceneLoader.LastLevelLoaded == "GreeceLevel3") { SceneManager.LoadScene("GreeceLevel4"); SceneLoader.LastLevelLoaded = "GreeceLevel4"; }
        else { }
        Time.timeScale = 1f;
    }
}




