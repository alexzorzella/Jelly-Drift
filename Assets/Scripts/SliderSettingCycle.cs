using System;
using UnityEngine;
using UnityEngine.UI;
public class SliderSettingCycle : ItemCycle
{
	public Image[] options { get; private set; }
	private void Awake()
	{
		this.options = this.optionsParent.transform.GetChild(base.transform.GetSiblingIndex()).GetComponentsInChildren<Image>();
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
			if (i <= base.selected)
			{
				this.options[i].color = this.selectedC;
			}
			else
			{
				this.options[i].color = this.deselectedC;
			}
		}
	}
	public Color deselectedC = new Color(1f, 1f, 1f, 0.3f);
	public Color selectedC = new Color(1f, 1f, 1f);
	public GameObject optionsParent;
	public SettingsUi settings;
}
