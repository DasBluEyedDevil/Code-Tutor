interface Product {
  name: string;
  price: number;
}

function totalPrice(products: Product[]): number {
  return products.reduce((sum, p) => sum + p.price, 0);
}