# Script to add WARNING sections to Java Database lessons
$filePath = "C:\Users\dasbl\Downloads\Code-Tutor\content\courses\java\course.json"

# Read the entire file
$content = Get-Content $filePath -Raw

# WARNING for Lesson 3 (SQL Queries)
$lesson3Search = @'
"type": "KEY_POINT",
              "title": "Query Order Matters!",
              "content": "SQL queries are executed in this order:\n\n1. FROM - Which table(s)\n2. WHERE - Filter rows\n3. GROUP BY - Group rows\n4. HAVING - Filter groups\n5. SELECT - Choose columns\n6. ORDER BY - Sort results\n7. LIMIT - Restrict number\n\nExample query using all:\n\nSELECT age, COUNT(*) as count, AVG(gpa) as avg_gpa\nFROM students\nWHERE email IS NOT NULL\nGROUP BY age\nHAVING count > 3\nORDER BY avg_gpa DESC\nLIMIT 5;"
            }
          ],
          "challenges": []
        },
        {
          "id": "epoch-5-lesson-4",
'@

$lesson3Replace = @'
"type": "KEY_POINT",
              "title": "Query Order Matters!",
              "content": "SQL queries are executed in this order:\n\n1. FROM - Which table(s)\n2. WHERE - Filter rows\n3. GROUP BY - Group rows\n4. HAVING - Filter groups\n5. SELECT - Choose columns\n6. ORDER BY - Sort results\n7. LIMIT - Restrict number\n\nExample query using all:\n\nSELECT age, COUNT(*) as count, AVG(gpa) as avg_gpa\nFROM students\nWHERE email IS NOT NULL\nGROUP BY age\nHAVING count > 3\nORDER BY avg_gpa DESC\nLIMIT 5;"
            },
            {
              "type": "WARNING",
              "title": "Query Performance Pitfalls",
              "content": "1. AVOID SELECT * ON LARGE TABLES:\n   - Fetches all columns, wastes memory\n   - Specify only needed columns\n\n2. MISSING INDEXES:\n   - Queries on non-indexed columns are SLOW\n   - Add indexes on columns used in WHERE, ORDER BY, JOIN\n\n3. LIKE WITH LEADING WILDCARD:\n   - LIKE '%text' cannot use indexes = full table scan\n   - LIKE 'text%' CAN use indexes = faster\n\n4. NULL COMPARISONS:\n   - WHERE column = NULL never works!\n   - Use WHERE column IS NULL instead\n\n5. AGGREGATES WITHOUT GROUP BY:\n   - COUNT(*), SUM(), AVG() without GROUP BY = one result for entire table\n   - Easily missed mistake in complex queries"
            }
          ],
          "challenges": []
        },
        {
          "id": "epoch-5-lesson-4",
'@

# WARNING for Lesson 4 (JOINs)
$lesson4Search = @'
"type": "KEY_POINT",
              "title": "JOINs are Like Matching Puzzle Pieces",
'@

$lesson4Replace = @'
"type": "WARNING",
              "title": "JOIN Gotchas to Avoid",
              "content": "1. CARTESIAN PRODUCT (Accidental Cross Join):\n   - Forgetting ON clause = rows multiply!\n   - 100 x 100 = 10,000 rows unexpectedly\n\n2. WRONG JOIN TYPE:\n   - INNER when you need LEFT = missing rows\n   - LEFT when you need INNER = unexpected NULLs\n\n3. AMBIGUOUS COLUMNS:\n   - SELECT name FROM A JOIN B = Error if both have 'name'\n   - Always use table prefixes: SELECT A.name, B.name\n\n4. N+1 QUERY PROBLEM:\n   - Looping through results and querying for each = SLOW\n   - Use a single JOIN instead of multiple queries\n\n5. JOINING ON WRONG COLUMNS:\n   - Always join PRIMARY KEY to FOREIGN KEY\n   - Verify column types match (INT to INT, not INT to VARCHAR)"
            },
            {
              "type": "KEY_POINT",
              "title": "JOINs are Like Matching Puzzle Pieces",
'@

# WARNING for Lesson 5 (JDBC) - add a formal WARNING section after the SQL Injection THEORY
$lesson5Search = @'
"type": "KEY_POINT",
              "title": "JDBC Best Practices",
              "content": "1. ALWAYS USE TRY-WITH-RESOURCES
'@

$lesson5Replace = @'
"type": "WARNING",
              "title": "Critical JDBC Security and Resource Management",
              "content": "SECURITY RISKS:\n- SQL Injection was the #1 web vulnerability in 2023\n- NEVER concatenate user input into SQL strings\n- ALWAYS use PreparedStatement with ? placeholders\n\nRESOURCE LEAKS:\n- Unclosed connections exhaust database pool\n- Always use try-with-resources (Java 7+)\n- Close in order: ResultSet, Statement, Connection\n\nCONNECTION POOL EXHAUSTION:\n- Creating connections is expensive (100-500ms each)\n- Use HikariCP or similar pool in production\n- Set reasonable pool size (10-20 connections typical)\n\nCREDENTIAL EXPOSURE:\n- Never hardcode database passwords in source code\n- Use environment variables or secrets management\n- Rotate credentials regularly"
            },
            {
              "type": "KEY_POINT",
              "title": "JDBC Best Practices",
              "content": "1. ALWAYS USE TRY-WITH-RESOURCES
'@

# Apply replacements
if ($content.Contains("Query Performance Pitfalls")) {
    Write-Host "Lesson 3 WARNING already exists"
} else {
    $content = $content.Replace($lesson3Search, $lesson3Replace)
    Write-Host "Added WARNING to Lesson 3"
}

if ($content.Contains("JOIN Gotchas to Avoid")) {
    Write-Host "Lesson 4 WARNING already exists"
} else {
    $content = $content.Replace($lesson4Search, $lesson4Replace)
    Write-Host "Added WARNING to Lesson 4"
}

if ($content.Contains("Critical JDBC Security")) {
    Write-Host "Lesson 5 WARNING already exists"
} else {
    $content = $content.Replace($lesson5Search, $lesson5Replace)
    Write-Host "Added WARNING to Lesson 5"
}

# Write back without BOM
[System.IO.File]::WriteAllText($filePath, $content)
Write-Host "File updated successfully"
