---
type: "THEORY"
title: "JDBC Connection Steps"
---

1. LOAD DRIVER (usually automatic now)
2. ESTABLISH CONNECTION
3. CREATE STATEMENT
4. EXECUTE QUERY
5. PROCESS RESULTS
6. CLOSE RESOURCES

Code example:

import java.sql.*;

public class DatabaseExample {
    public static void main(String[] args) {
        String url = "jdbc:mysql://localhost:3306/school";
        String user = "root";
        String password = "password";
        
        try (Connection conn = DriverManager.getConnection(url, user, password)) {
            // Connection established!
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}

try-with-resources automatically closes connection!