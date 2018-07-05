using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
	public ManagerStatus status { get; private set; }
	
	public void Startup()
	{
		Debug.Log("InventoryManager started...");
		status = ManagerStatus.Started;
	}
}
