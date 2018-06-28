using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{

	[SerializeField] private GameObject _cardBack;
	
	private void OnMouseDown()
	{
		// What's the difference between active and activeSelf?
		if (_cardBack.activeSelf)
		{
			_cardBack.SetActive(false);
		}
	}
}
