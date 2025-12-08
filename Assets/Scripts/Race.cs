using UnityEngine;

public class Race : MonoBehaviour {
    public GameObject enemyCarPrefab;
    GameController gameController;
    public GameObject enemyCar { get; set; }

    void Awake() {
        if (GameState.Instance.gamemode != Gamemode.Race) {
            Destroy(this);
            return;
        }

        gameController = gameObject.GetComponent<GameController>();
        var startPos = gameController.startPos;
        enemyCar = Instantiate(enemyCarPrefab, startPos.position + startPos.forward * 10f, startPos.rotation);
        enemyCar.GetComponent<CarAI>().SetPath(gameController.path);
    }

    void Start() {
        enemyCar.AddComponent<CheckpointUser>().player = false;
        GameController.Instance.currentCar.AddComponent<CheckpointUser>();
    }
}