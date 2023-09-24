using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6;
    public float hInput;
    public static int kills = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector2.right * hInput * moveSpeed * Time.deltaTime );
        if(PauseMenu.currentSceneName == "GreeceLevel1") { if(kills == 4) { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; SceneManager.LoadScene("FactGrcka 1"); kills = 0; PlayerPrefs.SetInt("greecePlayerScore", 110); } }
        if(PauseMenu.currentSceneName == "GreeceLevel2") { if(kills == 8) { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; SceneManager.LoadScene("FactGrcka 2"); kills = 0; PlayerPrefs.SetInt("greecePlayerScore", 120); } }
        if(PauseMenu.currentSceneName == "GreeceLevel3") { if(kills == 8) { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; SceneManager.LoadScene("FactGrcka 3"); kills = 0; PlayerPrefs.SetInt("greecePlayerScore", 130); } }
        if(PauseMenu.currentSceneName == "GreeceLevel4") { if(kills == 8) { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; SceneManager.LoadScene("FactGrcka 4"); kills = 0; PlayerPrefs.SetInt("greecePlayerScore", 140); } }
    }
}

