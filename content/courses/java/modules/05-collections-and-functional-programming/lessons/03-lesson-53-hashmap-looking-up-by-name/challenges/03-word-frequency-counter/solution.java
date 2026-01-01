// Solution: Word Frequency Counter
// This demonstrates using HashMap for counting occurrences

import java.util.HashMap;

public class WordCounter {
    public static HashMap<String, Integer> countWords(String[] words) {
        // Create HashMap to store word counts
        HashMap<String, Integer> counts = new HashMap<>();
        
        // Loop through each word in the array
        for (String word : words) {
            // If word already exists, increment count
            // If not, start count at 1
            if (counts.containsKey(word)) {
                counts.put(word, counts.get(word) + 1);
            } else {
                counts.put(word, 1);
            }
        }
        
        return counts;
    }
}