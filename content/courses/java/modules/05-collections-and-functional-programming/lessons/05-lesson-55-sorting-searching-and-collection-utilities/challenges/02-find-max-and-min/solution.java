// Solution: Find Max and Min
// This demonstrates using Collections.max() and min()

import java.util.ArrayList;
import java.util.Collections;

public class RangeFinder {
    public static int getRange(ArrayList<Integer> nums) {
        // Get the maximum value
        int max = Collections.max(nums);
        
        // Get the minimum value
        int min = Collections.min(nums);
        
        // Return the difference (range)
        return max - min;
    }
}