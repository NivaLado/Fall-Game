using UnityEngine;

public class CoinCollect : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			CoinsManager.instance.Add(10);
			gameObject.SetActive(false);
		}
	}

}
