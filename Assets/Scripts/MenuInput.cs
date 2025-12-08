using System;
using UnityEngine;
public class MenuInput : MonoBehaviour
{
	private void Awake()
	{
		if (MenuInput.Instance)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		MenuInput.Instance = this;
	}
	private void Update()
	{
		this.PlayerInput();
	}
	private void PlayerInput()
	{
	}
	public bool horizontalDone;
	public bool verticalDone;
	public int horizontal;
	public int vertical;
	public bool select;
	public static MenuInput Instance;
	public int wat;
}
