using Tutorial_03.products;

namespace Tutorial_03.containers;

public class RefrigeratedContainer : Container, ILoadable
{
    private string cargoType;
    private float containerTemperature;
    public RefrigeratedContainer(int cargoMass, int height, int weight, int depth, float containerTemperature, string cargoType) :
        base(cargoMass, height, weight, depth, "C")
    {
        this.cargoType = cargoType;
        this.containerTemperature = containerTemperature;
    }

    public void Empty()
    {
        CargoMass = 0;
        this.Product = null;
        Console.WriteLine($"{this} emptied.");
    }
 
    public void Load(Product product, int mass)
    {
        if (product.getTemperature() > containerTemperature &&
            product.getName().Equals(this.cargoType))
        {
            Console.WriteLine($"{mass}kg. {product.getName()} loaded in {this}");
            CargoMass += mass;
        }
    }
}