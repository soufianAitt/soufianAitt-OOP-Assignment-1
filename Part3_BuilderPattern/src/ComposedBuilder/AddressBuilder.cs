namespace Part3_BuilderPattern.ComposedBuilder;

public sealed class AddressBuilder
{
    private string? _street, _city, _state, _zip, _country;

    public AddressBuilder WithStreet(string v) { _street = v; return this; }
    public AddressBuilder WithCity(string v) { _city = v; return this; }
    public AddressBuilder WithState(string v) { _state = v; return this; }
    public AddressBuilder WithZipCode(string v) { _zip = v; return this; }
    public AddressBuilder WithCountry(string v) { _country = v; return this; }

    public Address Build()
    {
        var missing = new List<string>();
        if (string.IsNullOrWhiteSpace(_street)) missing.Add("Street");
        if (string.IsNullOrWhiteSpace(_city)) missing.Add("City");
        if (string.IsNullOrWhiteSpace(_zip)) missing.Add("ZipCode");
        if (string.IsNullOrWhiteSpace(_country)) missing.Add("Country");

        if (missing.Count > 0)
            throw new InvalidOperationException("Incomplete address. Missing: " + string.Join(", ", missing));

        return new Address(_street!, _city!, _state, _zip!, _country!);
    }
}
