using System;
using TMPro;
using UnityEngine;
public class CarButton : MonoBehaviour
{
	private void Awake()
	{
		this.SetState(CarButton.ButtonState.Next);
	}
	public void SetState(CarButton.ButtonState state)
	{
		this.state = state;
		if (state == CarButton.ButtonState.Next)
		{
			this.text.text = "Next";
			return;
		}
		this.text.text = "Buy";
	}
	public void Use()
	{
		if (this.state == CarButton.ButtonState.Next)
		{
			this.carCycle.SaveCar();
			return;
		}
		if (this.state == CarButton.ButtonState.BuySkin)
		{
			this.skinCycle.BuySkin();
			return;
		}
		if (this.state == CarButton.ButtonState.BuyCar)
		{
			this.carCycle.BuyCar();
		}
	}
	private CarButton.ButtonState state;
	public TextMeshProUGUI text;
	public CarCycle carCycle;
	public SkinCycle skinCycle;
	public enum ButtonState
	{
		Next,
		BuySkin,
		BuyCar
	}
}
