namespace Part3_BuilderPattern.SingleBuilder;

public sealed class InvoiceBuilder
{
    internal string? InvoiceId, CustomerName, CustomerEmail, CustomerPhone;
    internal string? BillingStreet, BillingCity, BillingState, BillingZipCode, BillingCountry;
    internal string? ShippingStreet, ShippingCity, ShippingState, ShippingZipCode, ShippingCountry;
    internal DateOnly? OrderDate;
    internal string? PaymentMethod, Currency;
    internal decimal? SubTotal;
    internal decimal DiscountAmount, TaxAmount;

    public InvoiceBuilder WithInvoiceId(string v) { InvoiceId = v; return this; }
    public InvoiceBuilder WithCustomerName(string v) { CustomerName = v; return this; }
    public InvoiceBuilder WithCustomerEmail(string v) { CustomerEmail = v; return this; }
    public InvoiceBuilder WithCustomerPhone(string v) { CustomerPhone = v; return this; }

    public InvoiceBuilder WithBillingStreet(string v) { BillingStreet = v; return this; }
    public InvoiceBuilder WithBillingCity(string v) { BillingCity = v; return this; }
    public InvoiceBuilder WithBillingState(string v) { BillingState = v; return this; }
    public InvoiceBuilder WithBillingZipCode(string v) { BillingZipCode = v; return this; }
    public InvoiceBuilder WithBillingCountry(string v) { BillingCountry = v; return this; }

    public InvoiceBuilder WithShippingStreet(string v) { ShippingStreet = v; return this; }
    public InvoiceBuilder WithShippingCity(string v) { ShippingCity = v; return this; }
    public InvoiceBuilder WithShippingState(string v) { ShippingState = v; return this; }
    public InvoiceBuilder WithShippingZipCode(string v) { ShippingZipCode = v; return this; }
    public InvoiceBuilder WithShippingCountry(string v) { ShippingCountry = v; return this; }

    public InvoiceBuilder WithOrderDate(DateOnly v) { OrderDate = v; return this; }
    public InvoiceBuilder WithPaymentMethod(string v) { PaymentMethod = v; return this; }
    public InvoiceBuilder WithCurrency(string v) { Currency = v; return this; }
    public InvoiceBuilder WithSubTotal(decimal v) { SubTotal = v; return this; }
    public InvoiceBuilder WithDiscountAmount(decimal v) { DiscountAmount = v; return this; }
    public InvoiceBuilder WithTaxAmount(decimal v) { TaxAmount = v; return this; }

    public Invoice Build()
    {
        var missing = new List<string>();
        void Req(string? value, string name) { 
		 if (string.IsNullOrWhiteSpace(value)) missing.Add(name);
		}

        Req(InvoiceId, nameof(InvoiceId));
        Req(CustomerName, nameof(CustomerName));
        Req(CustomerEmail, nameof(CustomerEmail));
        Req(BillingStreet, nameof(BillingStreet));
        Req(BillingCity, nameof(BillingCity));
        Req(BillingZipCode, nameof(BillingZipCode));
        Req(BillingCountry, nameof(BillingCountry));
        Req(ShippingStreet, nameof(ShippingStreet));
        Req(ShippingCity, nameof(ShippingCity));
        Req(ShippingZipCode, nameof(ShippingZipCode));
        Req(ShippingCountry, nameof(ShippingCountry));
        if (OrderDate == null) missing.Add(nameof(OrderDate));
        Req(PaymentMethod, nameof(PaymentMethod));
        Req(Currency, nameof(Currency));
        if (SubTotal == null) missing.Add(nameof(SubTotal));

        if (missing.Count > 0)
            throw new InvalidOperationException("Cannot build Invoice. Missing mandatory values: " + string.Join(", ", missing));

        if (SubTotal!.Value < 0) throw new InvalidOperationException("SubTotal cannot be negative.");
        if (DiscountAmount < 0 || TaxAmount < 0) throw new InvalidOperationException("Discount and tax cannot be negative.");
        if (DiscountAmount > SubTotal.Value) throw new InvalidOperationException("Discount cannot exceed the subtotal.");

        return new Invoice(this);
    }
}
