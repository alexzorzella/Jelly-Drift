using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CarCycle : ItemCycle
{
	private void Start()
	{
		base.max = CarDisplay.Instance.nCars;
	}
	private void OnEnable()
	{
		if (CarDisplay.Instance)
		{
			int lastCar = SaveManager.Instance.state.lastCar;
			base.selected = lastCar;
			CarDisplay.Instance.SpawnCar(lastCar);
			this.name.text = "| " + CarDisplay.Instance.currentCar.name;
			CarDisplay.Instance.SetSkin(SaveManager.Instance.state.lastSkin[lastCar]);
			this.carStats.SetStats(base.selected);
			this.skinCycle.selected = SaveManager.Instance.state.lastSkin[lastCar];
		}
	}
	public override void Cycle(int n)
	{
		base.Cycle(n);
		this.skinCycle.SetCarToCycle(base.selected);
		CarDisplay.Instance.SpawnCar(base.selected);
		if (SaveManager.Instance.state.carsUnlocked[base.selected])
		{
			this.name.text = "| " + CarDisplay.Instance.currentCar.name;
			SaveManager.Instance.state.lastCar = base.selected;
			SaveManager.Instance.state.lastSkin[base.selected] = this.skinCycle.selected;
			SaveManager.Instance.Save();
			GameState.Instance.car = base.selected;
			this.nextBtn.enabled = true;
			this.skinCycle.UpdateColor();
		}
		else
		{
			MonoBehaviour.print("not unlcoked");
			string str = "???";
			if (base.selected <= 5)
			{
				str = "<size=60%>Complete " + MapManager.Instance.maps[base.selected - 1].name + " on normal difficulty";
			}
			else if (base.selected == 6)
			{
				str = "<size=60%>Complete all races on hard difficulty";
			}
			else if (base.selected == 7)
			{
				str = "<size=60%>Complete 3-star time on all maps";
			}
			this.name.text = "| " + str;
			this.nextBtn.enabled = false;
			this.skinCycle.text.text = "| ???";
		}
		this.carStats.SetStats(base.selected);
	}
	public void BuyCar()
	{
	}
	public void SaveCar()
	{
		SaveManager.Instance.state.lastCar = base.selected;
		SaveManager.Instance.Save();
		GameState.Instance.car = base.selected;
		GameState.Instance.LoadMap();
	}
	public SkinCycle skinCycle;
	public new TextMeshProUGUI name;
	public Button nextBtn;
	public CarStats carStats;
}
