using System;

class Program
{
    static void PrintMenu() {
        Console.WriteLine();
        Console.WriteLine("---------- MENU ----------");
        Console.WriteLine("1) Print customers");
        Console.WriteLine("2) Print products");
        Console.WriteLine("3) Print all orders");
        Console.WriteLine("4) Print one order by id");
        Console.WriteLine("5) Create order");
        Console.WriteLine("6) Add line to order");
        Console.WriteLine("7) Mark order paid");
        Console.WriteLine("8) Show paid sales total");
        Console.WriteLine("0) Exit");
        Console.Write("Choice: ");
    }

    // Asks the user for a number and keeps asking until it is a valid number.
    static int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);
            string text = Console.ReadLine();
            int number;
            if (int.TryParse(text, out number))
            {
                return number;
            }
            Console.WriteLine("Please enter a number.");
        }
    }

    static void Main()
    {
        Console.WriteLine("Order System (with classes)");

        Shop shop = new Shop();
        shop.SeedSampleData();
        shop.RunDemoScenario();

        shop.PrintCustomers();
        shop.PrintProducts();
        shop.PrintAllOrders();
        Console.WriteLine();
        Console.WriteLine("Paid sales total after demo: " + shop.TotalSalesPaidOnly().ToString("F2"));

        int choice = -1;
        while (choice != 0)
        {
            PrintMenu();
            choice = ReadInt("");

            if (choice == 1)
            {
                shop.PrintCustomers();
            }
            else if (choice == 2)
            {
                shop.PrintProducts();
            }
            else if (choice == 3)
            {
                shop.PrintAllOrders();
            }
            else if (choice == 4)
            {
                int orderId = ReadInt("Order id: ");
                shop.PrintOrder(orderId);
            }
            else if (choice == 5)
            {
                int orderId = ReadInt("Order id: ");
                int customerId = ReadInt("Customer id: ");
                Console.Write("Date (YYYY-MM-DD): ");
                string date = Console.ReadLine();
                shop.CreateOrder(orderId, customerId, date);
            }
            else if (choice == 6)
            {
                int orderId = ReadInt("Order id: ");
                int productId = ReadInt("Product id: ");
                int quantity = ReadInt("Quantity: ");
                shop.AddLineToOrder(orderId, productId, quantity);
            }
            else if (choice == 7)
            {
                int orderId = ReadInt("Order id: ");
                shop.PayOrder(orderId);
            }
            else if (choice == 8)
            {
                Console.WriteLine("Paid sales total: " + shop.TotalSalesPaidOnly().ToString("F2"));
            }
            else if (choice == 0)
            {
                Console.WriteLine("Bye.");
            }
            else
            {
                Console.WriteLine("Unknown choice.");
            }
        }
    }
}
