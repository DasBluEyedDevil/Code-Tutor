---
type: "THEORY"
title: "PreparedStatement - The Right Way"
---

PreparedStatement benefits:
✓ Prevents SQL injection
✓ Better performance (compiled once, reused)
✓ Handles special characters automatically

String sql = "INSERT INTO students (name, age, email) VALUES (?, ?, ?)";

try (PreparedStatement pstmt = conn.prepareStatement(sql)) {
    // Set parameters (1-indexed)
    pstmt.setString(1, "Alice Johnson");
    pstmt.setInt(2, 20);
    pstmt.setString(3, "alice@example.com");
    
    int rowsInserted = pstmt.executeUpdate();
    IO.println("Inserted: " + rowsInserted);
}

BATCH INSERT (multiple rows):
pstmt.setString(1, "Alice");
pstmt.setInt(2, 20);
pstmt.addBatch();

pstmt.setString(1, "Bob");
pstmt.setInt(2, 21);
pstmt.addBatch();

int[] results = pstmt.executeBatch();  // Execute all at once