using System;
using UnityEngine;
public class ItemCycle : MonoBehaviour
{
	public int selected { get; set; }
	public int max { get; set; }
	public bool activeCycle { get; set; } = true;
	public virtual void Cycle(int n)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.selected += n;
		if (this.selected >= this.max)
		{
			this.selected = 0;
		}
		if (this.selected < 0)
		{
			this.selected = this.max - 1;
		}
	}
}
