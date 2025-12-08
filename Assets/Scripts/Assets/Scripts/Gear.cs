using System;

namespace Assets.Scripts
{
	public class Gear
	{
		public static int LookupTorqueCurve(int rpm)
		{
			if (rpm > 6000)
			{
				return 280;
			}
			for (int i = 0; i < Gear.rpmTorque.Length; i++)
			{
				if (i >= Gear.rpmTorque.Length)
				{
					return Gear.rpmTorque[Gear.rpmTorque.Length].Item2;
				}
				float num = (float)Gear.rpmTorque[i].Item1;
				float num2 = (float)Gear.rpmTorque[i + 1].Item1;
				if ((float)rpm >= num && (float)rpm < num2)
				{
					float num3 = (float)Gear.rpmTorque[i].Item2;
					float num4 = (float)Gear.rpmTorque[i + 1].Item2 - num3;
					float num5 = 1f - (num2 - (float)rpm) / (num2 - num);
					return (int)(num3 + num4 * num5);
				}
			}
			return 290;
		}
		public static float g1 = 2.66f;
		public static float g2 = 1.78f;
		public static float g3 = 1.3f;
		public static float g4 = 1f;
		public static float g5 = 0.74f;
		public static float g6 = 0.5f;
		public static float gR = 2.9f;
		public static float x_d = 3.42f;
		public static ValueTuple<int, int>[] rpmTorque = new ValueTuple<int, int>[]
		{
			new ValueTuple<int, int>(1000, 290),
			new ValueTuple<int, int>(2000, 325),
			new ValueTuple<int, int>(3000, 335),
			new ValueTuple<int, int>(3500, 345),
			new ValueTuple<int, int>(4000, 350),
			new ValueTuple<int, int>(4500, 355),
			new ValueTuple<int, int>(5000, 347),
			new ValueTuple<int, int>(5400, 330),
			new ValueTuple<int, int>(5650, 300),
			new ValueTuple<int, int>(6000, 280)
		};
	}
}
