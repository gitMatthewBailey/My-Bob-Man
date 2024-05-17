using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    //Class attributes/fields/variables or whatever you're meant to call them
    [SerializeField]
    [Tooltip("Link to the UI element for Score")]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    [Tooltip("Link to the UI element for High Score")]
    private TextMeshProUGUI highScoreText;
    private int score;
    private int highScore;

    void Start()
    {
        score = 0;
        //Sets the variable highScore to the value stored in the PlayerPrefs with the key "highScore". 
        //If the key does not exist, it defaults to 0.
        LoadHighScore();
        UpdateScoreText();
        UpdateHighScoreText();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
        CheckHighScore();
    }

    void UpdateScoreText()
    {
        //In C# - similar to in Java, ToString() is a method available for all data types, 
        //including integers like int. It allows you to convert the integer value to a string.
        //So, we make use of this within the UI to display the score and later in the code the High Score.
        scoreText.text = "Score: " + score.ToString();
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    void CheckHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
            UpdateHighScoreText();
        }
    }

    void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}
