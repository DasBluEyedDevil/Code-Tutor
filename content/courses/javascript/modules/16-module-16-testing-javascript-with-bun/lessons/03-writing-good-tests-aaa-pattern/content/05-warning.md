---
type: "WARNING"
title: "AAA Pattern Mistakes"
---

Common AAA (Arrange-Act-Assert) mistakes:

1. **Mixing Act and Assert**:
   ```javascript
   // WRONG - asserting while acting
   it('calculates total', () => {
     expect(cart.addItem({ price: 10 }).getTotal()).toBe(10);
   });
   
   // CORRECT - separate steps
   it('calculates total', () => {
     const cart = new Cart();     // Arrange
     cart.addItem({ price: 10 }); // Act
     expect(cart.getTotal()).toBe(10); // Assert
   });
   ```

2. **Too many assertions**:
   One test should verify one behavior. If you have 10 assertions, you probably have 10 tests.

3. **Arrange duplication**:
   Use beforeEach for common setup:
   ```javascript
   describe('Cart', () => {
     let cart;
     beforeEach(() => cart = new Cart()); // Shared Arrange
     
     it('starts empty', () => {
       expect(cart.items.length).toBe(0);
     });
   });
   ```

4. **Testing too many things at once**:
   If a test fails, you should know exactly what's broken.