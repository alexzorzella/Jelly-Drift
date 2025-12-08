using System;
using UnityEngine.UI;
public class ButtonCycle : ItemCycle
{
	private void Awake()
	{
		this.btn = base.GetComponent<Button>();
	}
	public override void Cycle(int n)
	{
		if (!this.btn.enabled)
		{
			return;
		}
		this.btn.onClick.Invoke();
	}
	private Button btn;
}
