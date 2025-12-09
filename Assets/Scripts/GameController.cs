using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController Instance;
    public Transform path;
    public Transform checkPoints;
    public LookAtTarget endCamera;
    public int finalCheckpoint;
    public float startTime = 1.5f;
    bool victory;
    public Transform startPos { get; set; }
    public GameObject currentCar { get; set; }
    public bool playing { get; set; }

    void Awake() {
        Instance = this;
        Time.timeScale = 1f;
        startPos = checkPoints.GetChild(0);
        Invoke("StartRace", startTime);

        currentCar = ResourceLoader.InstantiateObject("Car", startPos.position, startPos.rotation);
        currentCar.GetComponent<Car>().Initialize(CarCatalogue.GetSelectedCarData());
        
        // currentCar.GetComponent<CarSkin>().SetSkin(GameState.Instance.skin);
    }

    void Start() {
        CameraController.Instance.AssignTarget(currentCar.transform);
        ShakeController.Instance.car = currentCar.GetComponent<Car>();
        ReplayController.Instance.car = currentCar.GetComponent<Car>();
        currentCar.AddComponent<CheckpointUser>();
    }

    void Update() {
        PlayerInput();
    }

    void StartRace() {
        playing = true;
        Timer.Instance.StartTimer();
    }

    void PlayerInput() {
        if (IsInvoking("ShowFinishScreen")) {
            if (Input.GetButtonDown("Cancel")) {
                CancelInvoke("ShowFinishScreen");
                ShowFinishScreen();
            }

            return;
        }

        if (Input.GetButtonDown("Cancel") && !Pause.Instance.paused) {
            Pause.Instance.TogglePause();
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Recover() {
        var component = currentCar.GetComponent<CheckpointUser>();
        if (!component) {
            return;
        }

        print("cur check: " + component.GetCurrentCheckpoint(finalCheckpoint == 0));
        var child = checkPoints.GetChild(component.GetCurrentCheckpoint(finalCheckpoint == 0));
        currentCar.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        currentCar.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        currentCar.transform.rotation = child.rotation;
        currentCar.transform.position = child.position;
    }

    public void FinishRace(bool win, Transform car) {
        if (!playing) {
            return;
        }

        victory = win;
        playing = false;
        Time.timeScale = 0.3f;
        Invoke("ShowFinishScreen", 1f);
        if (endCamera) {
            endCamera.target = car;
            endCamera.gameObject.SetActive(true);
            CameraController.Instance.gameObject.SetActive(false);
        }
    }

    public void ShowFinishScreen() {
        FinishController.Instance.Open(victory);
    }
}