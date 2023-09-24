using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelIntro2 : MonoBehaviour
{
    public string LoadScene;

    // Start is called before the first frame update
    void Start()
    {
            int a = PlayerPrefs.GetInt("romeLoaded", 0);
            if(a < 2)
            { 
                SceneManager.LoadScene(LoadScene);
                PlayerPrefs.SetInt("romeLoaded", 2);
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                PlayerPrefs.SetInt("romeLoaded", 1);
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
