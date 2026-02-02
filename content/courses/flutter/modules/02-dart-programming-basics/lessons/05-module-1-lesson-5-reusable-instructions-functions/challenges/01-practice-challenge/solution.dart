// Solution: Simple Calculator Functions
// This solution demonstrates creating and using functions with parameters and return values

void main() {
  // Test our calculator functions
  var a = 10;
  var b = 5;

  print('$a + $b = ${add(a, b)}');
  print('$a - $b = ${subtract(a, b)}');
  print('$a * $b = ${multiply(a, b)}');
  print('$a / $b = ${divide(a, b)}');
}

// Addition function
int add(int x, int y) {
  return x + y;
}

// Subtraction function
int subtract(int x, int y) {
  return x - y;
}

// Multiplication function
int multiply(int x, int y) {
  return x * y;
}

// Division function (returns double for decimal results)
double divide(int x, int y) {
  if (y == 0) {
    print('Error: Cannot divide by zero!');
    return 0;
  }
  return x / y;
}

// Expected Output:
// 10 + 5 = 15
// 10 - 5 = 5
// 10 * 5 = 50
// 10 / 5 = 2.0
