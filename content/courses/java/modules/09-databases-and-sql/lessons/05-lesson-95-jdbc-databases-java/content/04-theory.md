---
type: "THEORY"
title: "Executing Updates - INSERT, UPDATE, DELETE"
---

Use executeUpdate() instead of executeQuery():

INSERT:
String sql = "INSERT INTO students (name, age) VALUES ('Alice', 20)";
try (Connection conn = DriverManager.getConnection(url, user, password);
     Statement stmt = conn.createStatement()) {
    
    int rowsAffected = stmt.executeUpdate(sql);
    IO.println("Inserted " + rowsAffected + " row(s)");
}

UPDATE:
String sql = "UPDATE students SET age = 21 WHERE name = 'Alice'";
int rowsAffected = stmt.executeUpdate(sql);

DELETE:
String sql = "DELETE FROM students WHERE age < 18";
int rowsAffected = stmt.executeUpdate(sql);