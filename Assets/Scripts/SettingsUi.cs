using System;
using UnityEngine;
public class SettingsUi : MonoBehaviour
{
	private void Start()
	{
		this.LoadAllSettings();
	}
	private void LoadAllSettings()
	{
		this.LoadSetting(this.motionBlur, SaveState.Instance.motionBlur);
		this.LoadSetting(this.dof, SaveState.Instance.dof);
		this.LoadSetting(this.graphics, SaveState.Instance.graphics);
		this.LoadSetting(this.quality, SaveState.Instance.quality);
		this.LoadSetting(this.camMode, SaveState.Instance.cameraMode);
		this.LoadSetting(this.camShake, SaveState.Instance.cameraShake);
		this.LoadSettingSlider(this.volume, SaveState.Instance.volume);
		this.LoadSettingSlider(this.music, SaveState.Instance.music);
	}
	private void LoadSetting(SettingCycle s, int n)
	{
		s.selected = n;
		s.UpdateOptions();
	}
	private void LoadSettingSlider(SliderSettingCycle s, int f)
	{
		s.selected = f;
		s.UpdateOptions();
	}
	public void UpdateSettings()
	{
		this.MotionBlur(this.motionBlur.selected);
		this.DoF(this.dof.selected);
		this.Graphics(this.graphics.selected);
		this.Quality(this.quality.selected);
		this.CamMode(this.camMode.selected);
		this.CamShake(this.camShake.selected);
		this.Volume();
		this.Music();
	}
	public void MotionBlur(int n)
	{
		SaveManager.Instance.state.motionBlur = n;
		SaveManager.Instance.Save();
		SaveState.Instance.motionBlur = n;
		PPController.Instance.LoadSettings();
	}
	public void DoF(int n)
	{
		SaveManager.Instance.state.dof = n;
		SaveManager.Instance.Save();
		SaveState.Instance.dof = n;
		PPController.Instance.LoadSettings();
	}
	public void Graphics(int n)
	{
		SaveManager.Instance.state.graphics = n;
		SaveManager.Instance.Save();
		SaveState.Instance.graphics = n;
		PPController.Instance.LoadSettings();
	}
	public void Quality(int n)
	{
		SaveManager.Instance.state.quality = n;
		SaveManager.Instance.Save();
		SaveState.Instance.quality = n;
		QualitySettings.SetQualityLevel(n + Mathf.Clamp(2 * n - 1, 0, 10));
		if (CameraCulling.Instance)
		{
			CameraCulling.Instance.UpdateCulling();
		}
	}
	public void CamMode(int n)
	{
		SaveManager.Instance.state.cameraMode = n;
		SaveManager.Instance.Save();
		SaveState.Instance.cameraMode = n;
	}
	public void CamShake(int n)
	{
		SaveManager.Instance.state.cameraShake = n;
		SaveManager.Instance.Save();
		SaveState.Instance.cameraShake = n;
	}
	public void Volume()
	{
		SaveManager.Instance.state.volume = this.volume.selected;
		SaveManager.Instance.Save();
		SaveState.Instance.volume = this.volume.selected;
		AudioListener.volume = (float)this.volume.selected / 10f;
	}
	public void Music()
	{
		SaveManager.Instance.state.music = this.music.selected;
		SaveManager.Instance.Save();
		SaveState.Instance.music = this.music.selected;
		MusicController.Instance.UpdateMusic((float)this.music.selected);
	}
	public void ResetSave()
	{
		SaveManager.Instance.NewSave();
		SaveManager.Instance.Save();
	}
	public SettingCycle motionBlur;
	public SettingCycle graphics;
	public SettingCycle quality;
	public SettingCycle camMode;
	public SettingCycle camShake;
	public SettingCycle dof;
	public SliderSettingCycle volume;
	public SliderSettingCycle music;
	private Color selected = Color.white;
	private Color deselected = new Color(0f, 0f, 0f, 0.3f);
}
