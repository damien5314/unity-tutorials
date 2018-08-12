using System.Runtime.InteropServices;
using UnityEngine;

public class TestPlugin : MonoBehaviour
{
	private static TestPlugin _instance;

	public static void Initialize()
	{
		if (_instance != null)
		{
			return;
		}

		Debug.Log("Initializing TestPlugin");

		GameObject owner = new GameObject("TestPlugin_Instance");
		_instance = owner.AddComponent<TestPlugin>();
		DontDestroyOnLoad(_instance);
	}

	#region iOS

	[DllImport("__Internal")]
	private static extern float _TestNumber();

	[DllImport("__Internal")]
	private static extern string _TestString(string test);

	#endregion iOS

	public static float TestNumber()
	{
		float val = 0f;
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			val = _TestNumber();
		}

		return val;
	}

	public static string TestString(string test)
	{
		string val = "";
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			val = _TestString(test);
		}

		return val;
	}
}
