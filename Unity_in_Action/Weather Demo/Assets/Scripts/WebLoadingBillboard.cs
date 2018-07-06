using UnityEngine;

public class WebLoadingBillboard : MonoBehaviour {

	public void Operate()
	{
		GameManagers.Images.GetWebImage(OnWebImageLoaded);
	}

	private void OnWebImageLoaded(Texture2D imageTexture)
	{
		GetComponent<Renderer>().material.mainTexture = imageTexture;
	}
}
