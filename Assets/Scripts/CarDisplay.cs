using System;
using UnityEngine;
public class CarDisplay : MonoBehaviour
{
	public int nCars { get; set; }
	private void Awake()
	{
		if (!(CarDisplay.Instance != null) || !(CarDisplay.Instance != this))
		{
			CarDisplay.Instance = this;
		}
		this.nCars = PrefabManager.Instance.cars.Length;
		this.SpawnCar(0);
	}
	private void Start()
	{
	}
	public void SetSkin(int n)
	{
		this.skin.SetSkin(n);
		MonoBehaviour.print("setting skin to: " + n);
	}
	public void Hide()
	{
		this.currentCar.gameObject.SetActive(false);
	}
	public void Show()
	{
		this.currentCar.gameObject.SetActive(true);
	}
	public void SpawnCar(int n)
	{
		UnityEngine.Object.Destroy(this.currentCar);
		this.currentCar = UnityEngine.Object.Instantiate<GameObject>(PrefabManager.Instance.cars[n], base.transform.position, base.transform.rotation);
		this.currentCar.name = PrefabManager.Instance.cars[n].name;
		this.skin = this.currentCar.GetComponent<CarSkin>();
		UnityEngine.Object.Destroy(this.currentCar.GetComponent<PlayerInput>());
		UnityEngine.Object.Destroy(this.currentCar.GetComponent<CheckpointUser>());
		if (!SaveManager.Instance.state.carsUnlocked[n])
		{
			foreach (Renderer renderer in this.currentCar.GetComponentsInChildren<Renderer>())
			{
				Material[] array = new Material[renderer.materials.Length];
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = this.black;
				}
				renderer.materials = array;
			}
		}
	}
	public GameObject currentCar;
	private CarSkin skin;
	public static CarDisplay Instance;
	public Material black;
}
