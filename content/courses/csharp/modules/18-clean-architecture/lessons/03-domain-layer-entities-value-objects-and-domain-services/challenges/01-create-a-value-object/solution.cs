namespace ShopFlow.Domain.ValueObjects;

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}

// Using 'record' for automatic value equality and immutability
public sealed record Address
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string PostalCode { get; }
    public string Country { get; }

    public Address(
        string street,
        string city,
        string state,
        string postalCode,
        string country)
    {
        // Validate required fields
        if (string.IsNullOrWhiteSpace(street))
            throw new DomainException("Street is required");
        
        if (string.IsNullOrWhiteSpace(city))
            throw new DomainException("City is required");
        
        if (string.IsNullOrWhiteSpace(postalCode))
            throw new DomainException("Postal code is required");
        
        if (string.IsNullOrWhiteSpace(country))
            throw new DomainException("Country is required");

        // Normalize and assign (trimming whitespace)
        Street = street.Trim();
        City = city.Trim();
        State = state?.Trim() ?? string.Empty;  // State can be optional
        PostalCode = postalCode.Trim();
        Country = country.Trim();
    }

    // Computed property for formatted address
    public string FullAddress =>
        string.IsNullOrEmpty(State)
            ? $"{Street}, {City} {PostalCode}, {Country}"
            : $"{Street}, {City}, {State} {PostalCode}, {Country}";

    // Note: 'record' automatically provides:
    // - Value equality (Equals, ==, !=)
    // - GetHashCode based on all properties
    // - ToString showing all values
    // - Deconstruction
    // - With-expressions for creating modified copies
}