using System;
using System.Collections.Generic;

// The Shop object owns all the data (this replaces the global variables of the C++ code).
class Shop
{
    private List<Customer> customers = new List<Customer>();
    private List<Product> products = new List<Product>();
    private List<Order> orders = new List<Order>();

    // ---------- find ----------
    public Customer FindCustomer(int id)
    {
        foreach (Customer c in customers)
        {
            if (c.Id == id) return c;
        }
        return null;
    }

    public Product FindProduct(int id)
    {
        foreach (Product p in products)
        {
            if (p.Id == id) return p;
        }
        return null;
    }

    public Order FindOrder(int id)
    {
        foreach (Order o in orders)
        {
            if (o.Id == id) return o;
        }
        return null;
    }

    // ---------- add ----------
    public void AddCustomer(int id, string name, string email, string city, bool isVip)
    {
        if (FindCustomer(id) != null)
        {
            Console.WriteLine("ERROR: customer id " + id + " already exists.");
            return;
        }
        customers.Add(new Customer(id, name, email, city, isVip));
    }

    public void AddProduct(int id, string name, double price, int stock)
    {
        if (FindProduct(id) != null)
        {
            Console.WriteLine("ERROR: product id " + id + " already exists.");
            return;
        }
        products.Add(new Product(id, name, price, stock));
    }

    public void CreateOrder(int orderId, int customerId, string date)
    {
        if (FindOrder(orderId) != null)
        {
            Console.WriteLine("ERROR: order id " + orderId + " already exists.");
            return;
        }

        Customer customer = FindCustomer(customerId);
        if (customer == null)
        {
            Console.WriteLine("ERROR: customer id " + customerId + " not found.");
            return;
        }

        orders.Add(new Order(orderId, customer, date));
    }

    public void AddLineToOrder(int orderId, int productId, int quantity)
    {
        Order order = FindOrder(orderId);
        if (order == null)
        {
            Console.WriteLine("ERROR: order id " + orderId + " not found.");
            return;
        }

        Product product = FindProduct(productId);
        if (product == null)
        {
            Console.WriteLine("ERROR: product id " + productId + " not found.");
            return;
        }

        order.AddLine(product, quantity);
    }

    public void PayOrder(int orderId)
    {
        Order order = FindOrder(orderId);
        if (order == null)
        {
            Console.WriteLine("ERROR: order id " + orderId + " not found.");
            return;
        }

        order.MarkPaid();
    }

    // ---------- reports ----------
    public void PrintCustomers()
    {
        Console.WriteLine();
        Console.WriteLine("=== CUSTOMERS (" + customers.Count + ") ===");
        foreach (Customer c in customers)
        {
            c.Print();
        }
    }

    public void PrintProducts()
    {
        Console.WriteLine();
        Console.WriteLine("=== PRODUCTS (" + products.Count + ") ===");
        foreach (Product p in products)
        {
            p.Print();
        }
    }

    public void PrintOrder(int orderId)
    {
        Order order = FindOrder(orderId);
        if (order == null)
        {
            Console.WriteLine("ERROR: order id " + orderId + " not found.");
            return;
        }
        order.Print();
    }

    public void PrintAllOrders()
    {
        Console.WriteLine();
        Console.WriteLine("=== ALL ORDERS (" + orders.Count + ") ===");
        foreach (Order o in orders)
        {
            o.Print();
        }
    }

    public double TotalSalesPaidOnly()
    {
        double sum = 0;
        foreach (Order o in orders)
        {
            if (o.IsPaid)
            {
                sum = sum + o.GetTotal();
            }
        }
        return sum;
    }

    // ---------- sample data ----------
    public void SeedSampleData()
    {
        AddCustomer(1, "Mona Ali", "mona@example.com", "Cairo", true);
        AddCustomer(2, "Omar Hassan", "omar@example.com", "Alexandria", false);
        AddCustomer(3, "Sara Nabil", "sara@example.com", "Giza", false);

        AddProduct(101, "USB Cable", 50.0, 100);
        AddProduct(102, "Wireless Mouse", 250.0, 40);
        AddProduct(103, "Mechanical Keyboard", 1200.0, 15);
        AddProduct(104, "Laptop Stand", 400.0, 25);
    }

    public void RunDemoScenario()
    {
        CreateOrder(1001, 1, "2026-09-15");
        AddLineToOrder(1001, 101, 2);
        AddLineToOrder(1001, 102, 1);
        PayOrder(1001);

        CreateOrder(1002, 2, "2026-09-15");
        AddLineToOrder(1002, 103, 1);
        AddLineToOrder(1002, 104, 1);

        CreateOrder(1003, 3, "2026-09-16");
        AddLineToOrder(1003, 101, 5);
        PayOrder(1003);
    }
}
