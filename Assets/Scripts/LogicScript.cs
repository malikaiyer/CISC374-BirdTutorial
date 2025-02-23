using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class LogicScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int playerScore;
    public int highScore;
    public Text scoreText;
    public Text highScoreText;
    public GameObject gameOverScreen;
    public GameObject titleScreen;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        UpdateHighScoreUI();
    }

    [ContextMenu("AddScore")] 
    public void addScore(int scoreToAdd)
    {
        if (gameOverScreen.activeSelf)
        {
            return;
        }
        else
        {
            playerScore = playerScore + scoreToAdd;
            scoreText.text = playerScore.ToString();
        }

        if(playerScore > highScore)
        {
            highScore = playerScore;
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save(); //save the high score to the disk
        }
        UpdateHighScoreUI();
    }


    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void GameOver(){
        gameOverScreen.SetActive(true);
    }

    private void UpdateHighScoreUI()
    {
        if (highScoreText != null){
            highScoreText.text = "High Score: " + highScore.ToString();
        }
        
        
    }
}
