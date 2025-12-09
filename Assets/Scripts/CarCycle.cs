using TMPro;
using UnityEngine.UI;

public class CarCycle : ItemCycle {
    public SkinCycle skinCycle;
    public new TextMeshProUGUI name;
    public Button nextBtn;
    public CarStats carStats;

    void Start() {
        max = CarDisplay.Instance.nCars;
    }

    void OnEnable() {
        if (CarDisplay.Instance) {
            var lastCar = SaveManager.Instance.state.lastCar;
            selected = lastCar;
            CarDisplay.Instance.SpawnCar(lastCar);
            name.text = "| " + CarDisplay.Instance.currentCar.name;
            CarDisplay.Instance.SetSkin(SaveManager.Instance.state.lastSkin[lastCar]);
            carStats.SetStats(selected);
            skinCycle.selected = SaveManager.Instance.state.lastSkin[lastCar];
        }
    }

    public override void Cycle(int n) {
        base.Cycle(n);
        
        CarCatalogue.CycleSelectedCar(n);
        skinCycle.SetCarToCycle(selected);
        
        CarDisplay.Instance.SpawnCar(selected);
        // if (SaveManager.Instance.state.carsUnlocked[selected]) {
        //     name.text = "| " + CarDisplay.Instance.currentCar.name;
        //     SaveManager.Instance.state.lastCar = selected;
        //     SaveManager.Instance.state.lastSkin[selected] = skinCycle.selected;
        //     SaveManager.Instance.Save();
        //     GameState.Instance.car = selected;
        //     nextBtn.enabled = true;
        //     skinCycle.UpdateColor();
        // }
        // else {
        //     print("not unlcoked");
        //     var str = "???";
        //     if (selected <= 5) {
        //         str = "<size=60%>Complete " + MapManager.Instance.maps[selected - 1].name + " on normal difficulty";
        //     }
        //     else if (selected == 6) {
        //         str = "<size=60%>Complete all races on hard difficulty";
        //     }
        //     else if (selected == 7) {
        //         str = "<size=60%>Complete 3-star time on all maps";
        //     }
        //
        //     name.text = "| " + str;
        //     nextBtn.enabled = false;
        //     skinCycle.text.text = "| ???";
        // }

        carStats.SetStats(selected);
    }

    public void BuyCar() {
    }

    public void SaveCar() {
        SaveManager.Instance.state.lastCar = selected;
        SaveManager.Instance.Save();
        GameState.Instance.car = selected;
        GameState.Instance.LoadMap();
    }
}