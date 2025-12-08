using System;
using TMPro;
using UnityEngine;
public class ProgressController : MonoBehaviour
{
	private void Awake()
	{
		this.defaultLevelScale = this.level.transform.localScale;
	}
	public void SetProgress(int oldXp, int newXp, int oldLevel, int oldMoney, int newMoney)
	{
		this.xp = (float)oldXp;
		this.desiredXp = newXp;
		this.currentLevel = oldLevel;
		this.currentMoney = (float)oldMoney;
		this.desiredMoney = (float)newMoney;
		this.level.text = "Lvl " + oldLevel;
		this.money.text = "$" + oldMoney;
		this.xpGained.text = "+ " + (newXp - oldXp) + "xp";
		this.moneyGained.text = "+ " + (newMoney - oldMoney) + "$";
		float num = (float)((int)this.xp - SaveManager.Instance.state.XpForLevel(this.currentLevel));
		int num2 = SaveManager.Instance.state.XpForLevel(this.currentLevel + 1) - SaveManager.Instance.state.XpForLevel(this.currentLevel);
		float x = num / (float)num2;
		this.progress.localScale = new Vector3(x, 1f, 1f);
		base.Invoke("GetReady", 0.5f);
	}
	private void GetReady()
	{
		this.ready = true;
	}
	private void Update()
	{
		if (!this.ready || (UnlockManager.Instance && UnlockManager.Instance.gameObject.activeInHierarchy))
		{
			return;
		}
		this.xp = Mathf.Lerp(this.xp, (float)this.desiredXp, Time.fixedDeltaTime * 0.5f);
		this.currentMoney = Mathf.Lerp(this.currentMoney, this.desiredMoney, Time.fixedDeltaTime * 0.5f);
		float num = (float)((int)this.xp - SaveManager.Instance.state.XpForLevel(this.currentLevel));
		int num2 = SaveManager.Instance.state.XpForLevel(this.currentLevel + 1) - SaveManager.Instance.state.XpForLevel(this.currentLevel);
		float x = num / (float)num2;
		this.progress.localScale = new Vector3(x, 1f, 1f);
		this.money.text = "$" + Mathf.CeilToInt(this.currentMoney);
		if (SaveManager.Instance.state.GetLevel((int)this.xp) > this.currentLevel)
		{
			MonoBehaviour.print("levelled up!");
			this.level.transform.localScale = this.defaultLevelScale * 1.5f;
			this.currentLevel++;
			this.level.text = "Lvl " + this.currentLevel;
		}
		this.ScaleLevel();
	}
	private void ScaleLevel()
	{
		this.level.transform.localScale = Vector3.Lerp(this.level.transform.localScale, this.defaultLevelScale, Time.deltaTime * 1f);
	}
	public Transform progress;
	public TextMeshProUGUI level;
	public TextMeshProUGUI money;
	public TextMeshProUGUI xpGained;
	public TextMeshProUGUI moneyGained;
	private float currentMoney;
	private float desiredMoney;
	private float xp;
	private int desiredXp;
	private int currentLevel;
	private bool ready;
	private Vector3 defaultLevelScale;
}
