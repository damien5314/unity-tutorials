using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

	public const int GridRows = 2;
	public const int GridColumns = 4;
	public const float OffsetX = 2.0f;
	public const float OffsetY = 2.5f;

	[SerializeField] private MemoryCard _originalCard;
	[SerializeField] private Sprite[] _images;

	void Start()
	{
		// TODO: This offset code is kinda hacky, shouldn't we define the positions explicitly?
		Vector3 startPosition = _originalCard.transform.position;
		
		int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3};
		numbers = ShuffleArray(numbers);

		for (int i = 0; i < GridColumns; i++)
		{
			for (int j = 0; j < GridRows; j++)
			{
				MemoryCard card;
				if (i == 0 && j == 0)
				{
					card = _originalCard;
				}
				else
				{
					card = Instantiate(_originalCard);
				}

				int index = j * GridColumns + i;
				int id = numbers[index];
				_originalCard.SetCard(id, _images[id]);

				float posX = card.transform.position.x + (OffsetX * i);
				float posY = card.transform.position.y + -(OffsetY * j);
				card.transform.position = new Vector3(posX, posY, startPosition.z);
			}
		}
	}

	private int[] ShuffleArray(int[] array)
	{
		int[] shuffled = array.Clone() as int[];
		for (int i = 0; i < array.Length; i++)
		{
			int tmp = shuffled[i];
			int r = Random.Range(i, shuffled.Length);
			shuffled[i] = shuffled[r];
			shuffled[r] = tmp;
		}

		return shuffled;
	}
}
