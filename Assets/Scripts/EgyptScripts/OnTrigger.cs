using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(PauseMenu.currentSceneName == "Labirint1") { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; SceneManager.LoadScene("FactEgipat 1"); PlayerPrefs.SetInt("egyptPlayerScore", 70); }
            if(PauseMenu.currentSceneName == "Labirint2") { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; SceneManager.LoadScene("FactEgipat 2"); PlayerPrefs.SetInt("egyptPlayerScore", 80); }
            if(PauseMenu.currentSceneName == "Labirint3") { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; SceneManager.LoadScene("FactEgipat 3"); PlayerPrefs.SetInt("egyptPlayerScore", 90); }
        }
    }
}