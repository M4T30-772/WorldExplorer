using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private TextMeshProUGUI lockedText;
    [SerializeField] private Button levelButton;
    [SerializeField] public Sprite goldStarSprite;
    public static string LastLevelLoaded = "";

    
    public void Start()
    {
        int requiredScore = GetRequiredScoreForScene(sceneToLoad);
        int romePlayerScore = PlayerPrefs.GetInt("romePlayerScore", 0);
        int egyptPlayerScore = PlayerPrefs.GetInt("egyptPlayerScore", 60);
        int greecePlayerScore = PlayerPrefs.GetInt("greecePlayerScore", 100);
        
/* RESET GAME AND PLAYER STATS!
        PlayerPrefs.SetInt("romePlayerScore", 0);
        PlayerPrefs.SetInt("egyptPlayerScore", 60);
        PlayerPrefs.SetInt("greecePlayerScore", 100);
*/

        //ROME
        if(requiredScore == 0 && romePlayerScore > 0) //RomeLevel1
        {
            ChangeSprite();
        }
        if(requiredScore == 10 && romePlayerScore > 10) //RomeLevel2
        {
            ChangeSprite();
        }
        if(requiredScore == 20 && romePlayerScore > 20) //RomeLevel3
        {
            ChangeSprite();
        }
        if(requiredScore == 30 && romePlayerScore > 30) //RomeLevel4
        {
            ChangeSprite();
        }
        if(requiredScore == 40 && romePlayerScore > 40) //RomeLevel5
        {
            ChangeSprite();
        }
        //EGYPT
        if(requiredScore == 60 && egyptPlayerScore > 60) //Labirint1
        {
            ChangeSprite();
        }
        if(requiredScore == 70 && egyptPlayerScore > 70) //Labirint2
        {
            ChangeSprite();
        }
        if(requiredScore == 80 && egyptPlayerScore > 80) //Labirint3
        {
            ChangeSprite();
        }
        //GREECE
        if(requiredScore == 100 && greecePlayerScore > 100) //GreeceLevel1
        {
            ChangeSprite();
        }
        if(requiredScore == 110 && greecePlayerScore > 110) //GreeceLevel2
        {
            ChangeSprite();
        }
        if(requiredScore == 120 && greecePlayerScore > 120) //GreeceLevel3
        {
            ChangeSprite();
        }
        if(requiredScore == 130 && greecePlayerScore > 130) //GreeceLevel4
        {
            ChangeSprite();
        }
    }

    public void ChangeSprite()
    {
        if (goldStarSprite != null)
        {
            Image imageComponent = levelButton.GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.sprite = goldStarSprite;
            }
        }
    }

    public void LoadScene()
    {
        int requiredScore = GetRequiredScoreForScene(sceneToLoad);

        int romePlayerScore = PlayerPrefs.GetInt("romePlayerScore", 0);
        int egyptPlayerScore = PlayerPrefs.GetInt("egyptPlayerScore", 60);
        int greecePlayerScore = PlayerPrefs.GetInt("greecePlayerScore", 100);

        if (requiredScore < 60 && romePlayerScore >= requiredScore)
        {
            SceneManager.LoadScene(sceneToLoad);
            LastLevelLoaded = sceneToLoad;
        }
        else if (requiredScore >= 60 && requiredScore < 110 && egyptPlayerScore >= requiredScore)
        {
            SceneManager.LoadScene(sceneToLoad);
            LastLevelLoaded = sceneToLoad;
        }
        else if (requiredScore >= 100 && greecePlayerScore >= requiredScore)
        {
            SceneManager.LoadScene(sceneToLoad);
            LastLevelLoaded = sceneToLoad;
        }
        else
        {
            lockedText.gameObject.SetActive(true);
            lockedText.text = "Level zaključan!";
            StartCoroutine(RemoveTextAfterDelay(3f));
        }
    }

    private IEnumerator RemoveTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        lockedText.gameObject.SetActive(false);
    }

    private int GetRequiredScoreForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "RomeLevel1":
                return 0;
            case "RomeLevel2":
                return 10;
            case "RomeLevel3":
                return 20;
            case "RomeLevel4":
                return 30;
            case "RomeLevel5":
                return 40;
            case "Labirint1":
                return 60;
            case "Labirint2":
                return 70;
            case "Labirint3":
                return 80;
            case "GreeceLevel1":
                return 100;
            case "GreeceLevel2":
                return 110;
            case "GreeceLevel3":
                return 120;
            case "GreeceLevel4":
                return 130;
            default:
                return 0;
        }
    }
}