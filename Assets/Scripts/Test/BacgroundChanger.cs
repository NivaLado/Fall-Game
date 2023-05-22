using UnityEngine;
using System.Collections.Generic;

public class BacgroundChanger : MonoBehaviour {

	public List<Sprite> images;
	InputControll instance;

	SpriteRenderer sprite;
	int i = 0;
	
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
		instance = InputControll.instance;
	}
	
	
	void Update () {
		if(instance.SwipeDown)
		{
			i++;
			i = Mathf.Clamp(i, 0 , images.Count-1);
			sprite.sprite = images[i];
		}
		if(instance.SwipeUp)
		{
			i--;
			i = Mathf.Clamp(i, 0 , images.Count-1);
			sprite.sprite = images[i];
		}
	}
}
