# protocol/order_status.yaml
enum: OrderStatus
values:
  - pending
  - processing
  - shipped
  - delivered
  - cancelled

---

# protocol/order.yaml
class: Order
table: orders
fields:
  orderId: String
  customerId: int
  status: OrderStatus, default='OrderStatus.pending'
  totalAmount: double
  createdAt: DateTime
  shippedAt: DateTime?

indexes:
  order_customer_idx:
    fields: customerId
  order_status_idx:
    fields: status