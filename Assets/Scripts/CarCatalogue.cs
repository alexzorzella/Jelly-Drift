using System.Collections.Generic;

public static class CarCatalogue {
    public static readonly List<CarData> cars = new() {
        new CarData.Builder("VF120").Build(),
        new CarData.Builder("V47").Build(),
        new CarData.Builder("TZ9").Build(),
        new CarData.Builder("Truck").Build(),
        new CarData.Builder("FD8").Build(),
        new CarData.Builder("S14").Build(),
        new CarData.Builder("Tofu Machine").Build(),
        new CarData.Builder("Banana").Build()
    };
}