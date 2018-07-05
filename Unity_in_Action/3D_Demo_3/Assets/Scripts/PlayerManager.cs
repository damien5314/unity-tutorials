using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
	public ManagerStatus status { get; private set; }

	public int Health;
	public int MaxHealth;
	
	public void Startup()
	{
		Debug.Log("PlayerManager starting...");

		Health = 50;
		MaxHealth = 100;
		
		status = ManagerStatus.Started;
	}

	public void ChangeHealth(int value)
	{
		Health += value;

		if (Health > MaxHealth)
		{
			Health = MaxHealth;
		} 
		else if (Health < 0)
		{
			Health = 0;
		}
		
		Debug.Log(string.Format("Health: {0} / {1}", Health, MaxHealth));
	}
}
