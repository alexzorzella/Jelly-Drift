using System;
using UnityEngine;
public class UIManager : MonoBehaviour
{
	private void Awake()
	{
		UIManager.Instance = this;
	}
	public Transform splitPos;
	public static UIManager Instance;
}
