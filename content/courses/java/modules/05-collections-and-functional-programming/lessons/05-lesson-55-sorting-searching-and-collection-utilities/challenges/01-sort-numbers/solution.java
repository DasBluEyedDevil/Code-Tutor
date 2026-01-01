// Solution: Sort Numbers
// This demonstrates using Collections.sort()

import java.util.ArrayList;
import java.util.Collections;

public class Sorter {
    public static ArrayList<Integer> sortNumbers(ArrayList<Integer> nums) {
        // Collections.sort modifies the list in-place
        Collections.sort(nums);
        
        // Return the sorted list
        return nums;
    }
}