// Solution: Sum of ArrayList
// This demonstrates iterating through an ArrayList

import java.util.ArrayList;

public class ListCalculator {
    public static int sumList(ArrayList<Integer> nums) {
        // Variable to accumulate sum
        int sum = 0;
        
        // Enhanced for loop - easier way to iterate
        for (int num : nums) {
            sum += num;
        }
        
        return sum;
    }
}