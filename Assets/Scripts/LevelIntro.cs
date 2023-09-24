using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelIntro : MonoBehaviour
{
    public string LoadScene;

    // Start is called before the first frame update
    void Start()
    {
            int a = PlayerPrefs.GetInt("greeceLoaded", 0);
            if(a < 2)
            { 
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene(LoadScene);
                PlayerPrefs.SetInt("greeceLoaded", 2);
            }
            else
            {
                PlayerPrefs.SetInt("greeceLoaded", 1);
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
