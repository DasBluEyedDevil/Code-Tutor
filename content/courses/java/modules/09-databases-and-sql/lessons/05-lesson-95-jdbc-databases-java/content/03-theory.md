---
type: "THEORY"
title: "Executing Queries - SELECT"
---

Use Statement or PreparedStatement:

String sql = "SELECT * FROM students WHERE age > 20";

try (Connection conn = DriverManager.getConnection(url, user, password);
     Statement stmt = conn.createStatement();
     ResultSet rs = stmt.executeQuery(sql)) {
    
    while (rs.next()) {  // Loop through results
        int id = rs.getInt("id");
        String name = rs.getString("name");
        int age = rs.getInt("age");
        
        System.out.println(id + ": " + name + ", " + age);
    }
}

KEY POINTS:
- ResultSet = cursor over query results
- rs.next() = move to next row (returns false when done)
- rs.getInt("column_name") = get value by column name
- rs.getString(1) = get by position (1-indexed)