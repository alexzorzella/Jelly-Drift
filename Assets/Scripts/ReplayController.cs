using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class ReplayController : MonoBehaviour
{
	public int hz { get; set; } = 30;
	public Car car { get; set; }
	private void Awake()
	{
		ReplayController.Instance = this;
	}
	private void Start()
	{
		string path = Application.persistentDataPath + "/replays";
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		this.filePath = string.Concat(new object[]
		{
			Application.persistentDataPath,
			"/replays/pb",
			GameState.Instance.map,
			".txt"
		});
		this.replay = new List<ReplayController.ReplayFrame>();
		this.startTime = Time.time;
	}
	private void FixedUpdate()
	{
		if (!GameController.Instance || !this.car)
		{
			return;
		}
		this.replay.Add(new ReplayController.ReplayFrame(this.car.rb.position, this.car.rb.rotation.eulerAngles, this.car.steerAngle, Time.time));
	}
	public void Save()
	{
		MonoBehaviour.print("saving");
		StreamWriter streamWriter = StreamWriter.Null;
		try
		{
			streamWriter = new StreamWriter(this.filePath, false);
			streamWriter.WriteLine(GameState.Instance.car + ", " + GameState.Instance.skin);
			foreach (ReplayController.ReplayFrame replayFrame in this.replay)
			{
				streamWriter.WriteLine(string.Concat(new object[]
				{
					replayFrame.pos,
					", ",
					replayFrame.rot,
					", ",
					replayFrame.steerAngle,
					",",
					replayFrame.time
				}));
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
		}
		finally
		{
			streamWriter.Close();
		}
	}
	private void Update()
	{
	}
	private List<ReplayController.ReplayFrame> replay;
	private float startTime;
	private float endTime;
	private string filePath;
	public static ReplayController Instance;
	[Serializable]
	public class ReplayFrame
	{
		public ReplayFrame(Vector3 pos, Vector3 rot, float steerAngle, float time)
		{
			this.pos = pos;
			this.rot = rot;
			this.steerAngle = steerAngle;
			this.time = time;
		}
		public Vector3 pos;
		public Vector3 rot;
		public float steerAngle;
		public float time;
	}
}
