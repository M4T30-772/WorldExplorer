using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadFinish : MonoBehaviour
{

    public Button goButton;

    private void Start()
    {
        goButton.onClick.AddListener(LoadSuccess);
    }

    public void LoadSuccess()
    {
        if(SceneLoader.LastLevelLoaded != "RomeLevel5" && SceneLoader.LastLevelLoaded != "GreeceLevel4" && SceneLoader.LastLevelLoaded != "Labirint3")
        {
            SceneManager.LoadScene("Successful");
        }
        else
        {
            if(SceneLoader.LastLevelLoaded == "RomeLevel5") { SceneManager.LoadScene("RomeFinish"); }
            else if(SceneLoader.LastLevelLoaded == "Labirint3") { SceneManager.LoadScene("EgyptFinish"); }
            else if(SceneLoader.LastLevelLoaded == "GreeceLevel4") { SceneManager.LoadScene("GreeceFinish"); }
            else { SceneManager.LoadScene("Successful"); }
        }
    }
}