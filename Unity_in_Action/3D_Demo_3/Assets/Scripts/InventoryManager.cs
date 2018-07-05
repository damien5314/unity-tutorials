using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
	public ManagerStatus status { get; private set; }

	public string EquippedItem { get; private set; }

	private Dictionary<string, int> _items;
	
	public void Startup()
	{
		Debug.Log("InventoryManager starting...");

		_items = new Dictionary<string, int>();
		
		status = ManagerStatus.Started;
	}

	private void DisplayItems()
	{
		StringBuilder itemDisplay = new StringBuilder("Items:\n");

		foreach (KeyValuePair<string, int> item in _items)
		{
			itemDisplay
				.Append(item.Key)
				.Append("(")
				.Append(item.Value)
				.Append(") ");
		}
		
		Debug.Log(itemDisplay);
	}

	public void AddItem(string name)
	{
		if (_items.ContainsKey(name))
		{
			_items[name] += 1;
		}
		else
		{
			_items[name] = 1;
		}

		DisplayItems();
	}

	public bool EquipItem(string name)
	{
		if (_items.ContainsKey(name) && EquippedItem != name)
		{
			EquippedItem = name;
			Debug.Log("Equipped: " + name);
			return true;
		}

		EquippedItem = null;
		Debug.Log("Unequipped: " + name);
		return false;
	}

	public List<string> GetItemList()
	{
		return _items.Keys.ToList();
	}

	public int GetItemCount(string name)
	{
		if (_items.ContainsKey(name))
		{
			return _items[name];
		}

		return 0;
	}
}
