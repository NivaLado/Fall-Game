using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuController : MonoBehaviour
{

    #region Singleton
    public static MenuController instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of MenuController");
            return;
        }
        instance = this;
    }
    #endregion

    public static bool GameIsPaused =  false;
    public static bool GameIsStarted = false;

    public GameObject pauseMenuUi;
    public GameObject optionsUi;
    public GameObject mainMenuUi;
    public GameObject gameplayUi;
    public GameObject replayUi;
    public GameObject inventoryUi;
    public GameObject fade;

    void Start()
    {
        HighScoreManager.gameOver += payToWinOrDie;
    }

    void Update()
    {
        if ( GameIsStarted && Input.GetKeyDown(KeyCode.Escape) )
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        GameIsStarted = true;
        GameIsPaused = false;
        mainMenuUi.SetActive(false);
        gameplayUi.SetActive(true);
    }

    public void Resume()
    {
        fade.SetActive(false);
        pauseMenuUi.SetActive(false);
        optionsUi.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
        gameplayUi.SetActive(true);
    }

    void Pause()
    {
        fade.gameObject.SetActive(true);
        pauseMenuUi.SetActive(true);
        gameplayUi.SetActive(false);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void Options()
    {
        pauseMenuUi.SetActive(false);
        optionsUi.SetActive(true);
    }

    public void hideReplayUi()
    {
        replayUi.SetActive(false);
        fade.SetActive(false);
    }

    public void Inventory()
    {
        inventoryUi.SetActive(true);
        mainMenuUi.SetActive(false);
    }

    public void QuitFromOptions()
    {
        optionsUi.SetActive(false);

        if (GameIsPaused)
            pauseMenuUi.SetActive(true);
        else
            mainMenuUi.SetActive(true);
    }

    void payToWinOrDie()
    {
        GameIsStarted = false;
        RetryMenu();
    }

    public void RetryMenu()
    {
        Time.timeScale = 0;
        fade.gameObject.SetActive(true);
        replayUi.SetActive(true);
    }

    public void StartOver()
    {
        HighScoreManager.gameOver -= payToWinOrDie;
        HighScoreManager.instance.StartOver();
        PlayerDirectionChange.StartOver();
        SceneManager.LoadScene(0);
        // fade.SetActive(false);
        // replayUi.SetActive(false);
        // mainMenuUi.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}