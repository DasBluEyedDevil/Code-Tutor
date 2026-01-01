// Solution: Sum with While Loop
// This demonstrates using while loops for accumulation

void main() {
    // Step 1: Create sum variable to accumulate the total
    int sum = 0;
    
    // Step 2: Create counter variable starting at 1
    int n = 1;
    
    // Step 3: Loop while n <= 5
    while (n <= 5) {
        // Add n to sum
        sum += n;
        
        // Increment n
        n++;
    }
    
    // Step 4: Print the final sum
    println(sum);
}