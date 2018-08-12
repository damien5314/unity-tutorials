using System;
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

#if UNITY_ANDROID

	private static Exception _pluginError;
	private static AndroidJavaClass _pluginClass;

	private static AndroidJavaClass GetPluginClass()
	{
		if (_pluginClass == null && _pluginError == null)
		{
			AndroidJNI.AttachCurrentThread();
			try
			{
				_pluginClass = new AndroidJavaClass("ddiehl.uia.TestPlugin");
			}
			catch (Exception e)
			{
				_pluginError = e;
			}
		}

		return _pluginClass;
	}

	private static AndroidJavaObject _unityActivity;

	private static AndroidJavaObject GetUnityActivity()
	{
		if (_unityActivity == null)
		{
			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			_unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		}

		return _unityActivity;
	}

#endif

	public static float TestNumber()
	{
		float val = 0f;
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			val = _TestNumber();
		}

#if UNITY_ANDROID
		if (!Application.isEditor && _pluginError == null)
		{
			val = GetPluginClass().CallStatic<int>("getNumber");
		}
#endif
		return val;
	}

	public static string TestString(string test)
	{
		string val = "";
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			val = _TestString(test);
		}

#if UNITY_ANDROID
		if (!Application.isEditor && _pluginError == null)
		{
			val = GetPluginClass().CallStatic<string>("getString", test);
		}
#endif

		return val;
	}
}
