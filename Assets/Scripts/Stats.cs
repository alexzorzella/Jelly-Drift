using System;
using TMPro;
using UnityEngine;
public class Stats : MonoBehaviour
{
	private void OnEnable()
	{
		MonoBehaviour.print("text:  " + this.text);
		this.text.text = "<size=110%>Times\n<size=75%>";
		for (int i = 0; i < MapManager.Instance.maps.Length; i++)
		{
			string name = MapManager.Instance.maps[i].name;
			string formattedTime = Timer.GetFormattedTime(SaveManager.Instance.state.times[i]);
			TextMeshProUGUI textMeshProUGUI = this.text;
			textMeshProUGUI.text = string.Concat(new string[]
			{
				textMeshProUGUI.text,
				name,
				" - ",
				formattedTime,
				"\n"
			});
		}
	}
	public void DeleteSave()
	{
		SaveManager.Instance.NewSave();
		SaveManager.Instance.Save();
	}
	public TextMeshProUGUI text;
}
