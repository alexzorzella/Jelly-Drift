using System.Collections.Generic;

public static class CarCatalogue {
    public static readonly List<CarData> cars = new() {
        new CarData.Builder("VF120").Build(),
        new CarData.Builder("V47").
            WithCarSpecs(3070, stability: 0.5F).
            WithDriftSpecs(1.37F).Build(),
        new CarData.Builder("TZ9").
            WithSuspensionSpecs(0.37F).
            WithCarSpecs(3150, stability: 0.1F).
            WithDriftSpecs(1.3F).Build(),
        new CarData.Builder("TZ9 Grand Turismo").
            WithPhysicsSpecs(1400, 0.6F, 1).
            WithSuspensionSpecs(0.37F).
            WithDriftSpecs(1.3F, 1F).
            WithCarSpecs(0.37F, 1, 6000, 0).Build(),
        new CarData.Builder("Truck").
            WithSuspensionSpecs(0.45F).
            WithCarSpecs(stability: 0).
            WithDriftSpecs(1.2F).Build(),
        new CarData.Builder("FD8").
            WithCarSpecs(3030, stability: 0.7F).
            WithDriftSpecs(1.4F).Build(),
        new CarData.Builder("FD8 Grand Turismo").
            WithPhysicsSpecs(1400).
            WithSuspensionSpecs(0.3F, 0.25F, 20000, 1600).
            WithCarSpecs(3030, antiRoll: 8000).
            WithDriftSpecs(1.4F, 1F).Build(),
        new CarData.Builder("T54").
            WithCarSpecs(2850, stability: 0.9F).
            WithDriftSpecs(1, 0.45F).Build(),
        new CarData.Builder("T54 Grand Turismo").
            WithPhysicsSpecs(1400).
            WithCarSpecs(2850, stability: 0).
            WithDriftSpecs(1, 1).Build(),
        new CarData.Builder("S14").
            WithPhysicsSpecs(1350, 0, 0.4F).
            WithCarSpecs(antiRoll: 8000, stability: 0).
            WithDriftSpecs(1.2F, 0.8F).Build(),
        new CarData.Builder("Tofu Machine").
            WithCarSpecs(3050, stability: 0.25F).
            WithDriftSpecs(1.43F).Build(),
        new CarData.Builder("Banana").
            WithSuspensionSpecs(0.55F).
            WithCarSpecs(3000, 1, 5000, 0).
            WithDriftSpecs(1.2F, 0.4F).Build(),
        new CarData.Builder("O").
            WithPhysicsSpecs(1450, 0, 1.5F).
            WithSuspensionSpecs(restHeight: 0.2F).
            WithCarSpecs(3200, antiRoll: 7000, stability: 0).
            WithDriftSpecs(1.4F, 1).Build()
    };
}