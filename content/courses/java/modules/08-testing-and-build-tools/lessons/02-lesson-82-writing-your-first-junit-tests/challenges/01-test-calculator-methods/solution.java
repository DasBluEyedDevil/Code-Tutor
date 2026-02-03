// Solution: Test Calculator Methods
// This demonstrates thinking about test scenarios

// Important test scenarios for multiply(int a, int b):
// 1. Positive numbers: multiply(3, 4) should return 12
// 2. Negative numbers: multiply(-2, 3) should return -6
// 3. Two negatives: multiply(-2, -3) should return 6
// 4. Zero: multiply(5, 0) should return 0
// 5. One: multiply(7, 1) should return 7

// Example test class structure:
// import org.junit.jupiter.api.Test;
// import static org.junit.jupiter.api.Assertions.*;
//
// class CalculatorTest {
//     @Test
//     void testMultiplyPositiveNumbers() {
//         assertEquals(12, Calculator.multiply(3, 4));
//     }
//     
//     @Test
//     void testMultiplyWithZero() {
//         assertEquals(0, Calculator.multiply(5, 0));
//     }
//     
//     @Test
//     void testMultiplyNegativeNumbers() {
//         assertEquals(6, Calculator.multiply(-2, -3));
//     }
// }

void main() {
    IO.println("Test scenarios defined above");
}