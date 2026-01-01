---
type: "EXAMPLE"
title: "Real-World Examples"
---


### Example 1: Login Check


### Example 2: Weather Advice


**Note**: `\'` lets you put an apostrophe inside a single-quoted string.

### Example 3: Shopping Cart


**Output**: `You need $5.0 more.`



```dart
void main() {
  var itemPrice = 50.00;
  var walletMoney = 45.00;

  if (walletMoney >= itemPrice) {
    print('Purchase successful!');
  } else {
    var shortage = itemPrice - walletMoney;
    print('You need \$$shortage more.');
  }
}
```
