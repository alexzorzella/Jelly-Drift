using System;
using UnityEngine;
public class ShowStats : MonoBehaviour
{
	private void OnEnable()
	{
		if (MenuStats.Instance)
		{
			MenuStats.Instance.gameObject.SetActive(this.show);
		}
	}
	public bool show;
}
