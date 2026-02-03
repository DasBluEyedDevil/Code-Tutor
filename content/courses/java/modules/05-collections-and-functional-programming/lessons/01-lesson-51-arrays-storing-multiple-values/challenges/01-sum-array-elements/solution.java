// Solution: Sum Array Elements
// This demonstrates looping through an array to calculate a sum

void main() {
    // Create the array with the given values
    int[] numbers = {5, 10, 15, 20, 25};
    
    // Variable to accumulate the sum
    var sum = 0;
    
    // Loop through each element in the array
    for (var i = 0; i < numbers.length; i++) {
        sum += numbers[i];  // Add each element to sum
    }
    
    // Print the total sum
    IO.println(sum);
}