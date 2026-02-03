// Solution: Method with Return Value
// This demonstrates methods that return values

void main() {
    // Call the method with 5 and print the result
    int result = doubleNumber(5);
    IO.println(result);
}

// Method that takes an int and returns it doubled
// Return type is int (not void) because we return a value
int doubleNumber(int number) {
    return number * 2;
}