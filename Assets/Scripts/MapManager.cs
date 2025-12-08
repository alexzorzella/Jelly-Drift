using System;
using UnityEngine;
public class MapManager : MonoBehaviour
{
	private void Awake()
	{
		if (MapManager.Instance != null && MapManager.Instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		MapManager.Instance = this;
	}
	public int GetStars(int map, float time)
	{
		int result = 0;
		if (time <= this.maps[map].times[2])
		{
			result = 3;
		}
		else if (time <= this.maps[map].times[1])
		{
			result = 2;
		}
		else if (time <= this.maps[map].times[0])
		{
			result = 1;
		}
		if (time <= 0f)
		{
			result = 0;
		}
		return result;
	}
	public MapManager.MapInformation[] maps;
	public static MapManager Instance;
	[Serializable]
	public class MapInformation
	{
		public int index;
		public string name;
		public Sprite image;
		public Color themeColor;
		public float[] times;
	}
}
