using UnityEngine;
using UnityEngine.Advertisements;

public class PlayAd : MonoBehaviour
{

    public void ShowAd()
    {
        if (Advertisement.isInitialized)
        {
            MenuController.GameIsStarted = false;
            Advertisement.Show("video", new ShowOptions());
        }
    }

    void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
				WatchedAd();
                Debug.Log("Finished");
                break;
            case ShowResult.Skipped:
                MenuController.instance.RetryMenu();
                Debug.Log("Skipped");
                break;
            case ShowResult.Failed:
                MenuController.instance.RetryMenu();
                Debug.Log("Failed");
                break;
        }
        MenuController.GameIsStarted = true;
    }

    void WatchedAd()
    {
		//Handle Scores
        HighScoreManager.instance.deadScore = 0;
        HighScoreManager.instance.loose = false;
        //Hide Menu
        MenuController.instance.hideReplayUi();
        //Reset Position
        PlayerDirectionChange.rb.velocity = Vector3.zero;
        PlayerDirectionChange.rb.position = new Vector3(0, PlayerDirectionChange.rb.position.y + 2f, PlayerDirectionChange.rb.position.z);
        //Back Time
        Time.timeScale = 1;
    }

}
