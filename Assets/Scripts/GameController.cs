using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
	public Transform startPos { get; set; }
	public GameObject currentCar { get; set; }
	public bool playing { get; set; }
	private void Awake()
	{
		GameController.Instance = this;
		Time.timeScale = 1f;
		this.startPos = this.checkPoints.GetChild(0);
		base.Invoke("StartRace", this.startTime);
		this.currentCar = UnityEngine.Object.Instantiate<GameObject>(PrefabManager.Instance.cars[GameState.Instance.car], this.startPos.position, this.startPos.rotation);
		this.currentCar.GetComponent<CarSkin>().SetSkin(GameState.Instance.skin);
	}
	private void Start()
	{
		CameraController.Instance.AssignTarget(this.currentCar.transform);
		ShakeController.Instance.car = this.currentCar.GetComponent<Car>();
		ReplayController.Instance.car = this.currentCar.GetComponent<Car>();
		this.currentCar.AddComponent<CheckpointUser>();
	}
	private void StartRace()
	{
		this.playing = true;
		Timer.Instance.StartTimer();
	}
	private void Update()
	{
		this.PlayerInput();
	}
	private void PlayerInput()
	{
		if (base.IsInvoking("ShowFinishScreen"))
		{
			if (Input.GetButtonDown("Cancel"))
			{
				base.CancelInvoke("ShowFinishScreen");
				this.ShowFinishScreen();
			}
			return;
		}
		if (Input.GetButtonDown("Cancel") && !Pause.Instance.paused)
		{
			Pause.Instance.TogglePause();
		}
	}
	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void Recover()
	{
		CheckpointUser component = this.currentCar.GetComponent<CheckpointUser>();
		if (!component)
		{
			return;
		}
		MonoBehaviour.print("cur check: " + component.GetCurrentCheckpoint(this.finalCheckpoint == 0));
		Transform child = this.checkPoints.GetChild(component.GetCurrentCheckpoint(this.finalCheckpoint == 0));
		this.currentCar.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
		this.currentCar.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		this.currentCar.transform.rotation = child.rotation;
		this.currentCar.transform.position = child.position;
	}
	public void FinishRace(bool win, Transform car)
	{
		if (!this.playing)
		{
			return;
		}
		this.victory = win;
		this.playing = false;
		Time.timeScale = 0.3f;
		base.Invoke("ShowFinishScreen", 1f);
		if (this.endCamera)
		{
			this.endCamera.target = car;
			this.endCamera.gameObject.SetActive(true);
			CameraController.Instance.gameObject.SetActive(false);
		}
	}
	public void ShowFinishScreen()
	{
		FinishController.Instance.Open(this.victory);
	}
	public Transform path;
	public Transform checkPoints;
	public LookAtTarget endCamera;
	public int finalCheckpoint;
	public float startTime = 1.5f;
	public static GameController Instance;
	private bool victory;
}
