using System.Collections;
using Tutorial_03.containers;
using Tutorial_03.transport;

namespace Tutorial_03;

public class Program
{   
    public static ArrayList systemContainers = new ArrayList();
    public static ArrayList systemCargoShips = new ArrayList();

    public static void getContainerData(string containerNumber)
    {
        foreach (Container container in systemContainers)
        {
            if (container.getSerialNumber() == containerNumber)
            {
                Console.WriteLine($"Container data: {container.getSerialNumber()}" +
                                  $" (product={container.getProductName()}," +
                                  $" cargo mass={container.getCargoMass()}," +
                                  $" max. payload={container.getMaxPayload()})");
            }
        }
    }

    public static void getCargoShipData(int cargoShipNumber)
    {
        foreach (CargoShip ship in systemCargoShips)
        {
            if (ship.getShipId() == cargoShipNumber)
            {
                Console.WriteLine($"");
            }
        }
    }
    
    public static void Main(string[] args)
    {
        string line;
        while (true)
        {   
            Console.WriteLine($"List of container ships: {String.Join(", ",systemCargoShips.ToArray())}");
            Console.WriteLine($"\nList of containers: {String.Join(", ",systemContainers.ToArray())}");
            Console.WriteLine("\nPossible actions:\n");
            Console.WriteLine(
                @"1. Add a new container.
2. Add a new cargo ship. L(liquid), G(gas), C(refrigirator)
3. Remove a container.
4. Remove a cargo ship.
");
            
            Console.Write("->");
            line = Console.ReadLine();
            string[] parts = line.Split(" ");

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
                            container = new RefrigeratedContainer(cargoType);
                            systemContainers.Add(container);
                            break;
                        default:
                            Console.WriteLine("Invalid input.");
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }
        }
    }
}