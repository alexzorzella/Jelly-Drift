using System;
using TMPro;
using UnityEngine;
public class SplitUi : MonoBehaviour
{
	private void Awake()
	{
		this.text = base.GetComponentInChildren<TextMeshProUGUI>();
		base.Invoke("DestroySelf", 3f);
		base.Invoke("StartFade", 1.5f);
		this.desiredScale = Vector3.one * 1f;
		base.transform.localScale = Vector3.zero;
	}
	private void StartFade()
	{
		this.text.CrossFadeAlpha(0f, 1.5f, true);
	}
	private void Update()
	{
		this.desiredScale = Vector3.Lerp(this.desiredScale, Vector3.zero, Time.deltaTime * this.speed * 0.1f);
		base.transform.localScale = Vector3.Lerp(base.transform.localScale, this.desiredScale, Time.deltaTime * this.speed * 7.5f);
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, Vector3.up, Time.deltaTime * this.speed);
	}
	public void SetSplit(string t)
	{
		this.text.text = t;
	}
	private void DestroySelf()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
	private TextMeshProUGUI text;
	private float speed = 1f;
	private Vector3 desiredScale;
}
