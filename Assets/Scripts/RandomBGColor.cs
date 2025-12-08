using System;
using UnityEngine;
public class RandomBGColor : MonoBehaviour
{
	private void Awake()
	{
		this.camera = base.GetComponentInChildren<Camera>();
	}
	private void RandomColor()
	{
		Color backgroundColor = this.colors[UnityEngine.Random.Range(0, this.colors.Length)];
		this.camera.backgroundColor = backgroundColor;
	}
	private void OnEnable()
	{
		this.RandomColor();
	}
	private Camera camera;
	private Color[] colors = new Color[]
	{
		new Color(1f, 0.65f, 0.4f),
		new Color(1f, 0.4f, 0.41f),
		new Color(1f, 0.4f, 0.66f),
		new Color(0.95f, 0.48f, 1f),
		new Color(0.45f, 0.45f, 1f),
		new Color(0.316f, 0.7123f, 1f),
		new Color(0.35f, 1f, 0.48f)
	};
}
