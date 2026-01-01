// Solution: Get Top Scores
// This demonstrates sorting in reverse order and sublist

import java.util.ArrayList;
import java.util.Collections;

public class TopScores {
    public static ArrayList<Integer> getTopThree(ArrayList<Integer> scores) {
        // Sort in descending order (highest first)
        Collections.sort(scores, Collections.reverseOrder());
        
        // Create result list
        ArrayList<Integer> result = new ArrayList<>();
        
        // Take up to 3 elements (or all if less than 3)
        int count = Math.min(3, scores.size());
        for (int i = 0; i < count; i++) {
            result.add(scores.get(i));
        }
        
        return result;
    }
}