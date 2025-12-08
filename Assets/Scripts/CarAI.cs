using System;
using UnityEngine;
public class CarAI : MonoBehaviour
{
	private void Start()
	{
		this.difficulty = (int)GameState.Instance.difficulty;
		MonoBehaviour.print(string.Concat(new object[]
		{
			"d: ",
			GameState.Instance.difficulty,
			", a: ",
			this.difficulty
		}));
		this.car.engineForce = (float)this.difficultyConfig[this.difficulty];
		base.InvokeRepeating("AdjustSpeed", 0.5f, 0.5f);
		if (GameController.Instance.finalCheckpoint != 0)
		{
			base.GetComponent<CheckpointUser>().ForceCheckpoint(0);
		}
	}
	public void Recover()
	{
		this.car.rb.linearVelocity = Vector3.zero;
		base.transform.position = this.nodes[this.FindClosestNode(3, base.transform)].position;
		int num = this.currentNode % this.nodes.Length;
		int num2 = (num + 1) % this.nodes.Length;
		Vector3 normalized = (this.nodes[num2].position - this.nodes[num].position).normalized;
		base.transform.rotation = Quaternion.LookRotation(normalized);
	}
	private void CheckRecover()
	{
		if (!GameController.Instance.playing)
		{
			return;
		}
		if (base.transform.position.y < (float)this.respawnHeight)
		{
			this.Recover();
		}
		if (base.IsInvoking("Recover"))
		{
			if (this.car.speed > 3f)
			{
				base.CancelInvoke("Recover");
			}
			return;
		}
		if (this.car.speed < 3f)
		{
			base.Invoke("Recover", this.recoverTime);
			return;
		}
		base.CancelInvoke("Recover");
	}
	private void Update()
	{
		if (!GameController.Instance.playing || !this.path)
		{
			return;
		}
		this.NewAI();
		this.CheckRecover();
	}
	public void SetPath(Transform p)
	{
		this.path = p;
		this.nodes = this.path.GetComponentsInChildren<Transform>();
		this.car = base.GetComponent<Car>();
		this.currentNode = this.FindClosestNode(this.nodes.Length, base.transform);
	}
	private int FindNextTurn()
	{
		for (int i = this.currentNode; i < this.currentNode + this.turnLookAhead; i++)
		{
			int num = i % this.nodes.Length;
			int num2 = (num + 1) % this.nodes.Length;
			int num3 = (num2 + 1) % this.nodes.Length;
			Vector3 vector = this.nodes[num2].position - this.nodes[num].position;
			Vector3 vector2 = this.nodes[num3].position - this.nodes[num2].position;
			float f = Vector3.SignedAngle(vector.normalized, vector2.normalized, Vector3.up);
			if (Mathf.Abs(f) > 20f)
			{
				this.turnDir = (int)Mathf.Sign(f);
				this.nextTurnLength = this.FindNextStraight(num2);
				return num2;
			}
		}
		return -1;
	}
	private int FindNextStraight(int startNode)
	{
		for (int i = startNode; i < startNode + this.turnLookAhead; i++)
		{
			int num = i % this.nodes.Length;
			int num2 = (num + 1) % this.nodes.Length;
			int num3 = (num2 + 1) % this.nodes.Length;
			Vector3 from = this.nodes[num2].position - this.nodes[num].position;
			Vector3 to = this.nodes[num3].position - this.nodes[num2].position;
			if (Mathf.Abs(Vector3.SignedAngle(from, to, Vector3.up)) < 15f)
			{
				return num2 - startNode;
			}
		}
		return 3;
	}
	private void NewAI()
	{
		int num = this.FindClosestNode(this.maxLookAhead, base.transform);
		this.currentNode = num;
		int num2 = (num + 1) % this.nodes.Length;
		if (this.currentNode > this.nextTurnStart + this.nextTurnLength)
		{
			this.nextTurnStart = this.FindNextTurn();
		}
		if (num2 < this.nextTurnStart)
		{
			this.xOffset = 0.13f * (float)this.turnDir;
		}
		else if (num2 >= this.nextTurnStart && num2 < this.nextTurnStart + this.nextTurnLength)
		{
			this.xOffset = -0.13f * (float)this.turnDir;
		}
		else
		{
			this.xOffset = 0f;
		}
		Vector3 b = Vector3.Cross(this.nodes[num2].position - this.nodes[num].position, Vector3.up) * this.xOffset;
		Vector3 vector = this.nodes[num2].position + b - base.transform.position;
		vector = base.transform.InverseTransformDirection(vector);
		float num3 = 1f + Mathf.Clamp(this.car.speed * 0.01f * this.speedSteerMultiplier, 0f, 1f);
		this.car.steering = Mathf.Clamp(vector.x * 0.05f * num3, -1f, 1f) * num3;
		this.car.throttle = 1f;
		this.car.throttle = 1f - Mathf.Abs(this.car.steering * Mathf.Clamp(this.car.speed - (float)this.maxTurnSpeed, 0f, 100f) * 0.06f);
	}
	private void AdjustSpeed()
	{
		float num = (float)this.FindClosestNode(this.nodes.Length, base.transform) / (float)this.nodes.Length;
		float num2 = (float)this.FindClosestNode(this.nodes.Length, GameController.Instance.currentCar.transform) / (float)this.nodes.Length;
		float num3 = num - num2;
		if (num3 < 0f)
		{
			num3 *= this.speedupM;
		}
		if (num3 > 0f)
		{
			num3 *= this.slowdownM;
		}
		float num4 = (float)this.difficultyConfig[this.difficulty] - Mathf.Clamp(num3 * 1000f * this.speedAdjustMultiplier, -8000f, 4000f);
		num4 = Mathf.Clamp(num4, 1000f, 8000f);
		this.car.engineForce = num4;
	}
	private int FindClosestNode(int maxLook, Transform target)
	{
		float num = float.PositiveInfinity;
		int result = 0;
		for (int i = 0; i < maxLook; i++)
		{
			int num2 = (this.currentNode + i) % this.nodes.Length;
			float num3 = Vector3.Distance(target.position, this.nodes[num2].position);
			if (num3 < num)
			{
				num = num3;
				result = num2;
			}
		}
		return result;
	}
	[ExecuteInEditMode]
	public Transform path;
	public Transform[] nodes;
	private Car car;
	private LineRenderer line;
	private float roadWidth = 0.4f;
	private float maxOffset = 0.36f;
	private int lookAhead = 4;
	private int maxLookAhead = 6;
	private int currentDriftNode;
	public int respawnHeight;
	private int difficulty;
	public int[] difficultyConfig;
	private float recoverTime = 1.5f;
	private int turnLookAhead = 6;
	private int turnDir;
	private int nextTurnStart;
	private int nextTurnLength;
	public float xOffset;
	public float speedSteerMultiplier = 1f;
	public float steerMultiplier = 1f;
	public int maxTurnSpeed = 50;
	private float speedAdjustMultiplier = 5f;
	private float speedupM = 15f;
	private float slowdownM = 5f;
	private int currentNode;
}
