using System;
using UnityEngine;
public class MoveImage : MonoBehaviour
{
	private void Update()
	{
		base.transform.localPosition += new Vector3(this.speed, 0f, 0f) * Time.deltaTime;
	}
	private float speed = 3f;
}
