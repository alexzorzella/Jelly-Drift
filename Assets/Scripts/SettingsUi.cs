using UnityEngine;

public class SettingsUi : MonoBehaviour {
    public SettingCycle motionBlur;
    public SettingCycle graphics;
    public SettingCycle quality;
    public SettingCycle camMode;
    public SettingCycle camShake;
    public SettingCycle dof;
    public SliderSettingCycle volume;
    public SliderSettingCycle music;
    Color deselected = new(0f, 0f, 0f, 0.3f);
    Color selected = Color.white;

    void Start() {
        LoadAllSettings();
    }

    void LoadAllSettings() {
        LoadSetting(motionBlur, SaveState.Instance.motionBlur);
        LoadSetting(dof, SaveState.Instance.dof);
        LoadSetting(graphics, SaveState.Instance.graphics);
        LoadSetting(quality, SaveState.Instance.quality);
        LoadSetting(camMode, SaveState.Instance.cameraMode);
        LoadSetting(camShake, SaveState.Instance.cameraShake);
        LoadSettingSlider(volume, SaveState.Instance.volume);
        LoadSettingSlider(music, SaveState.Instance.musicVolume);
    }

    void LoadSetting(SettingCycle s, int n) {
        s.selected = n;
        s.UpdateOptions();
    }

    void LoadSettingSlider(SliderSettingCycle s, int f) {
        s.selected = f;
        s.UpdateOptions();
    }

    public void UpdateSettings() {
        MotionBlur(motionBlur.selected);
        DoF(dof.selected);
        Graphics(graphics.selected);
        Quality(quality.selected);
        CamMode(camMode.selected);
        CamShake(camShake.selected);
        Volume();
        Music();
    }

    public void MotionBlur(int n) {
        SaveManager.Instance.state.motionBlur = n;
        SaveManager.Instance.Save();
        SaveState.Instance.motionBlur = n;
        PPController.Instance.LoadSettings();
    }

    public void DoF(int n) {
        SaveManager.Instance.state.dof = n;
        SaveManager.Instance.Save();
        SaveState.Instance.dof = n;
        PPController.Instance.LoadSettings();
    }

    public void Graphics(int n) {
        SaveManager.Instance.state.graphics = n;
        SaveManager.Instance.Save();
        SaveState.Instance.graphics = n;
        PPController.Instance.LoadSettings();
    }

    public void Quality(int n) {
        SaveManager.Instance.state.quality = n;
        SaveManager.Instance.Save();
        SaveState.Instance.quality = n;
        QualitySettings.SetQualityLevel(n + Mathf.Clamp(2 * n - 1, 0, 10));
        if (CameraCulling.Instance) {
            CameraCulling.Instance.UpdateCulling();
        }
    }

    public void CamMode(int n) {
        SaveManager.Instance.state.cameraMode = n;
        SaveManager.Instance.Save();
        SaveState.Instance.cameraMode = n;
    }

    public void CamShake(int n) {
        SaveManager.Instance.state.cameraShake = n;
        SaveManager.Instance.Save();
        SaveState.Instance.cameraShake = n;
    }

    public void Volume() {
        SaveManager.Instance.state.volume = volume.selected;
        SaveManager.Instance.Save();
        SaveState.Instance.volume = volume.selected;
        AudioListener.volume = volume.selected / 10f;
    }

    public void Music() {
        SaveManager.Instance.state.music = music.selected;
        SaveManager.Instance.Save();
        SaveState.Instance.musicVolume = music.selected;
        MusicController.i.UpdateVolume(music.selected);
    }

    public void ResetSave() {
        SaveManager.Instance.NewSave();
        SaveManager.Instance.Save();
    }
}