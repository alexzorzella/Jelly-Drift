using System;
using UnityEngine;
using UnityEngine.UI;
public class FirstButton : MonoBehaviour
{
	private void Awake()
	{
		this.btn = base.GetComponent<Button>();
	}
	public void SelectButton()
	{
		this.btn.Select();
	}
	private void Start()
	{
		this.btn.Select();
	}
	private Button btn;
}
