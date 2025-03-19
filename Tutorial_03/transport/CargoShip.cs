using System.Collections;
using Tutorial_03.containers;
using Tutorial_03.products;

namespace Tutorial_03.transport;

public class CargoShip
{
    static int idCounter = 1;
    
    private ArrayList containers = new ArrayList();
    private float maxSpeed;
    private int maxNContainers;
    private float maxCapacityTones;
    private float cargoWeight = 0;
    private int shipId;

    public CargoShip(float maxSpeed, int maxNContainers, float maxCapacityTones)
    {
        this.maxSpeed = maxSpeed;
        this.maxNContainers = maxNContainers;
        this.maxCapacityTones = maxCapacityTones;
        this.shipId = idCounter++;
    }

    public void LoadContainer(Container container)
    {
        if (containers.Count + 1 > maxNContainers &&
            cargoWeight + getTTLweight()<= maxCapacityTones * 1000)
        {
            Console.WriteLine($"{container} loaded on {this}");
            containers.Add(container);
        }
        else
        {
            Console.WriteLine("Max capacity of cargo ship is exceeded");
        }
    }

    public void LoadContainers(List<Container> containers)
    {
        foreach (Container container in containers)
        {
            LoadContainer(container);
        }
    }

    public void RemoveContainer(Container container)
    {
        Console.WriteLine($"{container} removed from {this}");
        containers.Remove(container);
    }

    public void UnLoad()
    {
        Console.WriteLine($"{this} unloaded");
        cargoWeight = 0;
        containers.Clear();
    }

    private float getTTLweight()
    {
       float weightTTL = 0;
       foreach (Container container in containers)
       {
           weightTTL += container.getCargoMass();
       }

       return weightTTL;
    }

    public void moveContainerToShip(Container container, CargoShip ship)
    {
        if (ship.containers.Contains(container))
        {
            Console.WriteLine($"{container} moved to {this} from {ship}");
            ship.RemoveContainer(container);
            this.containers.Add(container);
        }
        else
        {
            Console.WriteLine($"{container} is not present on {ship}");
        }
    }

    public void replaceContainer(string serialNumber, Container containerReplacement)
    {
        foreach (Container container in containers)
        {
            if (container.getSerialNumber() == serialNumber)
            {
                containers.Remove(container);
                containers.Add(containerReplacement);
                Console.WriteLine($"{containerReplacement} replaced {container}");
                return;
            }
        }
        
        Console.WriteLine($"{containerReplacement} not found on {this}");
    }

    public Product unLoadContainer(Container container)
    {
        Console.WriteLine($"{container} unloaded from {this}");
        return container.getProduct();
    }

    public int getShipId()
    {
        return this.shipId;
    }

    public float MaxSpeed
    {
        get => maxSpeed;
        set => maxSpeed = value;
    }

    public int MaxNContainers
    {
        get => maxNContainers;
        set => maxNContainers = value;
    }

    public float MaxCapacityTones
    {
        get => maxCapacityTones;
        set => maxCapacityTones = value;
    }

    public override string ToString()
    {
        return $"Cargo Ship #{shipId}";
    }
}