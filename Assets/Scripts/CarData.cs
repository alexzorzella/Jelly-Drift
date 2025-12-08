public class CarData {
    readonly string name;
    
    readonly float mass;
    readonly float linearDamping;
    readonly float angularDamping;
    
    // Suspension Variables (Customizable)

    readonly float suspensionLength;
    readonly float restHeight;
    readonly float suspensionForce;
    readonly float suspensionDamping;

    // Car Specs (Customizable)
    
    readonly float engineForce = 5000f;
    readonly float steerForce = 1f;
    readonly float antiRoll = 5000f;
    readonly float stability;

    // Drift Specs (Customizable)
    
    readonly float driftMultiplier = 1f;
    readonly float driftThreshold = 0.5f;

    // Audio

    readonly string accelerateSoundName;
    readonly string decelerateSoundName;
    
    CarData(
        string name,
        float mass,
        float linearDamping,
        float angularDamping,
        float suspensionLength,
        float restHeight,
        float suspensionForce,
        float suspensionDamping,
        float engineForce,
        float steerForce,
        float antiRoll,
        float stability,
        float driftMultiplier,
        float driftThreshold,
        string accelerateSoundName,
        string decelerateSoundName) {
        this.name = name;
        
        this.mass = mass;
        this.linearDamping = linearDamping;
        this.angularDamping = angularDamping;
        
        this.suspensionLength = suspensionLength;
        this.restHeight = restHeight;
        this.suspensionForce = suspensionForce;
        
        this.suspensionDamping = suspensionDamping;
        this.engineForce = engineForce;
        this.steerForce = steerForce;
        this.antiRoll = antiRoll;
        this.stability = stability;
        
        this.driftMultiplier = driftMultiplier;
        this.driftThreshold = driftThreshold;
        
        this.accelerateSoundName = accelerateSoundName;
        this.decelerateSoundName = decelerateSoundName;
    }

    public class Builder {
        readonly string name;
        
        float mass;
        float linearDamping;
        float angularDamping;
        
        // Suspension Variables

        float suspensionLength;
        float restHeight;
        float suspensionForce;
        float suspensionDamping;

        // Car Specs
    
        float engineForce = 5000f;
        float steerForce = 1f;
        float antiRoll = 5000f;
        float stability;

        // Drift Specs
    
        float driftMultiplier = 1f;
        float driftThreshold = 0.5f;

        // Audio

        string accelerateSoundName;
        string decelerateSoundName;
        
        public Builder(string name) {
            this.name = name;
        }

        public Builder WithPhysicsSpecs(float mass, float linearDamping, float angularDamping) {
            this.mass = mass;
            this.linearDamping = linearDamping;
            this.angularDamping = angularDamping;
            return this;
        }
        
        public Builder WithSuspensionSpecs(
            float suspensionLength,
            float restHeight,
            float suspensionForce,
            float suspensionDamping) {
            this.suspensionLength = suspensionLength;
            this.restHeight = restHeight;
            this.suspensionForce = suspensionForce;
            this.suspensionDamping = suspensionDamping;
            
            return this;
        }

        public Builder WithCarSpecs(
            float engineForce,
            float steerForce,
            float antiRoll,
            float stability) {
            this.engineForce = engineForce;
            this.steerForce = steerForce;
            this.antiRoll = antiRoll;
            this.stability = stability;
            
            return this;
        }

        public Builder WithDriftSpecs(
            float driftMultiplier,
            float driftThreshold) {
            this.driftMultiplier = driftMultiplier;
            this.driftThreshold = driftThreshold;

            return this;
        }

        public Builder WithAudio(
            string accelerateSoundName,
            string decelerateSoundName) {
            this.accelerateSoundName = accelerateSoundName;
            this.decelerateSoundName = decelerateSoundName;

            return this;
        }
        
        public CarData Build() {
            return new CarData(
                name,
                mass,
                linearDamping,
                angularDamping,
                suspensionLength, 
                restHeight , 
                suspensionForce, 
                suspensionDamping, 
                engineForce, steerForce, 
                antiRoll, 
                stability, 
                driftMultiplier, 
                driftThreshold,
                accelerateSoundName,
                decelerateSoundName);
        }
    }
    
    // Constants
    
    public const float brakeForce = 3000f;
    public const float dragForce = 3.5f;
    public const float rollFriction = 105f;
    public const float yawGripMultiplier = 0.15f;
    public const float yawGripThreshold = 0.6f;
    
    public string GetName() { return name; }
    public float GetMass() { return mass; }
    public float GetLinearDamping() { return linearDamping; }
    public float GetAngularDamping() { return angularDamping; }
    public float GetSuspensionLength() { return suspensionLength; }
    public float GetRestHeight() { return restHeight; }
    public float GetSuspensionForce() { return suspensionForce; }
    public float GetSuspensionDamping() { return suspensionDamping; }
    public float GetEngineForce() { return engineForce; }
    public float GetSteerForce() { return steerForce; }
    public float GetAntiRoll() { return antiRoll; }
    public float GetStability() { return stability; }
    public float GetDriftMultiplier() { return driftMultiplier; }
    public float GetDriftThreshold() { return driftThreshold; }
    public string GetAccelerateSoundName() { return accelerateSoundName; }
    public string GetDecelerateSoundName() { return decelerateSoundName; }
}