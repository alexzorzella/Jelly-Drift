using System;
using UnityEngine;
public class CheckpointUser : MonoBehaviour
{
	private void Awake()
	{
		this.checkedPoints = new bool[GameController.Instance.checkPoints.childCount];
	}
	public int GetCurrentCheckpoint(bool loopMap)
	{
		int num = 0;
		int num2 = 1;
		if (!loopMap)
		{
			num2 = 0;
		}
		int num3 = num2;
		while (num3 < this.checkedPoints.Length && this.checkedPoints[num3])
		{
			num++;
			num3++;
		}
		if (!loopMap)
		{
			return num - 1;
		}
		return num;
	}
	public bool CheckPoint(CheckPoint p)
	{
		if (p.nr != GameController.Instance.finalCheckpoint)
		{
			this.ClearCheckpoint(p.nr);
			return true;
		}
		if (this.ReadyToFinish())
		{
			GameController.Instance.FinishRace(this.player, base.transform);
			this.ClearCheckpoint(p.nr);
			return true;
		}
		return false;
	}
	private void ClearCheckpoint(int n)
	{
		if (this.checkedPoints[n])
		{
			return;
		}
		if (GameController.Instance.finalCheckpoint > 0 && n == 0)
		{
			this.checkedPoints[0] = true;
			return;
		}
		this.checkedPoints[n] = true;
		if (!this.player)
		{
			return;
		}
		SplitUi component = UnityEngine.Object.Instantiate<GameObject>(PrefabManager.Instance.splitUi).GetComponent<SplitUi>();
		component.transform.SetParent(UIManager.Instance.splitPos);
		component.transform.localPosition = Vector3.zero;
		component.SetSplit(Timer.GetFormattedTime(Timer.Instance.GetTimer()));
	}
	public void ForceCheckpoint(int n)
	{
		this.checkedPoints[n] = true;
	}
	private bool ReadyToFinish()
	{
		int num = 0;
		for (int i = 0; i < this.checkedPoints.Length; i++)
		{
			if (this.checkedPoints[i])
			{
				num++;
			}
		}
		return num >= this.checkedPoints.Length - 1;
	}
	private bool[] checkedPoints;
	public bool player = true;
}
