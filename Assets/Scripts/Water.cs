using System;
using UnityEngine;
public class Water : MonoBehaviour
{
	private void Start()
	{
		if (SystemInfo.deviceType == DeviceType.Handheld)
		{
			base.GetComponent<MeshRenderer>().material = this.bad;
		}
	}
	public Material bad;
}
