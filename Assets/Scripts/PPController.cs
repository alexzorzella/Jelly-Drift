using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class PPController : MonoBehaviour
{
	private void Awake()
	{
		PPController.Instance = this;
		this.volume = base.GetComponent<PostProcessVolume>();
		this.profile = this.volume.profile;
		this.motionBlur = this.profile.GetSetting<MotionBlur>();
		this.dof = this.profile.GetSetting<DepthOfField>();
	}
	private void Start()
	{
		this.LoadSettings();
	}
	public void LoadSettings()
	{
		if (SaveState.Instance.graphics != 1)
		{
			this.volume.enabled = false;
			return;
		}
		this.volume.enabled = true;
		if (SaveState.Instance.motionBlur == 1)
		{
			this.motionBlur.enabled.value = true;
		}
		else
		{
			this.motionBlur.enabled.value = false;
		}
		if (SaveState.Instance.dof == 1)
		{
			this.dof.enabled.value = true;
			return;
		}
		this.dof.enabled.value = false;
	}
	private PostProcessProfile profile;
	private PostProcessVolume volume;
	private MotionBlur motionBlur;
	private DepthOfField dof;
	public static PPController Instance;
}
