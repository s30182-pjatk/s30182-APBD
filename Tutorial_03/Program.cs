using System.Collections;
using System.Runtime.InteropServices.JavaScript;
using Tutorial_03.containers;
using Tutorial_03.products;
using Tutorial_03.transport;

namespace Tutorial_03;

public class Program
{   
    public static ArrayList systemContainers = new ArrayList();
    public static ArrayList systemCargoShips = new ArrayList();
    public static Dictionary<string, Product> nameProductMap = new Dictionary<string, Product>();

    public static void initialiseProducts()
    {
        Product bananas = new Product("Bananas", 13.3f, false);
        Product chocolate = new Product("Chocolate", 18, false);
        Product fish = new Product("Fish", 2, false);
        Product meat = new Product("Meat", -15, false);
        Product iceCream = new Product("Ice Cream", -18, false);
        Product frozenPizza = new Product("Frozen Pizza", -30, false);
        Product cheese = new Product("Cheese", 7.2f, false);
        Product sausages = new Product("Sausages", 5, false);
        Product butter = new Product("Butter", 20.5f, false);
        Product eggs = new Product("Eggs", 19, false);
        Product fuel = new Product("Fuel", -10, true);
        
        nameProductMap.Add(bananas.Name, bananas);
        nameProductMap.Add(chocolate.Name, chocolate);
        nameProductMap.Add(fish.Name, fish);
        nameProductMap.Add(meat.Name, meat);
        nameProductMap.Add(iceCream.Name, iceCream);
        nameProductMap.Add(frozenPizza.Name, frozenPizza);
        nameProductMap.Add(cheese.Name, cheese);
        nameProductMap.Add(sausages.Name, sausages);
        nameProductMap.Add(butter.Name, butter);
        nameProductMap.Add(eggs.Name, eggs);
        nameProductMap.Add(fuel.Name, fuel);
        
    }

    public static void printProducts()
    {
        Console.WriteLine($"Available products: {String.Join(", ", nameProductMap.Keys)}");
    }
    public static void printtContainerData(string serialNumber)
    {
        foreach (Container container in systemContainers)
        {
            if (container.getSerialNumber() == serialNumber)
            {
                Console.WriteLine($"{container} data: " +
                                  $" (product={container.getProductName()}, " +
                                  $" cargo mass={container.getCargoMass()}, " +
                                  $" max. payload={container.getMaxPayload()})");
            }
        }
    }

    public static void printCargoShipData(int cargoShipNumber)
    {
        foreach (CargoShip ship in systemCargoShips)
        {
            if (ship.getShipId() == cargoShipNumber)
            {
                Console.WriteLine($"{ship} data: " +
                                  $"max speed: {ship.MaxSpeed}, " +
                                  $"max N containers: {ship.MaxNContainers}, " +
                                  $"max cargo weigh: {ship.MaxCapacityTones} tons");
            }
        }
    }

    public static void removeContainer(string serialNumber)
    {
        foreach (Container container in systemContainers)
        {
            if (container.getSerialNumber() == serialNumber)
            {
                systemContainers.Remove(container);
                return;
            }
        }
        
        Console.WriteLine($"Container not found: {serialNumber}");
    }
    
    public static void removeShip(int id)
    {
        foreach (CargoShip ship in systemContainers)
        {
            if (ship.getShipId() == id)
            {
                systemContainers.Remove(ship);
                return;
            }
        }
        
        Console.WriteLine($"Ship not found: Cargo Ship #{id}");
    }
    
    
    public static void Main(string[] args)
    {
        string line;
        initialiseProducts();
        while (true)
        {   
            Console.WriteLine($"\nList of container ships: {String.Join(", ",systemCargoShips.ToArray())}");
            Console.WriteLine($"\nList of containers: {String.Join(", ",systemContainers.ToArray())}");
            Console.WriteLine("\nPossible actions:\n");
            Console.WriteLine(
                @"1. Add a new container. L[liquid], G[gas], C[refrigirator]
2. Add a new cargo ship. [maxspeed, maxNContainers, maxCapacity]
3. Remove a container. [serialNumber]
4. Remove a cargo ship. [ship id number]
5. Get information about container. [serialNumber]
6. Get information about cargo ship. [ship id number]
7. Print available products.
");
            
            Console.Write("->");
            line = Console.ReadLine();
            string[] parts = line.Split(" ");
            Console.WriteLine();

            switch (parts[0])
            {
                case "1":
                    if (parts.Length != 2)
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }
                    Container container;
                    string type = parts[1];

                    switch (type)
                    {
                        case "L":
                            container = new LiquidContainer();
                            systemContainers.Add(container);
                            Console.WriteLine($"{container} created.");
                            break;
                        case "G":
                            container = new GasContainer();
                            systemContainers.Add(container);
                            Console.WriteLine($"{container} created.");
                            break;
                        case "C":
                            Console.Write("Enter cargo type->");
                            string cargoType = Console.ReadLine();
                            Console.WriteLine();
                            container = new RefrigeratedContainer(cargoType);
                            systemContainers.Add(container);
                            break;
                        default:
                            Console.WriteLine("Invalid input.");
                            break;
                    }
                    break;
                case "2":
                    if (parts.Length != 4)
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }
                    CargoShip cargoShip = new CargoShip(float.Parse(parts[1]),
                        int.Parse(parts[2]),
                        float.Parse(parts[3]));
                    systemCargoShips.Add(cargoShip);
                    Console.WriteLine($"{cargoShip} created.");

                    break;
                case "3":
                    if (parts.Length != 2)
                    {
                        Console.WriteLine("Invalid input.");
                    }
                    
                    removeContainer((parts[1]));
                    Console.WriteLine($"Removed container: {parts[1]}");
                    break;
                case "4":
                    if (parts.Length != 2)
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }
                    
                    removeShip(int.Parse(parts[1]));
                    Console.WriteLine($"Removed cargo ship: #{parts[1]}");
                    break;
                case "5":
                    if (parts.Length != 2)
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }
                    printtContainerData(parts[1]);
                    break;
                case "6":
                    if (parts.Length != 2)
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }
                    printCargoShipData(int.Parse(parts[1]));
                    break;
                case "7":
                    if (parts.Length != 1)
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }
                    printProducts();
                    break;
                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }
        }
    }
}