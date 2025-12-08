using System;
using UnityEngine;
public class Race : MonoBehaviour
{
	public GameObject enemyCar { get; set; }
	private void Awake()
	{
		if (GameState.Instance.gamemode != Gamemode.Race)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		this.gameController = base.gameObject.GetComponent<GameController>();
		Transform startPos = this.gameController.startPos;
		this.enemyCar = UnityEngine.Object.Instantiate<GameObject>(this.enemyCarPrefab, startPos.position + startPos.forward * 10f, startPos.rotation);
		this.enemyCar.GetComponent<CarAI>().SetPath(this.gameController.path);
	}
	private void Start()
	{
		this.enemyCar.AddComponent<CheckpointUser>().player = false;
		GameController.Instance.currentCar.AddComponent<CheckpointUser>();
	}
	public GameObject enemyCarPrefab;
	private GameController gameController;
}
