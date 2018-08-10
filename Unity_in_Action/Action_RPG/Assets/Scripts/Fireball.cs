using UnityEngine;

public class Fireball : MonoBehaviour
{
	public float Speed = 10.0f;
	public int Damage = 1;

	void Update()
	{
		transform.Translate(0, 0, Speed * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		PlayerCharacter player = other.GetComponent<PlayerCharacter>();
		if (player != null)
		{
			player.Hurt(Damage);
		}

		Destroy(gameObject);
	}
}
