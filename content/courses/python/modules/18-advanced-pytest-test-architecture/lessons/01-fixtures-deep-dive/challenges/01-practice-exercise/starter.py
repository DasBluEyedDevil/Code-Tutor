import pytest

@pytest.fixture
def product_factory():
    created = []
    
    def _create_product(name: str, price: float = 9.99):
        product = {"name": name, "price": price, "id": len(created) + 1}
        created.____(product)
        return product
    
    ____ _create_product
    
    print(f"Cleaning up {len(created)} products")

def test_create_products(product_factory):
    p1 = product_factory("Widget")
    p2 = product_factory("Gadget", price=19.99)
    assert p1["id"] == 1
    assert p2["price"] == 19.99
