using System;
using UnityEngine;
public class StaticManagers : MonoBehaviour
{
	private void Awake()
	{
		if (StaticManagers.Instance != null && StaticManagers.Instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		StaticManagers.Instance = this;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}
	public static StaticManagers Instance;
}
