using System;
using System.Collections.Generic;

class Order
{
    public const int MaxLines = 20;

    public int Id;
    public Customer Customer;
    public string Date;
    public bool IsPaid;
    public List<OrderLine> Lines = new List<OrderLine>();

    public Order(int id, Customer customer, string date)
    {
        Id = id;
        Customer = customer;
        Date = date;
        IsPaid = false;
    }

    // Returns true if the line was added, false if something is wrong.
    public bool AddLine(Product product, int quantity)
    {
        if (IsPaid)
        {
            Console.WriteLine("ERROR: cannot change a paid order.");
            return false;
        }

        if (Lines.Count >= MaxLines)
        {
            Console.WriteLine("ERROR: order has too many lines.");
            return false;
        }

        if (quantity <= 0)
        {
            Console.WriteLine("ERROR: quantity must be positive.");
            return false;
        }

        if (product.Stock < quantity)
        {
            Console.WriteLine("ERROR: not enough stock for product #" + product.Id + ".");
            return false;
        }

        product.Stock = product.Stock - quantity;
        Lines.Add(new OrderLine(product, quantity));
        return true;
    }

    public bool MarkPaid()
    {
        if (Lines.Count == 0)
        {
            Console.WriteLine("ERROR: cannot pay an empty order.");
            return false;
        }

        IsPaid = true;
        return true;
    }

    public double GetTotal()
    {
        double total = 0;
        foreach (OrderLine line in Lines)
        {
            total = total + line.GetLineTotal();
        }

        if (Customer.IsVip)
        {
            total = total * 0.90;   // VIP gets 10% off
        }

        return total;
    }

    public void Print()
    {
        Console.WriteLine();
        Console.WriteLine("=== ORDER #" + Id + " ===");
        Console.WriteLine("Date: " + Date);
        Console.WriteLine("Customer: " + Customer.Name + " (#" + Customer.Id + ")");
        Console.WriteLine("Paid: " + (IsPaid ? "yes" : "no"));
        Console.WriteLine("Lines:");

        foreach (OrderLine line in Lines)
        {
            Console.WriteLine("  - " + line.Product.Name + "  x" + line.Quantity +
                              "  @" + line.UnitPrice.ToString("F2") +
                              "  = " + line.GetLineTotal().ToString("F2"));
        }

        if (Customer.IsVip)
        {
            Console.WriteLine("(VIP 10% discount applied)");
        }
        Console.WriteLine("TOTAL: " + GetTotal().ToString("F2"));
    }
}
