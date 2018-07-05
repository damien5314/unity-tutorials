using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{

	public bool RequireKey = false;
	
	[SerializeField] private GameObject[] _targets;

	private void OnTriggerEnter(Collider other)
	{
		foreach (GameObject target in _targets)
		{
			if (RequireKey && GameManagers.Inventory.EquippedItem != "key")
			{
				return;
			}
			target.SendMessage("Activate");
		}
	}

	private void OnTriggerExit(Collider other)
	{
		foreach (GameObject target in _targets)
		{
			target.SendMessage("Deactivate");
		}
	}
}
