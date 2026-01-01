---
type: "THEORY"
title: "⚠️ SQL Injection - A Critical Security Risk!"
---

NEVER concatenate user input directly into SQL:

❌ DANGEROUS CODE:
String userInput = "Alice'; DROP TABLE students; --";
String sql = "SELECT * FROM students WHERE name = '" + userInput + "'";
// Results in: SELECT * FROM students WHERE name = 'Alice'; DROP TABLE students; --'
// YOUR TABLE JUST GOT DELETED!

✓ SAFE CODE (PreparedStatement):
String sql = "SELECT * FROM students WHERE name = ?";
try (PreparedStatement pstmt = conn.prepareStatement(sql)) {
    pstmt.setString(1, userInput);  // Safe: input is escaped
    ResultSet rs = pstmt.executeQuery();
}

ALWAYS USE PREPAREDSTATEMENT WITH USER INPUT!