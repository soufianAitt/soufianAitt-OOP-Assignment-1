namespace Part3_BuilderPattern.ComposedBuilder;

public sealed class OrderDetails
{
    public DateOnly OrderDate { get; }
    public string PaymentMethod { get; }
    public string Currency { get; }
    public decimal SubTotal { get; }
    public decimal DiscountAmount { get; }
    public decimal TaxAmount { get; }
    public decimal TotalAmount => SubTotal - DiscountAmount + TaxAmount;

    internal OrderDetails(DateOnly orderDate, string paymentMethod, string currency,
                          decimal subTotal, decimal discountAmount, decimal taxAmount)
    {
        OrderDate = orderDate; PaymentMethod = paymentMethod; Currency = currency;
        SubTotal = subTotal; DiscountAmount = discountAmount; TaxAmount = taxAmount;
    }
}
