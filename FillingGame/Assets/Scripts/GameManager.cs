using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Current;
    
    [SerializeField] GameObject finishPanel;
    public GameObject startMenu, gameMenu, gameOverMenu , finishMenu;
    public TextMeshProUGUI scoreText, finishScoreText;
    public bool gameActive = false;
    private int score = 0;



    [Header("Slider")]
    public GameObject finishLine;
    public float maxDistance;
    public Slider levelProgressBar;
    private Pistol pistol;
    public TextMeshProUGUI currentLevelText, nextLevelText;
    
    public int currentLevel = 0;
    private int currentLevell = 0;



    private void Awake()
    {
        pistol = GameObject.Find("Pistol").GetComponent<Pistol>();
    }
    void Start()
    {
        Current = this;
        gameActive = false;
        Time.timeScale = 1;
        
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        currentLevell = PlayerPrefs.GetInt("currentLevell");

        if (SceneManager.GetActiveScene().name != "Level " + currentLevel)
        {


            SceneManager.LoadScene("Level " + currentLevel);

        }
        else
        {
            currentLevelText.text = (currentLevell + 1).ToString();
            nextLevelText.text = (currentLevell + 2).ToString();
        }
    }

    void Update()
    {
        if (gameActive)
        {
            float distance = finishLine.transform.position.x - pistol.transform.position.x;
            levelProgressBar.value = 1 - (distance / maxDistance);

        }
    }

    public void StartLevel()
    {
        maxDistance = finishLine.transform.position.x - pistol.transform.position.x;
        startMenu.SetActive(false);
        gameMenu.SetActive(true);
        gameActive = true;

    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        

    }


    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level " + (currentLevel + 1));

        if (currentLevel == 2)
        {

            PlayerPrefs.SetInt("currentLevel", currentLevel - 2);
            SceneManager.LoadScene("Level 0");
        }
    }

    public void GameOver()
    {
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        gameActive = false;
    }

    public void FinishGame()
    {
        PlayerPrefs.SetInt("currentLevel", currentLevel + 1);
        PlayerPrefs.SetInt("currentLevell", currentLevell + 1);
        finishScoreText.text = score.ToString();
        gameMenu.SetActive(false);
        finishMenu.SetActive(true);
        gameActive = false;
    }

    public void changescore(int increment)
    {
        score += increment;
        scoreText.text = score.ToString();

    }
    public void multiplicationcore(int increment)
    {
        score *= increment;
        scoreText.text = score.ToString();

    }


}
