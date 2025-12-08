using System;
using UnityEngine;
public class RandomSfx : MonoBehaviour
{
	private void Awake()
	{
		this.s = base.GetComponent<AudioSource>();
		if (this.playOnAwake)
		{
			this.Randomize();
		}
	}
	public void Randomize()
	{
		this.s.clip = this.sounds[UnityEngine.Random.Range(0, this.sounds.Length - 1)];
		this.s.pitch = UnityEngine.Random.Range(this.minPitch, this.maxPitch);
		this.s.Play();
	}
	public AudioClip[] sounds;
	[Range(0f, 2f)]
	public float maxPitch = 0.8f;
	[Range(0f, 2f)]
	public float minPitch = 1.2f;
	private AudioSource s;
	public bool playOnAwake = true;
}
