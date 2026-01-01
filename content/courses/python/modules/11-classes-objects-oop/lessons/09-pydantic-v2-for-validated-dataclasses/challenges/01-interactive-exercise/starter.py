from dataclasses import dataclass, field
from typing import List

# TODO: Create InvoiceItem dataclass
# Fields: name (str), quantity (int), unit_price (float)
# Validation: quantity > 0, unit_price >= 0
# Computed: line_total property

# TODO: Create Invoice dataclass  
# Fields: customer (str), items (list), tax_rate (float = 0.08)
# Computed properties: subtotal, tax_amount, total
# Method: add_item(name, quantity, unit_price)

# Test your implementation
invoice = Invoice(customer="Acme Corp")
invoice.add_item("Widget", 5, 29.99)
invoice.add_item("Gadget", 2, 49.99)
print(f"Subtotal: ${invoice.subtotal:.2f}")
print(f"Tax: ${invoice.tax_amount:.2f}")
print(f"Total: ${invoice.total:.2f}")