using Tutorial_03.exceptions;
using Tutorial_03.products;

namespace Tutorial_03.containers;

public class GasContainer: Container, IHazardous
{
    private float pressure;
    public GasContainer(int cargoMass, int height, int weight, int depth, float pressure) :
        base(cargoMass, height, weight, depth, "L")
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
            if (!product.getName().Equals(this.Product.getName()))
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