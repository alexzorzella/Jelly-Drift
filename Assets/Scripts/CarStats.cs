using System;
using UnityEngine;
using UnityEngine.UI;
public class CarStats : MonoBehaviour
{
	public void SetStats(int car)
	{
		Car component = PrefabManager.Instance.cars[car].GetComponent<Car>();
		float engineForce = component.engineForce;
		float driftMultiplier = component.driftMultiplier;
		float num = component.stability;
		this.dSpeed = (engineForce - this.minSpeed) / (this.maxSpeed - this.minSpeed);
		this.dDrift = (driftMultiplier - this.minDrift) / (this.maxDrift - this.minDrift);
		this.dStability = (num - this.minStab) / (this.maxStab - this.minStab);
	}
	private void Update()
	{
		this.speed.transform.localScale = Vector3.Lerp(this.speed.transform.localScale, new Vector3(this.dSpeed, 1f, 1f), Time.deltaTime * 4f);
		this.drift.transform.localScale = Vector3.Lerp(this.drift.transform.localScale, new Vector3(this.dDrift, 1f, 1f), Time.deltaTime * 4f);
		this.stability.transform.localScale = Vector3.Lerp(this.stability.transform.localScale, new Vector3(this.dStability, 1f, 1f), Time.deltaTime * 4f);
	}
	public Image speed;
	public Image drift;
	public Image stability;
	public float minSpeed;
	public float maxSpeed;
	public float minDrift;
	public float maxDrift;
	public float minStab;
	public float maxStab;
	private float dSpeed;
	private float dDrift;
	private float dStability;
}
