using System.Collections.Generic;
using UnityEngine;

public class BasicGui : MonoBehaviour {
	
	private void OnGUI()
	{
		int positionX = 10;
		int positionY = 10;
		int width = 100;
		int height = 30;
		int buffer = 10;

		List<string> itemList = GameManagers.Inventory.GetItemList();
		if (itemList.Count == 0)
		{
			GUI.Box(new Rect(positionX, positionY, width, height), "No items");
		}

		foreach (string item in itemList)
		{
			int count = GameManagers.Inventory.GetItemCount(item);
			Texture2D image = Resources.Load<Texture2D>("Icons/" + item);
			GUIContent guiContent = new GUIContent(string.Format("({0})", count), image);
			GUI.Box(new Rect(positionX, positionY, width, height), guiContent);
			positionX += width + buffer;
		}

		string equipped = GameManagers.Inventory.EquippedItem;
		if (equipped != null)
		{
			positionX = Screen.width - (width + buffer);
			Texture2D image = Resources.Load("Icons/" + equipped) as Texture2D;
			GUIContent content = new GUIContent("Equipped", image);
			GUI.Box(new Rect(positionX, positionY, width, height), content);
		}

		positionX = 10;
		positionY += height + buffer;

		foreach (string item in itemList)
		{
			if (GUI.Button(new Rect(positionX, positionY, width, height), "Equip " + item))
			{
				GameManagers.Inventory.EquipItem(item);
			}

			positionX += width + buffer;
		}
	}
}
