using Tutorial_03.products;

namespace Tutorial_03.containers;

public class RefrigeratedContainer : Container
{
    private string cargoType;
    private float containerTemperature;
    public RefrigeratedContainer(string cargoType, int cargoMass = 0, int height = 3, int weight = 200, int depth = 2,
        float containerTemperature = 10) :
        base(cargoMass, height, weight, depth, "C")
    {
        this.cargoType = cargoType;
        this.containerTemperature = containerTemperature;
    }

    public override void Empty()
    {
        CargoMass = 0;
        this.Product = null;
        Console.WriteLine($"{this} emptied.");
    }
 
    public override void Load(Product product, int mass)
    {
        if (product.getTemperature() > containerTemperature &&
            product.getName().Equals(this.cargoType))
        {
            Console.WriteLine($"{mass}kg. {product.getName()} loaded in {this}");
            CargoMass += mass;
        }
    }
}