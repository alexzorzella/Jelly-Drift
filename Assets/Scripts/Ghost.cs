using System;
using UnityEngine;
public class Ghost : MonoBehaviour
{
	private void Start()
	{
		this.ghost = PrefabManager.Instance.ghostMat;
		Renderer[] componentsInChildren = base.GetComponentsInChildren<MeshRenderer>();
		this.renderers = componentsInChildren;
		foreach (Renderer renderer in this.renderers)
		{
			Material[] materials = renderer.materials;
			for (int j = 0; j < materials.Length; j++)
			{
				Material material = new Material(this.ghost);
				material.color = materials[j].color;
				material.color = new Color(material.color.r, material.color.g, material.color.b, 0.2f);
				materials[j] = material;
			}
			renderer.materials = materials;
		}
	}
	private Renderer[] renderers;
	public Material ghost;
}
