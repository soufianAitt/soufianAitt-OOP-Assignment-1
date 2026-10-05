namespace Part3_BuilderPattern.ComposedBuilder;

public sealed class OrderBuilder
{
    private DateOnly? _date;
    private string? _payment, _currency;
    private decimal? _subTotal;
    private decimal _discount, _tax;

    public OrderBuilder WithOrderDate(DateOnly v) { _date = v; return this; }
    public OrderBuilder WithPaymentMethod(string v) { _payment = v; return this; }
    public OrderBuilder WithCurrency(string v) { _currency = v; return this; }
    public OrderBuilder WithSubTotal(decimal v) { _subTotal = v; return this; }
    public OrderBuilder WithDiscount(decimal v) { _discount = v; return this; }
    public OrderBuilder WithTax(decimal v) { _tax = v; return this; }

    public OrderDetails Build()
    {
        var missing = new List<string>();
        if (_date == null) missing.Add("OrderDate");
        if (string.IsNullOrWhiteSpace(_payment)) missing.Add("PaymentMethod");
        if (string.IsNullOrWhiteSpace(_currency)) missing.Add("Currency");
        if (_subTotal == null) missing.Add("SubTotal");

        if (missing.Count > 0)
            throw new InvalidOperationException("Incomplete order details. Missing: " + string.Join(", ", missing));
        if (_subTotal!.Value < 0) throw new InvalidOperationException("SubTotal cannot be negative.");
        if (_discount < 0 || _tax < 0) throw new InvalidOperationException("Discount and tax cannot be negative.");
        if (_discount > _subTotal.Value) throw new InvalidOperationException("Discount cannot exceed the subtotal.");

        return new OrderDetails(_date!.Value, _payment!, _currency!, _subTotal.Value, _discount, _tax);
    }
}
