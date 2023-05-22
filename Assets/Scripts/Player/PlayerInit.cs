using UnityEngine;

public class PlayerInit : MonoBehaviour {

	void Start () 
	{
		Quaternion rotation = Quaternion.Euler(90, 0, 0);
		if (PlayerPrefs.HasKey("Skin"))
			Instantiate(Inventory.instance.items[PlayerPrefs.GetInt("Skin")].prefab, new Vector3(0,3.5f,0), rotation);
		else
			Instantiate(Inventory.instance.items[0].prefab, new Vector3(0,3.5f,0), rotation);
	}
	
}
