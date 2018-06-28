using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryCard : MonoBehaviour
{

	[SerializeField] private GameObject _cardBack;
	[SerializeField] private SceneController _sceneController;
	private int _id;

	public int Id
	{
		get { return _id; }
		private set { _id = value; }
	}

	public void SetCard(int id, Sprite image)
	{
		Id = id;
		GetComponent<SpriteRenderer>().sprite = image;
	}

	private void OnMouseDown()
	{
		// What's the difference between active and activeSelf?
		if (_cardBack.activeSelf)
		{
			_cardBack.SetActive(false);
		}
	}
}
