using System.Collections.Generic;
using UnityEngine;

namespace MilkShake {
    [AddComponentMenu("MilkShake/Shaker")]
    public class Shaker : MonoBehaviour {
        public static List<Shaker> GlobalShakers = new();
        [SerializeField] bool addToGlobalShakers;
        readonly List<ShakeInstance> activeShakes = new();

        void Awake() {
            if (addToGlobalShakers) {
                GlobalShakers.Add(this);
            }
        }

        void Update() {
            if (SaveState.i.cameraShake == 0) {
                return;
            }

            var shakeResult = default(ShakeResult);
            for (var i = 0; i < activeShakes.Count; i++) {
                if (activeShakes[i].IsFinished) {
                    activeShakes.RemoveAt(i);
                    i--;
                }
                else {
                    shakeResult += activeShakes[i].UpdateShake(Time.deltaTime);
                }
            }

            transform.localPosition = shakeResult.PositionShake;
            transform.localEulerAngles = shakeResult.RotationShake;
        }

        void OnDestroy() {
            if (addToGlobalShakers) {
                GlobalShakers.Remove(this);
            }
        }

        public static ShakeInstance ShakeAll(IShakeParameters shakeData, int? seed = null) {
            var shakeInstance = new ShakeInstance(shakeData, seed);
            AddShakeAll(shakeInstance);
            return shakeInstance;
        }

        public static void ShakeAllSeparate(IShakeParameters shakeData, List<ShakeInstance> shakeInstances = null,
            int? seed = null) {
            if (shakeInstances != null) {
                shakeInstances.Clear();
            }

            for (var i = 0; i < GlobalShakers.Count; i++) {
                if (GlobalShakers[i].gameObject.activeInHierarchy) {
                    var shakeInstance = GlobalShakers[i].Shake(shakeData, seed);
                    if (shakeInstances != null && shakeInstance != null) {
                        shakeInstances.Add(shakeInstance);
                    }
                }
            }
        }

        public static void ShakeAllFromPoint(Vector3 point, float maxDistance, IShakeParameters shakeData,
            List<ShakeInstance> shakeInstances = null, int? seed = null) {
            if (shakeInstances != null) {
                shakeInstances.Clear();
            }

            for (var i = 0; i < GlobalShakers.Count; i++) {
                if (GlobalShakers[i].gameObject.activeInHierarchy) {
                    var shakeInstance = GlobalShakers[i].ShakeFromPoint(point, maxDistance, shakeData, seed);
                    if (shakeInstances != null && shakeInstance != null) {
                        shakeInstances.Add(shakeInstance);
                    }
                }
            }
        }

        public static void AddShakeAll(ShakeInstance shakeInstance) {
            for (var i = 0; i < GlobalShakers.Count; i++) {
                if (GlobalShakers[i].gameObject.activeInHierarchy) {
                    GlobalShakers[i].AddShake(shakeInstance);
                }
            }
        }

        public ShakeInstance Shake(IShakeParameters shakeData, int? seed = null) {
            var shakeInstance = new ShakeInstance(shakeData, seed);
            AddShake(shakeInstance);
            return shakeInstance;
        }

        public ShakeInstance ShakeFromPoint(Vector3 point, float maxDistance, IShakeParameters shakeData,
            int? seed = null) {
            var num = Vector3.Distance(transform.position, point);
            if (num < maxDistance) {
                var shakeInstance = new ShakeInstance(shakeData, seed);
                var num2 = 1f - Mathf.Clamp01(num / maxDistance);
                shakeInstance.StrengthScale = num2;
                shakeInstance.RoughnessScale = num2;
                AddShake(shakeInstance);
                return shakeInstance;
            }

            return null;
        }

        public void AddShake(ShakeInstance shakeInstance) {
            activeShakes.Add(shakeInstance);
        }
    }
}