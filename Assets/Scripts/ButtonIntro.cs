using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonIntro : MonoBehaviour
{
    public Button nextButton;
    public string SceneName;
    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(LoadGameScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(SceneName);
        Time.timeScale = 1f;
    }
}
