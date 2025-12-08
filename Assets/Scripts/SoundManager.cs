using System;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
	private void Awake()
	{
		if (SoundManager.Instance != null && SoundManager.Instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		SoundManager.Instance = this;
	}
	public void PlayCycle()
	{
		this.PlaySound(this.cycle);
	}
	public void PlayUnlock()
	{
		this.PlaySoundDelayed(this.unlock, 0.1f);
	}
	public void PlayError()
	{
		this.PlaySound(this.error);
	}
	public void PlayMoney()
	{
		this.PlaySound(this.buy);
	}
	public void PlayMenuNavigate()
	{
		this.PlaySound(this.menu);
	}
	public void PlaySound(AudioClip c)
	{
		this.audio.clip = c;
		this.audio.Play();
	}
	public void PlaySoundDelayed(AudioClip c, float d)
	{
		this.audio.clip = c;
		this.audio.PlayDelayed(d);
	}
	public static SoundManager Instance;
	public AudioClip cycle;
	public AudioClip menu;
	public AudioClip buy;
	public AudioClip unlock;
	public AudioClip error;
	public AudioSource audio;
}
