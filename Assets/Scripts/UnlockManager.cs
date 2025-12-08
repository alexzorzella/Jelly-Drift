using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class UnlockManager : MonoBehaviour
{
	private void Start()
	{
		UnlockManager.Instance = this;
		this.defaultSize = base.transform.localScale;
		this.desiredSize = Vector3.zero;
		base.transform.localScale = Vector3.zero;
		this.carDisplay = UnityEngine.Object.Instantiate<GameObject>(this.unlockDisplayPrefab).GetComponentInChildren<CarDisplay>();
		this.maps = this.carDisplay.transform.parent.GetChild(2);
		this.NextDisplay();
	}
	public void NextDisplay()
	{
		this.readyToSkip = false;
		base.Invoke("SetSkipReady", 0.4f);
		if (this.n >= this.unlocks.Count)
		{
			base.gameObject.SetActive(false);
			return;
		}
		List<UnlockManager.Unlock> list = this.unlocks;
		int num = this.n;
		this.n = num + 1;
		this.DisplayUnlock(list[num]);
		SoundManager.Instance.PlayUnlock();
	}
	public void DisplayUnlock(UnlockManager.Unlock u)
	{
		for (int i = 0; i < this.maps.childCount; i++)
		{
			this.maps.GetChild(i).gameObject.SetActive(false);
		}
		base.transform.localScale = Vector3.zero;
		this.desiredSize = this.defaultSize;
		string str = "";
		if (u.type == UnlockManager.UnlockType.Car)
		{
			SaveManager.Instance.state.carsUnlocked[u.index] = true;
			SaveManager.Instance.Save();
			str = "\"" + PrefabManager.Instance.cars[u.index].name + "\"";
			this.carDisplay.SpawnCar(u.index);
			this.carDisplay.SetSkin(SaveManager.Instance.state.skins[u.index].Length - 1);
		}
		else if (u.type == UnlockManager.UnlockType.Skin)
		{
			SaveManager.Instance.state.skins[u.index][u.subIndex] = true;
			SaveManager.Instance.Save();
			this.carDisplay.SpawnCar(u.index);
			this.carDisplay.SetSkin(u.subIndex);
			str = PrefabManager.Instance.cars[u.index].name + " \"" + this.carDisplay.currentCar.GetComponent<CarSkin>().GetSkinName(u.subIndex) + "\"";
		}
		else if (u.type == UnlockManager.UnlockType.Map)
		{
			SaveManager.Instance.state.mapsUnlocked[u.index] = true;
			SaveManager.Instance.Save();
			this.maps.GetChild(u.index).gameObject.SetActive(true);
			str = "\"" + MapManager.Instance.maps[u.index].name + "\"";
			this.carDisplay.Hide();
		}
		this.text.text = "<size=100%>unlocked:\n<b><size=80%>" + str;
	}
	private void Update()
	{
		base.transform.localScale = Vector3.Lerp(base.transform.localScale, this.desiredSize, Time.unscaledDeltaTime * 3f);
		if (Input.anyKey && this.readyToSkip)
		{
			this.NextDisplay();
		}
	}
	private void SetSkipReady()
	{
		this.readyToSkip = true;
	}
	public TextMeshProUGUI text;
	private Vector3 defaultSize;
	private Vector3 desiredSize;
	private Transform maps;
	private CarDisplay carDisplay;
	public GameObject unlockDisplayPrefab;
	public List<UnlockManager.Unlock> unlocks = new List<UnlockManager.Unlock>();
	public static UnlockManager Instance;
	private int n;
	private bool readyToSkip;
	public enum UnlockType
	{
		Car,
		Skin,
		Map
	}
	public class Unlock
	{
		public Unlock(UnlockManager.UnlockType t, int index, int subIndex)
		{
			this.type = t;
			this.index = index;
			this.subIndex = subIndex;
		}
		public UnlockManager.UnlockType type;
		public int index;
		public int subIndex;
	}
}
