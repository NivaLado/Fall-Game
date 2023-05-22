using UnityEngine;
using System.Collections;

public class FPS : MonoBehaviour
{
    public int fps;
    void Awake()
    {
        Time.timeScale = 0;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = fps;
    }

    void FixedUpdate() {
        if(Application.targetFrameRate != fps)
             Application.targetFrameRate = fps;
    }

    // string label = "";
	// float count;
	
	// IEnumerator Start ()
	// {
	// 	GUI.depth = 2;
	// 	while (true) {
	// 		if (Time.timeScale == 1) {
	// 			yield return new WaitForSeconds (0.1f);
	// 			count = (1 / Time.deltaTime);
	// 			label = "FPS :" + (Mathf.Round (count));
	// 		} else {
	// 			label = "Pause";
	// 		}
	// 		yield return new WaitForSeconds (0.5f);
	// 	}
	// }
	
    // private GUIStyle guiStyle = new GUIStyle();

	// void OnGUI ()
	// {
    //     guiStyle.fontSize = 50; //change the font size
	// 	GUI.Label (new Rect (75, 50, 100, 100), label,guiStyle);
	// }
}