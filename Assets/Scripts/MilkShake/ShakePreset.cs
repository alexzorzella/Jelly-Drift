using UnityEngine;

namespace MilkShake {
    [CreateAssetMenu(fileName = "New Shake Preset", menuName = "MilkShake/Shake Preset")]
    public class ShakePreset : ScriptableObject, IShakeParameters {
        [Header("Shake Type")] [SerializeField]
        ShakeType shakeType;

        [Header("Shake Strength")] [SerializeField]
        float strength;

        [SerializeField] float roughness;

        [Header("Fade")] [SerializeField] float fadeIn;

        [SerializeField] float fadeOut;

        [Header("Shake Influence")] [SerializeField]
        Vector3 positionInfluence;

        [SerializeField] Vector3 rotationInfluence;

        public ShakeType ShakeType {
            get => shakeType;
            set => shakeType = value;
        }

        public float Strength {
            get => strength;
            set => strength = value;
        }

        public float Roughness {
            get => roughness;
            set => roughness = value;
        }

        public float FadeIn {
            get => fadeIn;
            set => fadeIn = value;
        }

        public float FadeOut {
            get => fadeOut;
            set => fadeOut = value;
        }

        public Vector3 PositionInfluence {
            get => positionInfluence;
            set => positionInfluence = value;
        }

        public Vector3 RotationInfluence {
            get => rotationInfluence;
            set => rotationInfluence = value;
        }
    }
}