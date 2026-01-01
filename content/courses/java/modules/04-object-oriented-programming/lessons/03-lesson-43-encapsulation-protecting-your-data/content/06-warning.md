---
type: "WARNING"
title: "Common Encapsulation Pitfalls"
---

COMMON MISTAKES TO AVOID:

1. EXPOSING MUTABLE OBJECTS:
   private List<String> items;
   public List<String> getItems() { return items; }  // BAD!
   Caller can modify your internal list!
   CORRECT: return new ArrayList<>(items);  // Return a copy

2. SETTERS FOR EVERYTHING (ANEMIC CLASSES):
   Auto-generating all getters/setters defeats encapsulation.
   Only expose what is truly needed. Prefer behavior methods.

3. BREAKING IMMUTABILITY WITH ARRAYS:
   private int[] scores;
   public int[] getScores() { return scores; }  // BAD!
   CORRECT: return Arrays.copyOf(scores, scores.length);

4. FORGETTING VALIDATION IN CONSTRUCTORS:
   Validate in constructor too, not just setters.
   new BankAccount(-1000);  // Should this work?

5. JAVA 16+ ALTERNATIVE - RECORDS:
   Records are immutable by default with automatic encapsulation:
   record Account(double balance) {}
   Fields are final, only getters (no setters), perfect for DTOs.

6. FRAMEWORK REQUIREMENTS:
   Some frameworks (Hibernate, Spring) need getters/setters.
   Use records for DTOs, traditional classes for JPA entities.