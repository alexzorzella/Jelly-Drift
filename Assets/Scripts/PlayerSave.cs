using System;
using UnityEngine;
public class PlayerSave
{
	public int graphics { get; set; } = 1;
	public int quality { get; set; } = 2;
	public int motionBlur { get; set; } = 1;
	public int dof { get; set; } = 1;
	public int cameraMode { get; set; }
	public int cameraShake { get; set; } = 1;
	public int muted { get; set; }
	public int volume { get; set; } = 3;
	public int music { get; set; } = 4;
	public PlayerSave()
	{
		this.lastSkin = new int[15];
		this.skins = new bool[this.carsUnlocked.Length][];
		for (int i = 0; i < this.skins.Length; i++)
		{
			if (i == 5)
			{
				this.skins[i] = new bool[2];
			}
			else if (i > 5)
			{
				this.skins[i] = new bool[1];
			}
			else
			{
				this.skins[i] = new bool[5];
			}
			this.skins[i][0] = true;
		}
		if (SystemInfo.deviceType == DeviceType.Handheld)
		{
			this.graphics = 0;
			this.quality = 0;
			this.motionBlur = 0;
			this.dof = 0;
		}
		this.daniTimes[0] = 42.11238f;
		this.daniTimes[1] = 51.41264f;
		this.daniTimes[2] = 76.41264f;
		this.daniTimes[3] = 79.27263f;
		this.daniTimes[4] = 114.1815f;
		this.mapsUnlocked[0] = true;
		this.carsUnlocked[0] = true;
		for (int j = 0; j < this.races.Length; j++)
		{
			this.races[j] = -1;
		}
	}
	public int GetLevel(int xp)
	{
		return Mathf.FloorToInt(PlayerSave.NthRoot((float)xp, this.y) * this.x);
	}
	public int GetLevel()
	{
		return Mathf.FloorToInt(PlayerSave.NthRoot((float)this.xp, this.y) * this.x);
	}
	public int XpForLevel(int level)
	{
		return (int)Mathf.Pow((float)level / this.x, this.y);
	}
	public float LevelProgress()
	{
		float num = (float)(this.xp - this.XpForLevel(this.GetLevel()));
		int num2 = this.XpForLevel(this.GetLevel() + 1) - this.XpForLevel(this.GetLevel());
		return num / (float)num2;
	}
	private static float NthRoot(float A, float N)
	{
		return Mathf.Pow(A, 1f / N);
	}
	public static int GetSkinPrice(int car, int skin)
	{
		if (car < 5)
		{
			return 500 * (skin - 2) + car * 200;
		}
		return 9999;
	}
	public float[] times = new float[100];
	public int[] races = new int[100];
	public float[] daniTimes = new float[100];
	public bool[][] skins;
	public bool[] carsUnlocked = new bool[15];
	public bool[] mapsUnlocked = new bool[15];
	public int xp;
	public int money;
	public int lastCar;
	public int[] lastSkin;
	public int lastMap;
	public int lastDifficulty;
	public int lastGhost;
	private float x = 0.07f;
	private float y = 1.55f;
}
