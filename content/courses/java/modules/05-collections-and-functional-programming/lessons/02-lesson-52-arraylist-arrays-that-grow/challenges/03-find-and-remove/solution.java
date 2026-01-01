// Solution: Find and Remove
// This demonstrates removing elements from an ArrayList

import java.util.ArrayList;

public class ListFilter {
    public static ArrayList<Integer> removeNegatives(ArrayList<Integer> nums) {
        // Create a new ArrayList for positive numbers
        ArrayList<Integer> result = new ArrayList<>();
        
        // Only add non-negative numbers to result
        for (int num : nums) {
            if (num >= 0) {
                result.add(num);
            }
        }
        
        return result;
    }
}