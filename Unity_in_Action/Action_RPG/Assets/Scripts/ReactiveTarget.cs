using System.Collections;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
	public void ReactToHit()
	{
		WanderingAi behavior = GetComponent<WanderingAi>();
		if (behavior != null)
		{
			behavior.SetAlive(false);
		}

		StartCoroutine(Die());
	}

	private IEnumerator Die()
	{
		transform.Rotate(-75, 0, 0);

		yield return new WaitForSeconds(1.5f);

		Destroy(gameObject);
	}
}
