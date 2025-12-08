using System;
using System.Linq;
using TMPro;
using UnityEngine;
public class MySetting : MonoBehaviour
{
	public TextMeshProUGUI[] options { get; set; }
	private void Awake()
	{
		this.options = (from r in base.GetComponentsInChildren<TextMeshProUGUI>()
		where r.tag != "Ignore"
		select r).ToArray<TextMeshProUGUI>();
	}
}
