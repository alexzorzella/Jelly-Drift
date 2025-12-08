using System;
using UnityEngine;
public class MusicController : MonoBehaviour
{
	private void Awake()
	{
		if (MusicController.Instance != null && MusicController.Instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			MusicController.Instance = this;
		}
		this.music = base.GetComponent<AudioSource>();
	}
	private void Start()
	{
		if (!MusicController.Instance)
		{
			return;
		}
		this.UpdateMusic((float)SaveState.Instance.music);
		AudioListener.volume = (float)SaveState.Instance.volume / 10f;
	}
	private void Update()
	{
	}
	public void UpdateMusic(float f)
	{
		this.music.volume = f / 10f;
	}
	private AudioSource music;
	public static MusicController Instance;
}
