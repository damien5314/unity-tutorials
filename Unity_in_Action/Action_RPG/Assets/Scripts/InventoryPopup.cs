using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPopup : MonoBehaviour
{

	[SerializeField] private Image[] _itemIcons;
	[SerializeField] private Text[] _itemLabels;

	[SerializeField] private Text _currentItemLabel;
	[SerializeField] private Button _equipButton;
	[SerializeField] private Button _useButton;

	private string _currentItem;

	public void Refresh()
	{
		List<string> itemList = Managers.Inventory.GetItemList();

		for (int i = 0; i < _itemIcons.Length; i++)
		{
			if (i < itemList.Count)
			{
				_itemIcons[i].gameObject.SetActive(true);
				_itemLabels[i].gameObject.SetActive(true);

				string item = itemList[i];

				Sprite sprite = Resources.Load<Sprite>("Icons/" + item);
				_itemIcons[i].sprite = sprite;
				_itemIcons[i].SetNativeSize();

				int count = Managers.Inventory.GetItemCount(item);
				string message = "x" + count;
				if (item == Managers.Inventory.equippedItem)
				{
					message = "Equipped\n" + message;
				}

				_itemLabels[i].text = message;

				EventTrigger.Entry entry = new EventTrigger.Entry
				{
					eventID = EventTriggerType.PointerClick
				};
				entry.callback.AddListener(data => { OnItem(item); });

				EventTrigger trigger = _itemIcons[i].GetComponent<EventTrigger>();
				trigger.triggers.Clear();
				trigger.triggers.Add(entry);
			}
			else
			{
				_itemIcons[i].gameObject.SetActive(false);
				_itemLabels[i].gameObject.SetActive(false);
			}
		}

		if (!itemList.Contains(_currentItem))
		{
			_currentItem = null;
		}

		if (_currentItem == null)
		{
			_currentItemLabel.gameObject.SetActive(false);
			_equipButton.gameObject.SetActive(false);
			_useButton.gameObject.SetActive(false);
		}
		else
		{
			_currentItemLabel.gameObject.SetActive(true);
			_equipButton.gameObject.SetActive(true);
			_useButton.gameObject.SetActive(_currentItem == "health");
			_currentItemLabel.text = _currentItem + ":";
		}
	}

	public void OnItem(string item)
	{
		_currentItem = item;
		Refresh();
	}

	public void OnEquip()
	{
		Managers.Inventory.EquipItem(_currentItem);
		Refresh();
	}

	public void OnUse()
	{
		Managers.Inventory.ConsumeItem(_currentItem);
		if (_currentItem == "health")
		{
			Managers.Player.ChangeHealth(25);
		}
		
		Refresh();
	}
}
