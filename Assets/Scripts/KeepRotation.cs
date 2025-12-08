using System;
using UnityEngine;
public class KeepRotation : MonoBehaviour
{
	private void Update()
	{
		Vector3 eulerAngles = base.transform.rotation.eulerAngles;
		eulerAngles.x = 0f;
		base.transform.rotation = Quaternion.Euler(eulerAngles);
	}
}
