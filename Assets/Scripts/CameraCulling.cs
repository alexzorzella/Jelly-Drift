using System;
using UnityEngine;
public class CameraCulling : MonoBehaviour
{
	private void Awake()
	{
		CameraCulling.Instance = this;
		this.cam = base.GetComponent<Camera>();
		this.UpdateCulling();
	}
	public void UpdateCulling()
	{
		float[] array = new float[32];
		int quality = SaveState.Instance.quality;
		if (quality == 0)
		{
			array[12] = 120f;
		}
		else if (quality == 1)
		{
			array[12] = 300f;
		}
		else
		{
			array[12] = 1000f;
		}
		this.cam.layerCullDistances = array;
		this.cam.layerCullSpherical = true;
	}
	public static CameraCulling Instance;
	private Camera cam;
}
