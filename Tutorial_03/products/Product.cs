namespace Tutorial_03.products;

public class Product
{
    private string Name;
    private float temperature;
    private bool isHazardous;

    public Product(string name, float temperature, bool isHazardous)
    {
        Name = name;
        temperature = temperature;
        isHazardous = isHazardous;
    }

    public bool IsHazardous()
    {
        return isHazardous;
    }

    public float getTemperature()
    {
        return temperature;
    }

    public string getName()
    {
        return Name;
    }
}
