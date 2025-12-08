using System;
using UnityEngine;
public class SaveState : MonoBehaviour
{
	public int quality { get; set; }
	public int dof { get; set; }
	public int motionBlur { get; set; }
	public int cameraMode { get; set; }
	public int cameraShake { get; set; }
	public int muted { get; set; }
	public int volume { get; set; }
	public int music { get; set; }
	private void Awake()
	{
		if (SaveState.Instance != null && SaveState.Instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		SaveState.Instance = this;
	}
	private void Start()
	{
		if (!SaveState.Instance)
		{
			return;
		}
		this.LoadSettings();
	}
	private void LoadSettings()
	{
		this.graphics = SaveManager.Instance.state.graphics;
		this.quality = SaveManager.Instance.state.quality;
		this.motionBlur = SaveManager.Instance.state.motionBlur;
		this.dof = SaveManager.Instance.state.dof;
		this.cameraMode = SaveManager.Instance.state.cameraMode;
		this.cameraShake = SaveManager.Instance.state.cameraShake;
		this.muted = SaveManager.Instance.state.muted;
		this.volume = SaveManager.Instance.state.volume;
		this.music = SaveManager.Instance.state.music;
	}
	public int graphics { get; set; }
	public static SaveState Instance;
}
