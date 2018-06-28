using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

	[SerializeField] private MemoryCard _originalCard;
	[SerializeField] private Sprite[] _images;

	void Start()
	{
		int id = Random.Range(0, _images.Length);
		_originalCard.SetCard(id, _images[id]);
	}
}
