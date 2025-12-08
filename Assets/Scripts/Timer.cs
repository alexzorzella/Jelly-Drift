using System;
using TMPro;
using UnityEngine;
public class Timer : MonoBehaviour
{
	public static Timer Instance { get; set; }
	private void Awake()
	{
		Timer.Instance = this;
		this.text = base.GetComponent<TextMeshProUGUI>();
		this.stop = false;
		this.StartTimer();
	}
	public void StartTimer()
	{
		this.stop = false;
		this.timer = 0f;
	}
	private void Update()
	{
		if (!GameController.Instance)
		{
			return;
		}
		if (!GameController.Instance.playing || this.stop)
		{
			return;
		}
		this.timer += Time.deltaTime;
		AutoSplitterData.inGameTime = (double)this.timer;
		this.text.text = Timer.GetFormattedTime(this.timer);
	}
	public static string GetFormattedTime(float f)
	{
		if (f == 0f)
		{
			return "no time";
		}
		string arg = Mathf.Floor(f / 60f).ToString("00");
		string arg2 = Mathf.Floor(f % 60f).ToString("00");
		(f * 1000f % 1000f).ToString("000");
		string text = (f * 100f % 100f).ToString("00");
		if (text.Equals("100"))
		{
			text = "99";
		}
		return string.Format("{0}:{1}:{2}", arg, arg2, text);
	}
	public float GetTimer()
	{
		return this.timer;
	}
	public void Stop()
	{
		this.stop = true;
	}
	public int GetMinutes()
	{
		return (int)Mathf.Floor(this.timer / 60f);
	}
	private TextMeshProUGUI text;
	private float timer;
	private bool stop;
}
