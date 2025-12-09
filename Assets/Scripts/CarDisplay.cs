using UnityEngine;

public class CarDisplay : MonoBehaviour {
    public static CarDisplay Instance;
    public GameObject currentCar;
    public Material black;
    CarSkin skin;
    public int nCars { get; set; }

    void Awake() {
        if (!(Instance != null) || !(Instance != this)) {
            Instance = this;
        }

        nCars = PrefabManager.Instance.cars.Length;
        SpawnCar(0);
    }

    void Start() {
    }

    public void SetSkin(int n) {
        // skin.SetSkin(n);
        // print("setting skin to: " + n);
    }

    public void Hide() {
        currentCar.gameObject.SetActive(false);
    }

    public void Show() {
        currentCar.gameObject.SetActive(true);
    }

    public void SpawnCar(int n) {
        if (currentCar != null) {
            Destroy(currentCar);
        }
        
        // currentCar = Instantiate(PrefabManager.Instance.cars[n], transform.position, transform.rotation);
        // currentCar.name = PrefabManager.Instance.cars[n].name;
        
        currentCar = ResourceLoader.InstantiateObject("Car", transform.position, transform.rotation);
        currentCar.GetComponent<Car>().Initialize(CarCatalogue.GetSelectedCarData());
        
        skin = currentCar.GetComponent<CarSkin>();
        Destroy(currentCar.GetComponent<PlayerInput>());
        Destroy(currentCar.GetComponent<CheckpointUser>());
        
        if (!SaveManager.Instance.state.carsUnlocked[n]) {
            foreach (var renderer in currentCar.GetComponentsInChildren<Renderer>()) {
                var array = new Material[renderer.materials.Length];
                for (var j = 0; j < array.Length; j++) {
                    array[j] = black;
                }

                renderer.materials = array;
            }
        }
    }
}