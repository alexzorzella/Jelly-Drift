using System;
using TMPro;
public class GhostCycle : ItemCycle
{
	private void Awake()
	{
		base.max = 3;
	}
	private void Start()
	{
		this.UpdateText();
	}
	public override void Cycle(int n)
	{
		if (this.mapCycle.lockUi.activeInHierarchy)
		{
			return;
		}
		base.Cycle(n);
		this.UpdateText();
	}
	public void UpdateText()
	{
		GhostCycle.Ghost selected = (GhostCycle.Ghost)base.selected;
		this.ghost = selected;
		this.ghostText.text = " (" + selected + ")";
		string str = " (" + selected + ")";
		string str2 = "| ";
		if (selected == GhostCycle.Ghost.Dani)
		{
			str2 += Timer.GetFormattedTime(SaveManager.Instance.state.daniTimes[this.mapCycle.selected]);
		}
		else if (selected == GhostCycle.Ghost.PB)
		{
			str2 += Timer.GetFormattedTime(SaveManager.Instance.state.times[this.mapCycle.selected]);
		}
		this.ghostText.text = str2 + str;
		GameState.Instance.ghost = this.ghost;
	}
	private GhostCycle.Ghost ghost;
	public TextMeshProUGUI ghostText;
	public MapCycle mapCycle;
	public enum Ghost
	{
		PB,
		Dani,
		Off
	}
}
