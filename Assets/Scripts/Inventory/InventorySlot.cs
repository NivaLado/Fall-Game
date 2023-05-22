using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.Events;
// using UnityEngine.EventSystems;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour {

	public Image icon;

	public Image buyIcon;
	public GameObject buyButton;

	Item item;	// Current item in the slot

	// Add item to the slot
	public void AddItem (Item newItem)
	{
		//PlayerPrefs.DeleteAll(); //FOR DEBUG PURPOSES
		item = newItem;
		int buyed = PlayerPrefs.GetInt(item.name, 0);

		icon.sprite = item.icon;
		buyIcon.sprite = item.buyIcon;

		if (buyed == 1)
		{
			icon.enabled = true;
		}
		else
		{
			buyButton.SetActive(true);
		}
	}

	// Clear the slot
	public void ClearSlot ()
	{
		item = null;

		icon.sprite = null;
		icon.enabled = false;
	}


	// Use the item
	public void UseItem ()
	{
		if (item != null)
		{
			item.Use();
		}
	}

	// Buy the item
	public void BuyItem ()
	{
		if (item != null)
		{
			if(item.Buy())
			{
				buyButton.SetActive(false);
				icon.enabled = true;
			}
		}
	}

}
