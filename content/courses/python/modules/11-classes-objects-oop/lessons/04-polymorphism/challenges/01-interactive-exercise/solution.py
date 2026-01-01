# Payment Processing System
# This solution demonstrates polymorphism in action

class Payment:
    """Base class for all payment methods."""
    
    def __init__(self, amount):
        self.amount = amount
    
    def pay(self):
        """Must be implemented by subclasses."""
        raise NotImplementedError("Subclasses must implement pay()")

class CreditCard(Payment):
    """Credit card payment method."""
    
    def __init__(self, amount, card_number):
        super().__init__(amount)
        # Store only last 4 digits for security
        self.card_last_four = card_number[-4:]
    
    def pay(self):
        """Process credit card payment."""
        return f"Charged ${self.amount:.2f} to card ending in {self.card_last_four}"

class PayPal(Payment):
    """PayPal payment method."""
    
    def __init__(self, amount, email):
        super().__init__(amount)
        self.email = email
    
    def pay(self):
        """Process PayPal payment."""
        return f"Sent ${self.amount:.2f} via PayPal to {self.email}"

class Bitcoin(Payment):
    """Bitcoin payment method."""
    
    def __init__(self, amount, wallet_address):
        super().__init__(amount)
        # Shorten wallet address for display
        self.wallet_short = wallet_address[:8] + '...'
    
    def pay(self):
        """Process Bitcoin payment."""
        btc_amount = self.amount / 45000  # Simplified conversion
        return f"Sent {btc_amount:.6f} BTC (${self.amount:.2f}) to {self.wallet_short}"

def process_payment(payment):
    """Process any type of payment (polymorphism)."""
    print(f"Processing {type(payment).__name__} payment...")
    result = payment.pay()
    print(f"  {result}")
    return result

# Test the payment system
print("=== Payment Processing System ===")

# Create different payment types
payments = [
    CreditCard(99.99, "4111111111111234"),
    PayPal(49.99, "user@example.com"),
    Bitcoin(199.99, "1A1zP1eP5QGefi2DMPTfTL5SLmv7")
]

# Process all payments using the same function
print("\nProcessing payments:")
for payment in payments:
    process_payment(payment)
    print()

# Demonstrate polymorphism
print("The same function works with any payment type!")
print(f"All payments inherit from: {Payment.__name__}")