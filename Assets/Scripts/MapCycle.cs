using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MapCycle : ItemCycle
{
	private void Awake()
	{
		base.selected = SaveManager.Instance.state.lastMap;
	}
	private void Start()
	{
		if (this.starsDetails)
		{
			this.starTimes = this.starsDetails.GetComponentsInChildren<TextMeshProUGUI>();
		}
		this.SetMap(base.selected);
		base.max = MapManager.Instance.maps.Length;
		CarDisplay.Instance.Hide();
	}
	private void OnEnable()
	{
		if (!CarDisplay.Instance || !CarDisplay.Instance.currentCar)
		{
			return;
		}
		CarDisplay.Instance.Hide();
		base.selected = SaveManager.Instance.state.lastMap;
		this.SetMap(base.selected);
	}
	public override void Cycle(int n)
	{
		base.Cycle(n);
		this.SetMap(base.selected);
		GameState.Instance.map = base.selected;
	}
	private void Update()
	{
		if (this.lockUi.activeInHierarchy)
		{
			this.overlay.color = Color.Lerp(this.overlay.color, new Color(1f, 1f, 1f, 0.55f), Time.deltaTime * 1.2f);
			return;
		}
		this.overlay.color = Color.Lerp(this.overlay.color, MapManager.Instance.maps[base.selected].themeColor, Time.deltaTime * 0.9f);
	}
	private void SetMap(int n)
	{
		if (this.raceDetails)
		{
			this.raceDetails.UpdateStars(base.selected);
		}
		if (SaveManager.Instance.state.mapsUnlocked[n])
		{
			this.lockUi.SetActive(false);
			this.mapImg.sprite = MapManager.Instance.maps[n].image;
			this.name.text = "| " + MapManager.Instance.maps[n].name;
			this.time.text = "PB - " + Timer.GetFormattedTime(SaveManager.Instance.state.times[n]);
			if (this.ghostCycle)
			{
				this.ghostCycle.UpdateText();
			}
			if (this.difficultyCycle)
			{
				this.difficultyCycle.UpdateTextOnly();
			}
			if (this.starsDetails)
			{
				this.UpdateStars();
			}
			GameState.Instance.map = base.selected;
			this.nextButton.enabled = true;
			this.nextButton.GetComponent<ItemCycle>().activeCycle = true;
			SaveManager.Instance.state.lastMap = base.selected;
			SaveManager.Instance.Save();
			return;
		}
		this.lockUi.SetActive(true);
		this.mapImg.sprite = MapManager.Instance.maps[n].image;
		this.name.text = "| <size=60%>Complete " + MapManager.Instance.maps[n - 1].name + " on easy difficulty";
		this.time.text = "";
		this.ghostText.text = "| ";
		this.nextButton.enabled = false;
		this.nextButton.GetComponent<ItemCycle>().activeCycle = false;
	}
	private void UpdateStars()
	{
		MonoBehaviour.print(this.starTimes.Length);
		for (int i = 0; i < this.starTimes.Length; i++)
		{
			this.starTimes[i].text = Timer.GetFormattedTime(MapManager.Instance.maps[base.selected].times[i]);
		}
		int stars = MapManager.Instance.GetStars(base.selected, SaveManager.Instance.state.times[base.selected]);
		for (int j = 0; j < this.pbStars.Length; j++)
		{
			if (j < stars)
			{
				this.pbStars[j].color = Color.yellow;
			}
			else
			{
				this.pbStars[j].color = Color.gray;
			}
		}
	}
	public void SaveMap()
	{
		GameState.Instance.map = base.selected;
		GameState.Instance.gamemode = this.gamemode;
	}
	public Image mapImg;
	public Image overlay;
	public new TextMeshProUGUI name;
	public TextMeshProUGUI time;
	public GhostCycle ghostCycle;
	public DifficultyCycle difficultyCycle;
	public TextMeshProUGUI ghostText;
	public Button nextButton;
	public Transform starsDetails;
	public TextMeshProUGUI[] starTimes;
	public Image[] pbStars;
	public GameObject lockUi;
	public Gamemode gamemode;
	public RaceDetails raceDetails;
}
