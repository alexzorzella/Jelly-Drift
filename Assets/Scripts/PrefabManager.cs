using System;
using UnityEngine;
public class PrefabManager : MonoBehaviour
{
	private void Awake()
	{
		if (PrefabManager.Instance != null && PrefabManager.Instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		PrefabManager.Instance = this;
	}
	public static PrefabManager Instance;
	public GameObject[] cars;
	public GameObject splitUi;
	public GameObject crashParticles;
	public Material ghostMat;
}
