// Solution: Count to 10
// This demonstrates while loops for counting

void main() {
    // Step 1: Create counter variable starting at 1
    int i = 1;
    
    // Step 2: Loop while i <= 10
    while (i <= 10) {
        // Step 3: Print the current value
        IO.println(i);
        
        // Step 4: Increment i (CRITICAL to avoid infinite loop!)
        i++;
    }
}