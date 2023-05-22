using UnityEngine;
using TMPro;

public class CoinsManager : MonoBehaviour
{

    #region Singleton
    public static CoinsManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of HighScoreManager");
            return;
        }
        instance = this;
        if(PlayerPrefs.HasKey("Coins"))
            Coins = PlayerPrefs.GetInt("Coins", Coins);
        else 
            Coins = 0;
    }
    #endregion

    void Start()
    {
        coinsText.text = Coins.ToString();
    }

    int coins;
    public TMP_Text coinsText;

    public int Coins
    {
        get { return coins; }
        set { coins = value; }
    }

    public void Check(bool a, bool b)
    {   
        if(a)
        coinsText.text += " R11G11B10 ";
        if(b)
        coinsText.text += " FP16 ";
    }

    public void Add(int ammout)
    {
        Coins += ammout;
        coinsText.text = Coins.ToString();
        // if(HighScoreManager.instance.deadScore > 0)
        // HighScoreManager.instance.deadScore -= 1;
        PlayerPrefs.SetInt("Coins", Coins);
    }

    public void Withdraw(int ammout)
    {
        Coins -= ammout;
        coinsText.text = Coins.ToString();
        PlayerPrefs.SetInt("Coins", Coins);
    }

}
