from dataclasses import dataclass, field
from typing import List

@dataclass(slots=True, frozen=True)
class InvoiceItem:
    """Validated invoice line item."""
    name: str
    quantity: int
    unit_price: float
    
    def __post_init__(self):
        if not self.name.strip():
            raise ValueError("name cannot be empty")
        if self.quantity <= 0:
            raise ValueError("quantity must be greater than 0")
        if self.unit_price < 0:
            raise ValueError("unit_price cannot be negative")
        # Round price to 2 decimals
        object.__setattr__(self, 'unit_price', round(self.unit_price, 2))
    
    @property
    def line_total(self) -> float:
        return round(self.quantity * self.unit_price, 2)

@dataclass(slots=True)
class Invoice:
    """Invoice with computed totals."""
    customer: str
    items: List[InvoiceItem] = field(default_factory=list)
    tax_rate: float = 0.08
    
    def __post_init__(self):
        if not self.customer.strip():
            raise ValueError("customer cannot be empty")
        if not (0 <= self.tax_rate <= 1):
            raise ValueError("tax_rate must be between 0 and 1")
    
    def add_item(self, name: str, quantity: int, unit_price: float) -> InvoiceItem:
        """Add a validated item to the invoice."""
        item = InvoiceItem(name=name, quantity=quantity, unit_price=unit_price)
        self.items.append(item)
        return item
    
    @property
    def subtotal(self) -> float:
        return round(sum(item.line_total for item in self.items), 2)
    
    @property
    def tax_amount(self) -> float:
        return round(self.subtotal * self.tax_rate, 2)
    
    @property
    def total(self) -> float:
        return round(self.subtotal + self.tax_amount, 2)
    
    def to_dict(self) -> dict:
        """Serialize invoice to dictionary."""
        return {
            "customer": self.customer,
            "items": [
                {
                    "name": item.name,
                    "quantity": item.quantity,
                    "unit_price": item.unit_price,
                    "line_total": item.line_total
                }
                for item in self.items
            ],
            "subtotal": self.subtotal,
            "tax_rate": self.tax_rate,
            "tax_amount": self.tax_amount,
            "total": self.total
        }

# Test the implementation
print("=== Invoice System Demo ===")
print()

invoice = Invoice(customer="Acme Corp")
invoice.add_item("Widget", 5, 29.99)
invoice.add_item("Gadget", 2, 49.99)
invoice.add_item("Gizmo", 10, 9.99)

print(f"Invoice for: {invoice.customer}")
print("-" * 40)
for item in invoice.items:
    print(f"  {item.name}: {item.quantity} x ${item.unit_price:.2f} = ${item.line_total:.2f}")
print("-" * 40)
print(f"Subtotal: ${invoice.subtotal:.2f}")
print(f"Tax ({invoice.tax_rate*100:.0f}%): ${invoice.tax_amount:.2f}")
print(f"Total: ${invoice.total:.2f}")

print("\n=== Validation Tests ===")

try:
    bad_item = InvoiceItem("", 1, 10.00)
except ValueError as e:
    print(f"Empty name: {e}")

try:
    bad_item = InvoiceItem("Test", 0, 10.00)
except ValueError as e:
    print(f"Zero quantity: {e}")

try:
    bad_item = InvoiceItem("Test", 1, -5.00)
except ValueError as e:
    print(f"Negative price: {e}")

print("\n=== Serialization ===")
import json
print(json.dumps(invoice.to_dict(), indent=2))