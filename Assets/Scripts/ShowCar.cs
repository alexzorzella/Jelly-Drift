using System;
using UnityEngine;
public class ShowCar : MonoBehaviour
{
	private void OnEnable()
	{
		if (!CarDisplay.Instance || !CarDisplay.Instance.currentCar)
		{
			return;
		}
		if (this.show)
		{
			CarDisplay.Instance.Show();
			return;
		}
		CarDisplay.Instance.Hide();
	}
	public bool show;
}
