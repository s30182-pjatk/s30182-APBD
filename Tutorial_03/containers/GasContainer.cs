using Tutorial_03.exceptions;
using Tutorial_03.products;

namespace Tutorial_03.containers;

public class GasContainer: Container, IHazardous
{
    private float pressure;
    public GasContainer(int cargoMass = 0, int height = 2, int weight = 100, int depth = 1, float pressure = 0.1f) :
        base(cargoMass, height, weight, depth, "G")
    {
        this.pressure = pressure;
    }

    public override void Empty()
    {
        CargoMass *= 0.05f;
        this.Product = null;
        Console.WriteLine($"{this} emptied.");
    }

    public override void Load(Product product, int mass)
    { 
        // As far as I understood only one type of gas can be stored. Description appears to imply so.
        if (product == null)
        {
            this.Product = product;
        }
        else
        {
            if (!product.Name.Equals(this.Product.Name))
            {
                sendNotification();
                return;
            }
        }
        
        try
        {
            if (CargoMass + mass < MaxPayload)
            {
                this.CargoMass += mass;
            }
            else
            {
                throw new OverloadException();
            }
        }
        catch (OverloadException)
        {
            sendNotification();
        }
    }

    public void sendNotification()
    {
        Console.WriteLine($"Dangerous action was attempted on container[{this.SerialNumber}].");
    }
}