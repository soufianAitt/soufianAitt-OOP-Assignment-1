namespace Part3_BuilderPattern.ComposedBuilder;

public sealed class InvoiceBuilder
{
    private string? _id, _name, _email, _phone;
    private Address? _billing, _shipping;
    private OrderDetails? _order;
    private bool _shipToBilling;

    public InvoiceBuilder WithInvoiceId(string v) { _id = v; return this; }

    public InvoiceBuilder ForCustomer(string name, string email, string? phone = null)
    {
        _name = name; _email = email; _phone = phone;
        return this;
    }

    // The SAME AddressBuilder configures both addresses.
    public InvoiceBuilder BillTo(Action<AddressBuilder> configure)
    {
        var b = new AddressBuilder();
        configure(b);
        _billing = b.Build();                 // AddressBuilder validates itself
        return this;
    }

    public InvoiceBuilder ShipTo(Action<AddressBuilder> configure)
    {
        var b = new AddressBuilder();
        configure(b);
        _shipping = b.Build();
        _shipToBilling = false;
        return this;
    }

    public InvoiceBuilder ShipToBillingAddress() { _shipToBilling = true; return this; }

    public InvoiceBuilder WithOrder(Action<OrderBuilder> configure)
    {
        var b = new OrderBuilder();
        configure(b);
        _order = b.Build();                   
        return this;
    }

    public Invoice Build()
    {
        var missing = new List<string>();
        if (string.IsNullOrWhiteSpace(_id)) missing.Add("InvoiceId");
        if (string.IsNullOrWhiteSpace(_name) || string.IsNullOrWhiteSpace(_email)) missing.Add("Customer (name, email)");
        if (_billing == null) missing.Add("BillingAddress");
        if (_shipping == null && !_shipToBilling) missing.Add("ShippingAddress");
        if (_order == null) missing.Add("Order");

        if (missing.Count > 0)
            throw new InvalidOperationException("Cannot build Invoice. Missing: " + string.Join(", ", missing));

        return new Invoice(_id!, _name!, _email!, _phone, _billing!, _shipToBilling ? _billing! : _shipping!, _order!);
    }
}
