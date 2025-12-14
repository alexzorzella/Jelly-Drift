using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishController : MonoBehaviour {
    public static FinishController Instance;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI pbTimer;
    public TextMeshProUGUI mapName;
    public TextMeshProUGUI newBest;
    public TextMeshProUGUI victoryText;
    public GameObject timePanel;
    public GameObject racePanel;
    public ProgressController progressTime;
    public ProgressController progressRace;
    public UnlockManager unlockManager;
    public Image[] stars;

    void Awake() {
        Instance = this;
    }

    public void Open(bool victory) {
        if (GameState.i.gamemode == Gamemode.TimeTrial) {
            timePanel.SetActive(true);
            mapName.text = MapManager.i.GetSelectedMap().GetName();
            var num = Timer.Instance.GetTimer();
            // var num2 = SaveManager.Instance.state.times[map];
            // if (num < num2 || num2 == 0f) {
            //     SaveManager.Instance.state.times[map] = num;
            //     SaveManager.Instance.Save();
            //     newBest.gameObject.SetActive(true);
            //     ReplayController.Instance.Save();
            // }
            //
            // var num3 = MapManager.Instance.GetStars(map, num);
            // print(string.Concat("checking stars for time: ", num, ", stars: ", num3));
            // for (var i = 0; i < num3; i++) {
            //     stars[i].color = Color.yellow;
            // }

            timer.text = Timer.GetFormattedTime(num);
            // pbTimer.text = "Best | " + Timer.GetFormattedTime(SaveManager.Instance.state.times[map]);
        }
        else if (GameState.i.gamemode == Gamemode.Race) {
            racePanel.SetActive(true);
            if (victory) {
                victoryText.text = "Victory";
            }
            else {
                victoryText.text = "Defeat";
            }
        }

        // CheckUnlocks(victory);
        SaveManager.Instance.Save();
        
        var num4 = 50;
        var num5 = 50;
        if (GameState.i.gamemode == Gamemode.Race) {
            progressRace.SetProgress(SaveManager.Instance.state.xp, SaveManager.Instance.state.xp + num4,
                SaveManager.Instance.state.GetLevel(), SaveManager.Instance.state.money,
                SaveManager.Instance.state.money + num5);
        }
        else {
            progressTime.SetProgress(SaveManager.Instance.state.xp, SaveManager.Instance.state.xp + num4,
                SaveManager.Instance.state.GetLevel(), SaveManager.Instance.state.money,
                SaveManager.Instance.state.money + num5);
        }

        SaveManager.Instance.state.xp += num4;
        SaveManager.Instance.state.money += num5;
        if (unlockManager.unlocks.Count > 0) {
            unlockManager.gameObject.SetActive(true);
        }
    }

    void CheckUnlocks(bool victory) {
        // var map = GameState.Instance.map;
        // if (GameState.Instance.gamemode == Gamemode.Race && victory) {
        //     var num = SaveManager.Instance.state.races[map];
        //     var difficulty = (int)GameState.Instance.difficulty;
        //     if (difficulty > num) {
        //         SaveManager.Instance.state.races[map] = difficulty;
        //         SaveManager.Instance.Save();
        //     }
        //
        //     var num2 = GameState.Instance.map + 1;
        //     if (GameState.Instance.difficulty >= DifficultyCycle.Difficulty.Easy &&
        //         num2 < MapManager.Instance.maps.Length && !SaveManager.Instance.state.mapsUnlocked[num2]) {
        //         SaveManager.Instance.state.mapsUnlocked[num2] = true;
        //         SaveManager.Instance.Save();
        //         unlockManager.unlocks.Add(new UnlockManager.Unlock(UnlockManager.UnlockType.Map, num2, 0));
        //     }
        //
        //     if (GameState.Instance.difficulty >= DifficultyCycle.Difficulty.Normal &&
        //         !SaveManager.Instance.state.carsUnlocked[num2]) {
        //         SaveManager.Instance.state.carsUnlocked[num2] = true;
        //         SaveManager.Instance.Save();
        //         unlockManager.unlocks.Add(new UnlockManager.Unlock(UnlockManager.UnlockType.Car, num2, 0));
        //     }
        //
        //     if (GameState.Instance.difficulty >= DifficultyCycle.Difficulty.Hard &&
        //         !SaveManager.Instance.state.skins[GameState.Instance.map][1]) {
        //         SaveManager.Instance.state.skins[GameState.Instance.map][1] = true;
        //         SaveManager.Instance.Save();
        //         unlockManager.unlocks.Add(new UnlockManager.Unlock(UnlockManager.UnlockType.Skin,
        //             GameState.Instance.map, 1));
        //     }
        // }
        //
        // if (GameState.Instance.gamemode == Gamemode.TimeTrial &&
        //     MapManager.Instance.GetStars(GameState.Instance.map, Timer.Instance.GetTimer()) == 3 &&
        //     !SaveManager.Instance.state.skins[map][2]) {
        //     SaveManager.Instance.state.skins[map][2] = true;
        //     SaveManager.Instance.Save();
        //     unlockManager.unlocks.Add(new UnlockManager.Unlock(UnlockManager.UnlockType.Skin, map, 2));
        // }
        //
        // if (!SaveManager.Instance.state.carsUnlocked[6]) {
        //     var num3 = 0;
        //     while (num3 < MapManager.Instance.maps.Length && SaveManager.Instance.state.races[num3] >= 2) {
        //         if (num3 == MapManager.Instance.maps.Length - 1) {
        //             SaveManager.Instance.state.carsUnlocked[6] = true;
        //             SaveManager.Instance.Save();
        //             unlockManager.unlocks.Add(new UnlockManager.Unlock(UnlockManager.UnlockType.Car, 6, 0));
        //         }
        //
        //         num3++;
        //     }
        // }
        //
        // if (!SaveManager.Instance.state.carsUnlocked[7]) {
        //     var num4 = 0;
        //     while (num4 < MapManager.Instance.maps.Length &&
        //            MapManager.Instance.GetStars(num4, SaveManager.Instance.state.times[num4]) >= 3) {
        //         if (num4 == MapManager.Instance.maps.Length - 1) {
        //             SaveManager.Instance.state.carsUnlocked[7] = true;
        //             SaveManager.Instance.Save();
        //             unlockManager.unlocks.Add(new UnlockManager.Unlock(UnlockManager.UnlockType.Car, 7, 0));
        //         }
        //
        //         num4++;
        //     }
        // }
        //
        // SaveManager.Instance.Save();
        // print("1");
        // if (!SaveManager.Instance.state.skins[5][1]) {
        //     print("2");
        //     for (var i = 0; i < MapManager.Instance.maps.Length; i++) {
        //         var num5 = SaveManager.Instance.state.times[i];
        //         var num6 = SaveManager.Instance.state.daniTimes[i];
        //         print("i: " + i);
        //         if (num5 > num6) {
        //             break;
        //         }
        //
        //         if (i == MapManager.Instance.maps.Length - 1) {
        //             SaveManager.Instance.state.skins[5][1] = true;
        //             SaveManager.Instance.Save();
        //             unlockManager.unlocks.Add(new UnlockManager.Unlock(UnlockManager.UnlockType.Skin, 5, 1));
        //         }
        //     }
        // }
    }

    public void Restart() {
        GameController.Instance.RestartGame();
        Time.timeScale = 1f;
    }

    public void Menu() {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
}