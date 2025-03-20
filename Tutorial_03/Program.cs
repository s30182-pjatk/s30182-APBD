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

    public static void testInit()
    {
        Container container = new LiquidContainer();
        Container container1 = new GasContainer();
        Container container2 = new RefrigeratedContainer("Bananas");
        Container container3 = new RefrigeratedContainer("Chocolate");
        CargoShip cargoShip = new CargoShip(12, 3, 2);
        CargoShip cargoShip2 = new CargoShip(12, 3, 2);
        container.Load(nameProductMap["Fuel"], 100);
        systemContainers.Add(container);
        systemContainers.Add(container1);
        systemContainers.Add(container2);
        systemContainers.Add(container3);
        cargoShip.LoadContainer(container);
        systemCargoShips.Add(cargoShip);
        systemCargoShips.Add(cargoShip2);
        
    }

    public static void printProducts()
    {
        Console.WriteLine($"Available products: {String.Join(", ", nameProductMap.Keys)}");
    }
    public static void printtContainerData(string serialNumber)
    {
        
        Container container = getContainer(serialNumber);
        if (container != null)
        {
            Console.WriteLine($"{container.getSerialNumber()} data: " +
                              $" (product={container.getProductName()}, " +
                              $" cargo mass={container.getCargoMass()}, " +
                              $" max. payload={container.getMaxPayload()})");
        }
    }

    public static void printCargoShipData(int cargoShipNumber)
    {
        CargoShip ship = getCargoShip(cargoShipNumber);
        if (ship != null)
        {
            Console.WriteLine($"{ship} data: " +
                              $"max speed: {ship.MaxSpeed}, " +
                              $"max N containers: {ship.MaxNContainers}, " +
                              $"max cargo weight: {ship.MaxCapacityTones} tons, " +
                              $"cargo on board: {string.Join(", ", ship.Containers.ToArray())}");
        }
    }

    public static void removeContainer(string serialNumber)
    {
        Container container = getContainer(serialNumber);
        systemCargoShips.Remove(container);
    }
    
    public static void removeShip(int id)
    {
        CargoShip ship = getCargoShip(id);
        systemCargoShips.Remove(ship);
    }

    public static void transferContainerToCargoShip(string serialNumber, int fromCargoShipNumber, int toCargoShipNumber)
    {
        Container container = getContainer(serialNumber);
        CargoShip fromShip = getCargoShip(fromCargoShipNumber);
        CargoShip toShip = getCargoShip(toCargoShipNumber);

        if (container == null || fromShip == null || toShip == null)
        {
            return;
        }

        fromShip.moveContainerToShip(container, toShip);
        
    }

    public static void replaceContainerOnShip(int shipNumber, string serialNumber, string newSerialNumber)
    {
        Container container = getContainer(serialNumber);
        Container newContainer = getContainer(newSerialNumber);
        CargoShip fromShip = getCargoShip(shipNumber);
        
        // Check if container already on another ship
        foreach (CargoShip ship in systemCargoShips)
        {
            if (ship.hasContainer(newContainer))
            {
                Console.WriteLine("Container already on a ship. Transfer instead.");
                return;
            }
        }
        
        fromShip.replaceContainer(container, newContainer);
    }
    public static Container getContainer(string serialNumber)
    {
        foreach (Container container in systemContainers)
        {
            if (container.getSerialNumber() == serialNumber)
            {
                return container;
            }
        }
        
        Console.WriteLine($"Container not found: {serialNumber}");
        return null;
    }

    public static CargoShip getCargoShip(int id)
    {
        foreach (CargoShip ship in systemCargoShips)
        {
            if (ship.getShipId() == id)
            {
                return ship;
            }
        }
        
        Console.WriteLine($"Ship not found: Cargo Ship #{id}");
        return null;
    }
    
    public static void Main(string[] args)
    {
        
        
        
        // Placeholders
        Container container;
        CargoShip cargoShip;
        string line;
        initialiseProducts();
        
        // Testing method
        testInit();
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
8. Load container to ship. [serialNumber] [cargo ship number]
9. Load product to container. [product name] [mass] [serialNumber]
10. Unload container. [serial number]
11. Transfer container to another ship. [serialNumber] [cargo ship number from] [cargo ship number to]
12. Replace container with another on a ship. [cargo ship number] [serialNumber] [serial Number]
13. Load n number of containers. [cargo ship number] [serialNumber]*n
14. Empty container. [serial number]
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
                    cargoShip = new CargoShip(float.Parse(parts[1]),
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
                case "8":
                    if (parts.Length != 3)
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }
                    
                    container = getContainer(parts[1]);
                    cargoShip = getCargoShip(int.Parse(parts[2]));
                    cargoShip.LoadContainer(container);
                    break;
                case "9":
                    if (parts.Length != 4)
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }

                    if (nameProductMap[parts[1]] == null)
                    {
                        Console.WriteLine("Invalid name product.");
                        continue;
                    }
                    
                    container = getContainer(parts[3]);
                    container.Load(nameProductMap[parts[1]], int.Parse(parts[2]));
                    break;
                case "10":
                    if (parts.Length != 2)
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }
                    container = getContainer(parts[1]);
                    container.Empty();
                    break;
                case "11":
                    if (parts.Length != 4)
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }
                    transferContainerToCargoShip(parts[1], int.Parse(parts[2]), int.Parse(parts[3]));
                    break;
                case "12":
                    if (parts.Length != 4)
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }
                    
                    replaceContainerOnShip(int.Parse(parts[1]), parts[2], parts[3]);
                    break;
                case "13":
                    if (parts.Length < 2)
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }
                    CargoShip ship = getCargoShip(int.Parse(parts[1]));
                    for (int i = 2; i < parts.Length; i++)
                    {
                        container = getContainer(parts[i]);
                        if (!systemContainers.Contains(container))
                        {
                            Console.WriteLine($"Container not found: {container.getSerialNumber()}");
                            continue;
                        }
                        ship.LoadContainer(container);
                    }

                    break;
                case "14":
                    if (parts.Length != 2)
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }
                    container = getContainer(parts[1]);
                    container.Empty();
                    break;
                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }
        }
    }
}