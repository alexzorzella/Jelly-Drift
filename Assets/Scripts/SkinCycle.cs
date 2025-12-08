using System;
using TMPro;
public class SkinCycle : ItemCycle
{
	private void Start()
	{
		base.max = SaveManager.Instance.state.skins[this.carCycle.selected].Length;
		this.UpdateColor();
	}
	public void SetCarToCycle(int n)
	{
		base.selected = SaveManager.Instance.state.lastSkin[n];
		base.max = SaveManager.Instance.state.skins[n].Length;
	}
	public override void Cycle(int n)
	{
		base.Cycle(n);
		this.UpdateColor();
	}
	public void UpdateColor()
	{
		if (!SaveManager.Instance.state.carsUnlocked[this.carCycle.selected])
		{
			return;
		}
		CarDisplay.Instance.SetSkin(base.selected);
		int num = 0;
		if (SaveManager.Instance.state.skins[this.carCycle.selected][base.selected])
		{
			num = base.selected;
			SaveManager.Instance.state.lastSkin[this.carCycle.selected] = num;
			SaveManager.Instance.Save();
		}
		GameState.Instance.skin = num;
		this.UpdateText(num == base.selected);
	}
	public void UpdateText(bool unlocked)
	{
		int selected = this.carCycle.selected;
		int selected2 = base.selected;
		this.carBtn.SetState(CarButton.ButtonState.Next);
		string text = "<size=60%>";
		if (!SaveManager.Instance.state.skins[selected][selected2])
		{
			if (selected < 5)
			{
				if (selected2 == 1)
				{
					text = text + "Complete " + MapManager.Instance.maps[selected].name + " on hard difficulty";
				}
				else if (selected2 == 2)
				{
					text = text + "Complete " + MapManager.Instance.maps[selected].name + " 3-star time";
				}
				else
				{
					this.carBtn.SetState(CarButton.ButtonState.BuySkin);
					int skinPrice = PlayerSave.GetSkinPrice(selected, selected2);
					text = string.Concat(new object[]
					{
						text,
						"<size=80%><font=\"Ubuntu-Bold SDF\">Buy (",
						skinPrice,
						"$)"
					});
				}
			}
			if (selected == 5)
			{
				text += "Beat the ghost of Dani on all maps..";
			}
		}
		else
		{
			text = CarDisplay.Instance.currentCar.GetComponent<CarSkin>().GetSkinName(base.selected);
		}
		this.text.text = "| " + text;
	}
	public void BuySkin()
	{
		int skinPrice = PlayerSave.GetSkinPrice(this.carCycle.selected, base.selected);
		if (SaveManager.Instance.state.money >= skinPrice)
		{
			SaveManager.Instance.state.money -= skinPrice;
			SaveManager.Instance.state.skins[this.carCycle.selected][base.selected] = true;
			SaveManager.Instance.Save();
			this.UpdateText(true);
			this.unlockManager.unlocks.Add(new UnlockManager.Unlock(UnlockManager.UnlockType.Skin, this.carCycle.selected, base.selected));
			this.unlockManager.gameObject.SetActive(true);
			this.menuStats.UpdateStats();
			this.UpdateColor();
			SoundManager.Instance.PlayMoney();
			return;
		}
		SoundManager.Instance.PlayError();
	}
	public TextMeshProUGUI text;
	public CarCycle carCycle;
	public CarButton carBtn;
	public MenuStats menuStats;
	public UnlockManager unlockManager;
}
