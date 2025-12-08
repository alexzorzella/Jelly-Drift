using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
	public bool paused { get; set; }
	private void Awake()
	{
		Pause.Instance = this;
	}
	public void PauseGame()
	{
		if (this.paused)
		{
			return;
		}
		Time.timeScale = 0f;
		this.pauseMenu.SetActive(true);
		this.paused = true;
	}
	public void ResumeGame()
	{
		Time.timeScale = 1f;
		this.pauseMenu.SetActive(false);
		this.paused = false;
		MonoBehaviour.print("resuiming game");
	}
	public void Recover()
	{
		Time.timeScale = 1f;
		GameController.Instance.Recover();
		this.ResumeGame();
	}
	public void RestartGame()
	{
		Time.timeScale = 1f;
		this.pauseMenu.SetActive(false);
		this.paused = false;
		GameController.Instance.RestartGame();
	}
	public void Options()
	{
	}
	public void TogglePause()
	{
		if (!GameController.Instance.playing)
		{
			return;
		}
		if (!this.paused)
		{
			this.PauseGame();
			return;
		}
		this.ResumeGame();
	}
	public void Quit()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("Menu");
		this.paused = false;
	}
	public static Pause Instance;
	public GameObject pauseMenu;
}
