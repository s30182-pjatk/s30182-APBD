using Tutorial_03.products;

namespace Tutorial_03.containers;

public class LiquidContainer : Container, IHazardous
{
    public LiquidContainer(int cargoMass = 0, int height = 2, int weight = 150, int depth = 1)
        : base(cargoMass, height, weight, depth, "L")
    {
        
    }

    public override void Empty()
    {
        CargoMass = 0;
        this.Product = null;
        Console.WriteLine($"{this} emptied.");
    }

    public override void Load(Product product, int mass)
    {
        
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
        if (this.storesHazardous())
        {
            if ((this.CargoMass + mass) < (this.MaxPayload * 0.5f))
            {
                this.CargoMass += mass;
            }
            else
            {
                this.sendNotification();
            }
        }
        else
        {
            if ((this.CargoMass + mass) < (this.MaxPayload * 0.9f))
            {
                this.CargoMass += mass;
            }
            else
            {
                this.sendNotification();
            }
        }
    }

    public void sendNotification()
    {
        Console.WriteLine("Dangerous action was attempted.");
    }

    private bool storesHazardous()
    {
        return this.Product.IsHazardous();
    }
}