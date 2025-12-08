using System;
using UnityEngine;
public class MenuSounds : MonoBehaviour
{
	public void Play()
	{
		SoundManager.Instance.PlayMenuNavigate();
	}
}
