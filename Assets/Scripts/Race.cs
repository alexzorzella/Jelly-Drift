using UnityEngine;

public class Race : MonoBehaviour {
    public GameObject enemyCarPrefab;
    GameController gameController;
    public GameObject enemyCarObject { get; set; }

    void Awake() {
        if (GameState.Instance.gamemode != Gamemode.Race) {
            Destroy(this);
            return;
        }

        gameController = gameObject.GetComponent<GameController>();
        var startPos = gameController.startPos;
        
        enemyCarObject = ResourceLoader.InstantiateObject("Car", startPos.position + startPos.forward * 10f, startPos.rotation);

        Car enemyCar = enemyCarObject.GetComponent<Car>();
        
        enemyCar.Initialize(CarCatalogue.GetSelectedOpponentCarData(), true);
        
        CarAi carAi = enemyCarObject.AddComponent<CarAi>();
        carAi.Initialize(enemyCar);
        carAi.SetPath(gameController.path);
    }

    void Start() {
        enemyCarObject.AddComponent<CheckpointUser>().player = false;
        GameController.Instance.currentCar.AddComponent<CheckpointUser>();
    }
}