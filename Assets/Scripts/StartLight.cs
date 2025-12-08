using System;
using UnityEngine;
public class StartLight : MonoBehaviour
{
	private void Start()
	{
		this.rend = base.GetComponent<MeshRenderer>();
		this.colors = this.rend.materials;
		this.SetColor(-1);
		base.Invoke("NextColor", GameController.Instance.startTime / 3f);
	}
	private void NextColor()
	{
		this.SetColor(this.c);
		if (this.audio)
		{
			this.audio.pitch = 1f + (float)this.c * 0.5f / 3f;
			this.audio.Play();
		}
		this.c++;
		if (this.c < 3)
		{
			base.Invoke("NextColor", GameController.Instance.startTime / 3f);
		}
	}
	private void SetColor(int c)
	{
		Material[] array = new Material[this.colors.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = this.colors[i];
		}
		for (int j = 0; j < array.Length; j++)
		{
			if (j == c + 1)
			{
				array[j] = this.colors[j];
			}
			else
			{
				array[j] = this.colors[0];
			}
		}
		this.rend.materials = array;
	}
	private MeshRenderer rend;
	public Material[] colors;
	public AudioSource audio;
	private int c;
}
