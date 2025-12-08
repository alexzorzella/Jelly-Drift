using System;
using UnityEngine;
public class CollisionSfx : MonoBehaviour
{
	private void OnCollisionEnter(Collision other)
	{
		if (!this.crashAudio || !this.ready)
		{
			return;
		}
		if (other.relativeVelocity.magnitude < 6f)
		{
			return;
		}
		if (other.contacts.Length != 0)
		{
			Quaternion rotation = Quaternion.LookRotation(other.contacts[0].normal);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(PrefabManager.Instance.crashParticles, other.contacts[0].point, rotation);
			Renderer component = other.gameObject.GetComponent<Renderer>();
			if (component)
			{
				Material material = component.materials[0];
				gameObject.GetComponent<ParticleSystem>().GetComponent<Renderer>().material = material;
			}
		}
		this.crashAudio.Randomize();
		this.ready = false;
		base.Invoke("GetReady", 0.5f);
	}
	private void GetReady()
	{
		this.ready = true;
	}
	public RandomSfx crashAudio;
	private bool ready = true;
}
