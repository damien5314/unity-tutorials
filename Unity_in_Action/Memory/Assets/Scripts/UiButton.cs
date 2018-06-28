using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButton : MonoBehaviour
{

	public Color HighlightColor = Color.cyan;
	
	[SerializeField] private GameObject _targetObject;
	[SerializeField] private string _targetMessage;

	private void OnMouseOver()
	{
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer != null)
		{
			spriteRenderer.color = HighlightColor;
		}
	}

	private void OnMouseExit()
	{
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer != null)
		{
			spriteRenderer.color = Color.white;
		}
	}

	private void OnMouseDown()
	{
		transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
	}

	private void OnMouseUp()
	{
		transform.localScale = Vector3.one;
		if (_targetObject != null)
		{
			_targetObject.SendMessage(_targetMessage);
		}
	}
}
