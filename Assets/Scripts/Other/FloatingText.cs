using UnityEngine;

public class FloatingText : MonoBehaviour {

	void OnEnable() {
		  Invoke("disableText", 1);
	}

	void disableText()
	{
		gameObject.SetActive(false);
	}

}
