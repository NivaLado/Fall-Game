using UnityEngine;

/* The base item class. All items should derive from this. */
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject 
{
	new public string name = "New Item";	// Name of the item
	public Sprite icon = null;				// Item icon
	public Sprite buyIcon = null;				// BuyItem icon
	public GameObject prefab;
	public int skinIndex;
	public int price;

	// Called when the item is pressed in the inventory
	public virtual void Use ()
	{
		GameObject toDestroy = GameObject.FindGameObjectWithTag("Player");
		Destroy(toDestroy);
		Quaternion rotation = Quaternion.Euler(90, 0, 0);
		Instantiate(prefab, new Vector3(0,3.5f,0), rotation);
		PlayerPrefs.SetInt("Skin",skinIndex);		
	}

	// Called when the item is pressed in the inventory
	public virtual bool Buy ()
	{
		if(CoinsManager.instance.Coins >= price)
		{
			CoinsManager.instance.Withdraw(price);
			PlayerPrefs.SetInt(name, 1);	
			return true;
		}
		return false;
	}

	// Call this method to remove the item from inventory
	public void RemoveFromInventory ()
	{
		Inventory.instance.Remove(this);
	}

}
