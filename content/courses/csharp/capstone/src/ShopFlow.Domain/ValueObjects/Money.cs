namespace ShopFlow.Domain.ValueObjects;

/// <summary>
/// Value object representing a monetary amount with currency.
/// Immutable and compared by value.
/// </summary>
public sealed class Money : IEquatable<Money>
{
    public decimal Amount { get; }
    public string Currency { get; }

    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    /// <summary>
    /// Creates a Money value object.
    /// </summary>
    /// <param name="amount">The monetary amount (must be non-negative).</param>
    /// <param name="currency">The currency code (e.g., "USD", "EUR").</param>
    /// <exception cref="ArgumentException">Thrown when amount is negative or currency is invalid.</exception>
    public static Money Create(decimal amount, string currency = "USD")
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative.", nameof(amount));

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency cannot be empty.", nameof(currency));

        if (currency.Length != 3)
            throw new ArgumentException("Currency must be a 3-letter ISO code.", nameof(currency));

        return new Money(Math.Round(amount, 2), currency.ToUpperInvariant());
    }

    /// <summary>
    /// Creates a Money value with zero amount.
    /// </summary>
    public static Money Zero(string currency = "USD") => Create(0, currency);

    /// <summary>
    /// Adds two Money values together.
    /// </summary>
    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot add {Currency} and {other.Currency}.");

        return Create(Amount + other.Amount, Currency);
    }

    /// <summary>
    /// Subtracts a Money value from this one.
    /// </summary>
    public Money Subtract(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot subtract {other.Currency} from {Currency}.");

        return Create(Amount - other.Amount, Currency);
    }

    /// <summary>
    /// Multiplies the amount by a factor.
    /// </summary>
    public Money Multiply(decimal factor)
    {
        if (factor < 0)
            throw new ArgumentException("Factor cannot be negative.", nameof(factor));

        return Create(Amount * factor, Currency);
    }

    public bool Equals(Money? other)
    {
        if (other is null) return false;
        return Amount == other.Amount && Currency == other.Currency;
    }

    public override bool Equals(object? obj) => Equals(obj as Money);

    public override int GetHashCode() => HashCode.Combine(Amount, Currency);

    public override string ToString() => $"{Amount:F2} {Currency}";

    public static bool operator ==(Money? left, Money? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(Money? left, Money? right) => !(left == right);

    public static bool operator <(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException($"Cannot compare {left.Currency} and {right.Currency}.");
        return left.Amount < right.Amount;
    }

    public static bool operator >(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException($"Cannot compare {left.Currency} and {right.Currency}.");
        return left.Amount > right.Amount;
    }

    public static bool operator <=(Money left, Money right) => left < right || left == right;

    public static bool operator >=(Money left, Money right) => left > right || left == right;
}
