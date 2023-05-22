using UnityEngine;
using System.Collections;
using TMPro;

public class DisplayHighScores : MonoBehaviour
{

    public TMP_Text[] names;
    public TMP_Text[] scores;

    HighScores realScores;

    void Start()
    {
        for (int i = 0; i < names.Length; i++)
        {
            names[i].text = "Fetching...";
            scores[i].text = "0";
        }

        realScores = GetComponent<HighScores>();

        //StartCoroutine("RefreshHighscores");
		realScores.DownloadHighScores();
    }

    public void OnHigscoresDownloaded(HighScore[] list)
    {
        for (int i = 0; i < names.Length; i++)
        {
            if (list.Length > i)
            {
                names[i].text = list[i].username;
                scores[i].text = list[i].score.ToString();
            } else {
				names[i].text = "Fetching...";
            	scores[i].text = "-1";
			}
        }
    }

    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            realScores.DownloadHighScores();
            yield return new WaitForSeconds(30);
        }
    }
}
