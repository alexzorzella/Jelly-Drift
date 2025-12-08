using System;
using UnityEngine;
public class LowpasController : MonoBehaviour
{
	private void Awake()
	{
		this.lowpass = base.GetComponent<AudioLowPassFilter>();
		MonoBehaviour.print("got lowpass: " + this.lowpass);
	}
	private void Update()
	{
		if (Pause.Instance.paused)
		{
			this.desiredFreq = 200f;
		}
		else if (Time.timeScale < 1f)
		{
			this.desiredFreq = 500f;
		}
		else
		{
			this.desiredFreq = 22000f;
		}
		this.lowpass.cutoffFrequency = Mathf.Lerp(this.lowpass.cutoffFrequency, this.desiredFreq, Time.fixedDeltaTime * 4f);
	}
	private AudioLowPassFilter lowpass;
	private float desiredFreq;
}
