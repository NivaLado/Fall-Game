using UnityEngine;
using System.Collections;

public class HighScores : MonoBehaviour
{
    const string PrivateCode = "BEWjDxCyM0yyxcbWEjPiMwgqKreVJ6skerNB8E4OlQSQ";
    const string PublicCode = "5ae19ba0191a840bcc961b74";
    const string webUrl = "http://dreamlo.com/lb/";

    public HighScore[] highscoresList;
	static HighScores instance;
    DisplayHighScores display;

    void Awake()
    {
		instance = this;
        display = GetComponent<DisplayHighScores>();
    }

    public static void AddNewHighScore(string username, int score)
    {
        instance.StartCoroutine(instance.UploadNewHighScore(username, score));
    }

    IEnumerator UploadNewHighScore(string username, int score)
    {
        WWW www = new WWW(webUrl + PrivateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
		{
			DownloadHighScores();
			//print("Upload Successful");
		}
        else
        {
            print("Error Uploading: " + www.error);
        }
    }

    public void DownloadHighScores()
    {
        StartCoroutine("DownloadHighscoresFromDB");
    }

    IEnumerator DownloadHighscoresFromDB()
    {
        WWW www = new WWW(webUrl + PublicCode + "/pipe/0/10");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighScores(www.text);
            display.OnHigscoresDownloaded(highscoresList);
        }
        else
        {
            print("Error Uploading: " + www.error);
        }
    }

    void FormatHighScores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        highscoresList = new HighScore[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);

            highscoresList[i] = new HighScore(username, score);
        }
    }
}

public struct HighScore
{
    public string username;
    public int score;

    public HighScore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }
}