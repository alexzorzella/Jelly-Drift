using System;
using UnityEngine;
public class CarSkin : MonoBehaviour
{
	private void Start()
	{
	}
	public void SetSkin(int n)
	{
		if (this.skinsToChange.Length == 0)
		{
			return;
		}
		MonoBehaviour.print("n: " + n);
		int i = 0;
		while (i < this.skinsToChange[n].myArray.Length)
		{
			int num = this.skinsToChange[n].myArray[i++];
			int num2 = this.skinsToChange[n].myArray[i++];
			int num3 = this.skinsToChange[n].myArray[i++];
			Material[] array = this.renderers[num].materials;
			array[num2] = this.materials[num3];
			this.renderers[num].materials = array;
		}
	}
	public string GetSkinName(int n)
	{
		return this.materials[n].name;
	}
	private int currentSkin;
	public Renderer[] renderers;
	public Material[] materials;
	public CarSkin.SkinArray[] skinsToChange;
	[Serializable]
	public class SkinArray
	{
		public int[] myArray;
	}
}
