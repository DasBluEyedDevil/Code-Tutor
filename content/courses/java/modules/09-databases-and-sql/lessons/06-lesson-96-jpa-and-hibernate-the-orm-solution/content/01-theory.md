---
type: "THEORY"
title: "The Problem: JDBC is Painful"
---

Look at this JDBC code to get a student:

String sql = "SELECT * FROM students WHERE id = ?";
try (PreparedStatement pstmt = conn.prepareStatement(sql)) {
    pstmt.setInt(1, studentId);
    try (ResultSet rs = pstmt.executeQuery()) {
        if (rs.next()) {
            Student s = new Student();
            s.setId(rs.getInt("id"));
            s.setName(rs.getString("name"));
            s.setAge(rs.getInt("age"));
            s.setEmail(rs.getString("email"));
            return s;
        }
    }
}

PROBLEMS:
- Tons of boilerplate for simple operations
- Manual mapping between ResultSet and objects
- SQL strings scattered throughout code
- Easy to make mistakes (wrong column names)
- Must handle connections, exceptions everywhere

There MUST be a better way...