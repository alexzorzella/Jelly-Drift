using System;
using TMPro;
using UnityEngine;
public class MenuStats : MonoBehaviour
{
	private void Start()
	{
		MenuStats.Instance = this;
		this.UpdateStats();
		this.currMoney = (float)SaveManager.Instance.state.money;
		this.money.text = "$" + this.currMoney;
	}
	public void UpdateStats()
	{
		float x = SaveManager.Instance.state.LevelProgress();
		this.currentXp.transform.localScale = new Vector3(x, 1f, 1f);
		this.level.text = "Lvl" + SaveManager.Instance.state.GetLevel();
	}
	private void Update()
	{
		this.currMoney = Mathf.Lerp(this.currMoney, (float)SaveManager.Instance.state.money, Time.deltaTime * 3f);
		this.money.text = "$" + Mathf.CeilToInt(this.currMoney);
	}
	public TextMeshProUGUI level;
	public TextMeshProUGUI money;
	public Transform currentXp;
	public static MenuStats Instance;
	private float currMoney;
}
