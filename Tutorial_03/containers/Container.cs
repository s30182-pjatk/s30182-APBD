using System.Collections;
using Tutorial_03.products;

namespace Tutorial_03.containers;

public abstract class Container
{
    protected static int serialNumberCounter = 1;
    
    protected float CargoMass { get; set; }
    protected int Height { get; }
    protected int Weight{ get; }
    protected int Depth{ get; }
    protected string SerialNumber{ get; }
    protected int MaxPayload{ get; }
    protected Product Product = null;

    public Container(int cargoMass, int height, int weight, int depth, string type)
    {
        this.CargoMass = cargoMass;
        this.Height = height;
        this.Weight = weight;
        this.Depth = depth;
        this.SerialNumber = $"KON-{type}-{serialNumberCounter++}";
    }

    public float getCargoMass()
    {
        return CargoMass;
    }

    public string getSerialNumber()
    {
        return SerialNumber;
    }

    public Product getProduct()
    {
        return Product;
    }

    public float getMaxPayload()
    {
        return MaxPayload;
    }

    public override string ToString()
    {
        if (Product == null)
        {
            return $"{SerialNumber}:empty";
        }
        
        return $"{SerialNumber}:{Product.getName()}";
    }

    public string getProductName()
    {
        return Product.getName();
    }
    public abstract void Empty();
    public abstract void Load(Product product, int mass);
}