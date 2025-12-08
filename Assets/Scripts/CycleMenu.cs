using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class CycleMenu : MonoBehaviour
{
	private void Awake()
	{
		this.selected = this.startSelect;
		this.cycles = new List<ItemCycle>();
		this.cycleText = new List<TextMeshProUGUI>();
		for (int i = 0; i < base.transform.childCount; i++)
		{
			ItemCycle component = base.transform.GetChild(i).GetComponent<ItemCycle>();
			if (component)
			{
				this.cycles.Add(component);
				TextMeshProUGUI componentInChildren = component.GetComponentInChildren<TextMeshProUGUI>();
				this.cycleText.Add(componentInChildren);
				componentInChildren.color = Color.white;
			}
		}
		this.cycleText[this.selected].color = Color.black;
	}
	private void Start()
	{
		SaveManager.Instance.state.skins[5][1] = true;
	}
	private void UpdateSelected()
	{
		foreach (TextMeshProUGUI textMeshProUGUI in this.cycleText)
		{
			textMeshProUGUI.color = Color.white;
			if (this.correspondingText.Length != 0 && !this.correspondingText[this.selected].gameObject.CompareTag("Ignore"))
			{
				this.correspondingText[this.selected].color = Color.white;
			}
		}
		this.cycleText[this.selected].color = Color.black;
		if (this.correspondingText.Length != 0 && !this.correspondingText[this.selected].gameObject.CompareTag("Ignore"))
		{
			this.correspondingText[this.selected].color = Color.black;
		}
	}
	private void OnEnable()
	{
		this.selected = this.startSelect;
		this.horizontalDone = true;
		this.verticalDone = true;
		this.UpdateSelected();
	}
	private void Update()
	{
		this.PlayerInput();
	}
	private void PlayerInput()
	{
		if (UnlockManager.Instance && UnlockManager.Instance.gameObject.activeInHierarchy)
		{
			return;
		}
		int num = (int)Input.GetAxisRaw("HorizontalMenu");
		int num2 = (int)(-(int)Input.GetAxisRaw("VerticalMenu"));
		bool buttonDown = Input.GetButtonDown("Submit");
		bool buttonDown2 = Input.GetButtonDown("Cancel");
		if ((num != 0 && !this.horizontalDone) || buttonDown)
		{
			if (this.cycles[this.selected].activeCycle)
			{
				this.cycles[this.selected].Cycle(num);
				SoundManager.Instance.PlayCycle();
			}
			else
			{
				SoundManager.Instance.PlayError();
			}
		}
		if (num2 != 0 && !this.verticalDone)
		{
			this.cycleText[this.selected].color = Color.white;
			if (this.correspondingText.Length != 0 && !this.correspondingText[this.selected].gameObject.CompareTag("Ignore"))
			{
				this.correspondingText[this.selected].color = Color.white;
			}
			this.selected += num2;
			if (this.selected >= this.cycles.Count)
			{
				this.selected = 0;
			}
			else if (this.selected < 0)
			{
				this.selected = this.cycles.Count - 1;
			}
			this.cycleText[this.selected].color = Color.black;
			if (this.correspondingText.Length != 0 && !this.correspondingText[this.selected].gameObject.CompareTag("Ignore"))
			{
				this.correspondingText[this.selected].color = Color.black;
			}
			SoundManager.Instance.PlayMenuNavigate();
		}
		if (buttonDown2)
		{
			this.cycles[this.backBtn].Cycle(1);
			SoundManager.Instance.PlayCycle();
		}
		this.horizontalDone = (num != 0);
		this.verticalDone = (num2 != 0);
	}
	private List<ItemCycle> cycles;
	private List<TextMeshProUGUI> cycleText;
	public TextMeshProUGUI[] correspondingText;
	private int selected;
	public int startSelect;
	public int backBtn;
	private bool horizontalDone;
	private bool verticalDone;
}
