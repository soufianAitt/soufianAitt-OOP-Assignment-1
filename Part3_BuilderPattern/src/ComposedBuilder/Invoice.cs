namespace Part3_BuilderPattern.ComposedBuilder;

public sealed class Invoice
{
    public string InvoiceId { get; }
    public string CustomerName { get; }
    public string CustomerEmail { get; }
    public string? CustomerPhone { get; }
    public Address BillingAddress { get; }
    public Address ShippingAddress { get; }
    public OrderDetails Order { get; }

    internal Invoice(string invoiceId, string name, string email, string? phone, Address billing, Address shipping, OrderDetails order) {
        InvoiceId = invoiceId; 
		CustomerName = name;
		CustomerEmail = email; 
		CustomerPhone = phone;
        BillingAddress = billing;
		ShippingAddress = shipping;
		Order = order;
    }

}
