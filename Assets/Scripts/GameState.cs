using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {
    public static GameState Instance;
    public GhostCycle.Ghost ghost;
    public DifficultyCycle.Difficulty difficulty = DifficultyCycle.Difficulty.Normal;
    public int car { get; set; } = 1;
    public int map { get; set; }
    public Gamemode gamemode { get; set; }
    public int skin { get; set; } = 1;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }

        Instance = this;
    }

    public void LoadMap() {
        SceneManager.LoadScene(string.Concat(map));
    }
}