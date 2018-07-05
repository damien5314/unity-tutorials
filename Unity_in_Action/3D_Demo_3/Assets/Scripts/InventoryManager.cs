using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
	public ManagerStatus status { get; private set; }

	private List<string> _items;
	
	public void Startup()
	{
		Debug.Log("InventoryManager starting...");

		_items = new List<string>();
		
		status = ManagerStatus.Started;
	}

	private void DisplayItems()
	{
		StringBuilder itemDisplay = new StringBuilder("Items: ");

		foreach (string item in _items)
		{
			itemDisplay.Append(item + " ");
		}
		
		Debug.Log(itemDisplay);
	}

	public void AddItem(string name)
	{
		_items.Add(name);
		DisplayItems();
	}
}
