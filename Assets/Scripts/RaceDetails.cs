using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class RaceDetails : MonoBehaviour
{
	public void UpdateStars(int map)
	{
		int num = SaveManager.Instance.state.races[map] + 1;
		if (num <= 0)
		{
			this.text.text = "None";
		}
		else
		{
			this.text.text = ((DifficultyCycle.Difficulty)(num - 1)).ToString();
		}
		for (int i = 0; i < this.pbStars.Length; i++)
		{
			if (i < num)
			{
				this.pbStars[i].color = Color.yellow;
			}
			else
			{
				this.pbStars[i].color = Color.gray;
			}
		}
	}
	public Image[] pbStars;
	public TextMeshProUGUI text;
}
