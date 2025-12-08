using System;
using TMPro;
using UnityEngine;
public class DifficultyCycle : ItemCycle
{
	private void Awake()
	{
		base.max = 3;
		base.selected = SaveManager.Instance.state.lastDifficulty;
		MonoBehaviour.print("loaded selected: " + base.selected);
	}
	private void Start()
	{
		this.UpdateText();
		MonoBehaviour.print("in start method: " + base.selected);
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
		DifficultyCycle.Difficulty selected = (DifficultyCycle.Difficulty)base.selected;
		this.ghostText.text = "| " + selected;
		GameState.Instance.difficulty = selected;
		SaveManager.Instance.state.lastDifficulty = base.selected;
		SaveManager.Instance.Save();
		MonoBehaviour.print("saved last difficulty as:  " + base.selected);
	}
	public void UpdateTextOnly()
	{
		DifficultyCycle.Difficulty selected = (DifficultyCycle.Difficulty)base.selected;
		this.ghostText.text = "| " + selected;
	}
	public TextMeshProUGUI ghostText;
	public MapCycle mapCycle;
	public enum Difficulty
	{
		Easy,
		Normal,
		Hard
	}
}
