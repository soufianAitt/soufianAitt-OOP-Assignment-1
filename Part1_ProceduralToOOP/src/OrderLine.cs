class OrderLine
{
    public Product Product;
    public int Quantity;
    public double UnitPrice;   // price saved when the line was added

    public OrderLine(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
        UnitPrice = product.Price;
    }

    public double GetLineTotal()
    {
        return UnitPrice * Quantity;
    }
}
