using System;
using TMPro;
using UnityEngine;
public class SettingCycle : ItemCycle
{
	public TextMeshProUGUI[] options { get; private set; }
	private void Awake()
	{
		this.options = this.optionsParent.transform.GetChild(base.transform.GetSiblingIndex()).GetComponentsInChildren<TextMeshProUGUI>();
		base.max = this.options.Length;
		this.UpdateOptions();
	}
	public override void Cycle(int n)
	{
		base.Cycle(n);
		this.UpdateOptions();
		this.settings.UpdateSettings();
	}
	public void UpdateOptions()
	{
		for (int i = 0; i < base.max; i++)
		{
			this.options[i].color = this.deselectedC;
			if (i == base.selected)
			{
				this.options[i].color = this.selectedC;
			}
		}
	}
	private Color deselectedC = new Color(1f, 1f, 1f, 0.3f);
	private Color selectedC = new Color(1f, 1f, 1f);
	public GameObject optionsParent;
	public SettingsUi settings;
}
