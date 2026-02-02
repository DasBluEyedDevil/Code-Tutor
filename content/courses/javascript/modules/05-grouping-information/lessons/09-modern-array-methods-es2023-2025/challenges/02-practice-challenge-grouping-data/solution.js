let orders = [
  { id: 1, product: 'Laptop', price: 1000, status: 'completed' },
  { id: 2, product: 'Mouse', price: 25, status: 'pending' },
  { id: 3, product: 'Keyboard', price: 75, status: 'completed' },
  { id: 4, product: 'Monitor', price: 300, status: 'cancelled' },
  { id: 5, product: 'Headphones', price: 150, status: 'pending' },
  { id: 6, product: 'Webcam', price: 80, status: 'completed' }
];

// 1. Group orders by status
let byStatus = Object.groupBy(orders, order => order.status);

console.log('Completed orders:', byStatus.completed?.length ?? 0);  // 3
console.log('Pending orders:', byStatus.pending?.length ?? 0);  // 2
console.log('Cancelled orders:', byStatus.cancelled?.length ?? 0);  // 1

// 2. Calculate total value of completed orders
let completedTotal = byStatus.completed?.reduce((sum, order) => sum + order.price, 0) ?? 0;
console.log('Completed orders total: $' + completedTotal);  // $1155