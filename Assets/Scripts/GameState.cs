using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameState : MonoBehaviour
{
	public int car { get; set; } = 1;
	public int map { get; set; }
	public Gamemode gamemode { get; set; }
	public int skin { get; set; } = 1;
	private void Awake()
	{
		if (GameState.Instance != null && GameState.Instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			GameState.Instance = this;
		}
		GameState.Instance = this;
	}
	public void LoadMap()
	{
		SceneManager.LoadScene(string.Concat(this.map));
	}
	public GhostCycle.Ghost ghost;
	public DifficultyCycle.Difficulty difficulty = DifficultyCycle.Difficulty.Normal;
	public static GameState Instance;
}
