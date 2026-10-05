using System;

class Product
{
    public int Id;
    public string Name;
    public double Price;
    public int Stock;

    public Product(int id, string name, double price, int stock)
    {
        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }

    public void Print()
    {
        Console.WriteLine("#" + Id + "  " + Name + "  price=" + Price.ToString("F2") + "  stock=" + Stock);
    }
}
