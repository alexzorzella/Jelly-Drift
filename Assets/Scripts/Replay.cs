using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class Replay : MonoBehaviour
{
	private void Start()
	{
		if (GameState.Instance.gamemode != Gamemode.TimeTrial || GameState.Instance.ghost == GhostCycle.Ghost.Off)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		GhostCycle.Ghost ghost = GameState.Instance.ghost;
		this.replay = new List<ReplayController.ReplayFrame>();
		this.replayDeltaTime = 1f / (float)ReplayController.Instance.hz;
		string text = "pb";
		text += GameState.Instance.map;
		if (ghost == GhostCycle.Ghost.PB)
		{
			this.filePath = Application.persistentDataPath + "/replays/" + text + ".txt";
			if (!File.Exists(this.filePath))
			{
				UnityEngine.Object.Destroy(this);
				MonoBehaviour.print("couldnt find destroying");
				return;
			}
		}
		MonoBehaviour.print("path: " + this.filePath);
		if (ghost == GhostCycle.Ghost.Dani)
		{
			this.ReadTextAsset();
			return;
		}
		StreamReader streamReader = new StreamReader(this.filePath);
		string text2 = streamReader.ReadLine();
		text2 = text2.Replace("(", string.Empty).Replace(")", string.Empty);
		int[] array = Array.ConvertAll<string, int>(text2.Split(new char[]
		{
			','
		}), new Converter<string, int>(int.Parse));
		MonoBehaviour.print(string.Concat(new object[]
		{
			"lna: ",
			array[0],
			", ",
			array[1]
		}));
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(PrefabManager.Instance.cars[array[0]]);
		gameObject.GetComponent<CarSkin>().SetSkin(array[1]);
		while ((text2 = streamReader.ReadLine()) != null)
		{
			text2 = text2.Replace("(", string.Empty).Replace(")", string.Empty);
			float[] array2 = Array.ConvertAll<string, float>(text2.Split(new char[]
			{
				','
			}), new Converter<string, float>(float.Parse));
			ReplayController.ReplayFrame item = new ReplayController.ReplayFrame(new Vector3(array2[0], array2[1], array2[2]), new Vector3(array2[3], array2[4], array2[5]), array2[6], array2[7]);
			this.replay.Add(item);
		}
		this.rb = gameObject.GetComponent<Rigidbody>();
		this.rb.isKinematic = true;
		UnityEngine.Object.Destroy(gameObject.GetComponentInChildren<Collider>());
		UnityEngine.Object.Destroy(gameObject.GetComponent<PlayerInput>());
		ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].gameObject.SetActive(false);
		}
		Suspension[] componentsInChildren2 = gameObject.GetComponentsInChildren<Suspension>();
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			componentsInChildren2[i].showFx = false;
		}
		AudioSource[] componentsInChildren3 = gameObject.GetComponentsInChildren<AudioSource>();
		for (int i = 0; i < componentsInChildren3.Length; i++)
		{
			componentsInChildren3[i].enabled = false;
		}
		gameObject.AddComponent<Ghost>();
	}
	private void ReadTextAsset()
	{
		TextAsset textAsset = this.daniTimes[GameState.Instance.map];
		if (!textAsset)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		string[] array = textAsset.text.Split(new string[]
		{
			Environment.NewLine
		}, StringSplitOptions.None);
		int i = 0;
		int[] array2 = Array.ConvertAll<string, int>(array[i++].Replace("(", string.Empty).Replace(")", string.Empty).Split(new char[]
		{
			','
		}), new Converter<string, int>(int.Parse));
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(PrefabManager.Instance.cars[array2[0]]);
		gameObject.GetComponent<CarSkin>().SetSkin(array2[1]);
		while (i < array.Length - 1)
		{
			float[] array3 = Array.ConvertAll<string, float>(array[i++].Replace("(", string.Empty).Replace(")", string.Empty).Split(new char[]
			{
				','
			}), new Converter<string, float>(float.Parse));
			ReplayController.ReplayFrame item = new ReplayController.ReplayFrame(new Vector3(array3[0], array3[1], array3[2]), new Vector3(array3[3], array3[4], array3[5]), array3[6], array3[7]);
			this.replay.Add(item);
		}
		this.rb = gameObject.GetComponent<Rigidbody>();
		this.rb.isKinematic = true;
		UnityEngine.Object.Destroy(gameObject.GetComponentInChildren<Collider>());
		UnityEngine.Object.Destroy(gameObject.GetComponent<PlayerInput>());
		ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			componentsInChildren[j].gameObject.SetActive(false);
		}
		Suspension[] componentsInChildren2 = gameObject.GetComponentsInChildren<Suspension>();
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			componentsInChildren2[j].showFx = false;
		}
		AudioSource[] componentsInChildren3 = gameObject.GetComponentsInChildren<AudioSource>();
		for (int j = 0; j < componentsInChildren3.Length; j++)
		{
			componentsInChildren3[j].enabled = false;
		}
		gameObject.AddComponent<Ghost>();
	}
	private void FixedUpdate()
	{
		if (this.currentFrame >= this.replay.Count - 1)
		{
			return;
		}
		this.rb.MovePosition(Vector3.Lerp(this.rb.transform.position, this.replay[this.currentFrame].pos, Time.deltaTime * 30f));
		this.rb.MoveRotation(Quaternion.Lerp(this.rb.rotation, Quaternion.Euler(this.replay[this.currentFrame].rot), Time.deltaTime * 30f));
		this.currentFrame++;
	}
	private List<ReplayController.ReplayFrame> replay;
	private float replayDeltaTime;
	private string filePath;
	public TextAsset[] daniTimes;
	private int currentFrame;
	private Vector3 nextPos;
	private Rigidbody rb;
}
