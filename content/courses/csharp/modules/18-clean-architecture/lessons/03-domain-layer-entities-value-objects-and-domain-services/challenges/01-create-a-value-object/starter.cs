namespace ShopFlow.Domain.ValueObjects;

// Implement the Address Value Object here
// Requirements:
// - Immutable (consider using a record)
// - Validation in constructor
// - Value equality
// - Properties: Street, City, State, PostalCode, Country
// - Computed property: FullAddress

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}