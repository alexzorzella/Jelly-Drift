using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapCycle : ItemCycle {
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

    void Awake() {
        selected = SaveManager.Instance.state.lastMap;
    }

    void Start() {
        if (starsDetails) {
            starTimes = starsDetails.GetComponentsInChildren<TextMeshProUGUI>();
        }

        SetMap(selected);
        max = MapManager.Instance.maps.Length;
        CarDisplay.Instance.Hide();
    }

    void Update() {
        if (lockUi.activeInHierarchy) {
            overlay.color = Color.Lerp(overlay.color, new Color(1f, 1f, 1f, 0.55f), Time.deltaTime * 1.2f);
            return;
        }

        overlay.color = Color.Lerp(overlay.color, MapManager.Instance.maps[selected].themeColor, Time.deltaTime * 0.9f);
    }

    void OnEnable() {
        if (!CarDisplay.Instance || !CarDisplay.Instance.currentCar) {
            return;
        }

        CarDisplay.Instance.Hide();
        selected = SaveManager.Instance.state.lastMap;
        SetMap(selected);
    }

    public override void Cycle(int n) {
        base.Cycle(n);
        SetMap(selected);
        GameState.Instance.map = selected;
    }

    void SetMap(int n) {
        if (raceDetails) {
            raceDetails.UpdateStars(selected);
        }

        lockUi.SetActive(false);
        mapImg.sprite = MapManager.Instance.maps[n].image;
        name.text = "| " + MapManager.Instance.maps[n].name;
        time.text = "PB - " + Timer.GetFormattedTime(SaveManager.Instance.state.times[n]);
        if (ghostCycle) {
            ghostCycle.UpdateText();
        }
        
        if (difficultyCycle) {
            difficultyCycle.UpdateTextOnly();
        }
        
        if (starsDetails) {
            UpdateStars();
        }
        
        GameState.Instance.map = selected;
        nextButton.enabled = true;
        nextButton.GetComponent<ItemCycle>().activeCycle = true;
        SaveManager.Instance.state.lastMap = selected;
        SaveManager.Instance.Save();
    }

    void UpdateStars() {
        print(starTimes.Length);
        for (var i = 0; i < starTimes.Length; i++) {
            starTimes[i].text = Timer.GetFormattedTime(MapManager.Instance.maps[selected].times[i]);
        }

        var stars = MapManager.Instance.GetStars(selected, SaveManager.Instance.state.times[selected]);
        for (var j = 0; j < pbStars.Length; j++) {
            if (j < stars) {
                pbStars[j].color = Color.yellow;
            }
            else {
                pbStars[j].color = Color.gray;
            }
        }
    }

    public void SaveMap() {
        GameState.Instance.map = selected;
        GameState.Instance.gamemode = gamemode;
    }
}