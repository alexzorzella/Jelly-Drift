using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PPController : MonoBehaviour {
    public static PPController Instance;
    DepthOfField dof;
    MotionBlur motionBlur;

    PostProcessProfile profile;
    PostProcessVolume volume;

    void Awake() {
        Instance = this;
        volume = GetComponent<PostProcessVolume>();
        profile = volume.profile;
        motionBlur = profile.GetSetting<MotionBlur>();
        dof = profile.GetSetting<DepthOfField>();
    }

    void Start() {
        LoadSettings();
    }

    public void LoadSettings() {
        if (SaveState.Instance.graphics != 1) {
            volume.enabled = false;
            return;
        }

        volume.enabled = true;
        if (SaveState.Instance.motionBlur == 1) {
            motionBlur.enabled.value = true;
        }
        else {
            motionBlur.enabled.value = false;
        }

        if (SaveState.Instance.dof == 1) {
            dof.enabled.value = true;
            return;
        }

        dof.enabled.value = false;
    }
}