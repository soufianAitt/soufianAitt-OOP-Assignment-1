using System;

class Customer
{
    public int Id;
    public string Name;
    public string Email;
    public string City;
    public bool IsVip;

    public Customer(int id, string name, string email, string city, bool isVip)
    {
        Id = id;
        Name = name;
        Email = email;
        City = city;
        IsVip = isVip;
    }

    public void Print()
    {
        string vip = IsVip ? "yes" : "no";
        Console.WriteLine("#" + Id + "  " + Name + "  <" + Email + ">  " + City + "  vip=" + vip);
    }
}
