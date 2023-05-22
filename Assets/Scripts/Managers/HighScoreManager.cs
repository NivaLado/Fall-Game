using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    #region Singleton
    public static HighScoreManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of HighScoreManager");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnPlayerDeath();
    public static event OnPlayerDeath gameOver;

    public TMP_Text scoreText;
    public TMP_Text deadText;

    public float duration = 0.2f;
    public int target;
    public int deadScore;

    //AudioManager audioManager;   //Singleton

    float score = 0;
    int rounded;
    public bool loose = false;
    string username = "Default";

    void Start()
    {
        //audioManager = AudioManager.instance;   // SoundManager Instace
    }

    void FixedUpdate()
    {
        if (deadScore <= 3)
        {
            deadText.text = deadScore.ToString();
            if (score != target)
            {
                score = Mathf.Lerp(score, target, duration);      //score++;
                rounded = (int)Mathf.Round(score);
                scoreText.text = rounded.ToString();
            }
        }
        else
        {
            if (!loose)
            {
                loose = true;
                if (gameOver != null)
                    gameOver();
            }
        }
    }

    public void StartOver()
    {
        HighScores.AddNewHighScore(username, (int)score);
        //audioManager.PlaySound("Fuck"); //Sound
    }

    public void Revive()
    {
        HighScores.AddNewHighScore(username, (int)score);
        //audioManager.PlaySound("Fuck"); //Sound
    }

    public void GetPlayerName(string _username)
    {
        username = _username;
    }
}

