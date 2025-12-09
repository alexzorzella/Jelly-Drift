using UnityEngine;

public class ResourceLoader : MonoBehaviour {
	static ResourceLoader _i;

	void Start() {
		DontDestroyOnLoad(gameObject);
	}
	
	public static ResourceLoader i {
		get {
			if (_i == null) {
				ResourceLoader x = Resources.Load<ResourceLoader>("ResourceLoader");

				_i = Instantiate(x);
			}
			return _i;
		}
	}

	public RuntimeAnimatorController LoadAnimatorController(string animatorName) {
		RuntimeAnimatorController result = Resources.Load<RuntimeAnimatorController>(animatorName);

		if (result == null) {
			Debug.Log($"Controlador de animação de '{animatorName}' não existe.");
			return null;
		}

		return result;
	}
	
	public static GameObject LoadObject(string objectName) {
		GameObject result = Resources.Load<GameObject>(objectName);
		return result;
	}

	public static GameObject InstantiateObject(string objectName) {
		GameObject result = InstantiateObject(objectName, Vector3.zero, Quaternion.identity);
		return result;
	}

	public static GameObject InstantiateObject(string objectName, Vector3 position, Quaternion rotation) {
		GameObject loadedObject = LoadObject(objectName);

		if (loadedObject == null) {
			Debug.LogError($"Couldn't find object in Resources named '{objectName}'.");
			return null;
		}
		
		return Instantiate(loadedObject, position, rotation);
	}
}