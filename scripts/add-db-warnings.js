const fs = require('fs');
const path = require('path');

const filePath = path.join(__dirname, '..', 'content', 'courses', 'java', 'course.json');

// Read the file
let content = fs.readFileSync(filePath, 'utf8');

// WARNING sections to add
const warnings = {
  lesson1: {
    search: /"title": "SQL - The Database Language",\s*"content": "SQL \(Structured Query Language\)[^}]+"\s*\}\s*\],\s*"challenges": \[\]/,
    replacement: (match) => {
      return match.replace(
        /\}\s*\],\s*"challenges": \[\]/,
        `},
            {
              "type": "WARNING",
              "title": "Database Operations Are Permanent",
              "content": "Unlike variables in your program, database changes are PERMANENT:\\n\\n- DELETE removes data forever (unless you have backups)\\n- UPDATE cannot be undone without restoring from backup\\n- No Ctrl+Z in production databases!\\n\\nALWAYS:\\n- Test queries on development data first\\n- Back up data before running DELETE or UPDATE\\n- Use WHERE clauses carefully - forgetting WHERE deletes ALL rows\\n- In production, use transactions so you can rollback mistakes"
            }
          ],
          "challenges": []`
      );
    }
  },
  lesson2: {
    search: /"title": "Primary Keys - The Unique Identifier"[^}]+"\s*\}\s*\],\s*"challenges": \[\]\s*\},\s*\{\s*"id": "epoch-5-lesson-3"/,
    replacement: (match) => {
      return match.replace(
        /\}\s*\],\s*"challenges": \[\]\s*\},\s*\{\s*"id": "epoch-5-lesson-3"/,
        `},
            {
              "type": "WARNING",
              "title": "Common SQL Mistakes to Avoid",
              "content": "1. FORGETTING NOT NULL:\\n   - Fields without NOT NULL can have empty values\\n   - Required fields should always have NOT NULL\\n\\n2. WRONG DATA TYPES:\\n   - Using VARCHAR for numbers = slow sorting, bad comparisons\\n   - Using FLOAT for money = rounding errors (use DECIMAL instead)\\n\\n3. DUPLICATE DATA:\\n   - Storing same data in multiple tables = inconsistency\\n   - Use foreign keys and JOINs instead\\n\\n4. NO PRIMARY KEY:\\n   - Every table needs a PRIMARY KEY\\n   - Without it, you cannot uniquely identify rows\\n\\n5. SELECT * IN PRODUCTION:\\n   - Retrieves all columns, even unused ones\\n   - Wastes bandwidth and memory\\n   - Always specify needed columns: SELECT name, age FROM..."
            }
          ],
          "challenges": []
        },
        {
          "id": "epoch-5-lesson-3"`
      );
    }
  },
  lesson3: {
    search: /"title": "Query Order Matters!"[^}]+"\s*\}\s*\],\s*"challenges": \[\]\s*\},\s*\{\s*"id": "epoch-5-lesson-4"/,
    replacement: (match) => {
      return match.replace(
        /\}\s*\],\s*"challenges": \[\]\s*\},\s*\{\s*"id": "epoch-5-lesson-4"/,
        `},
            {
              "type": "WARNING",
              "title": "Query Performance Pitfalls",
              "content": "1. AVOID SELECT * ON LARGE TABLES:\\n   - Fetches all columns, wastes memory\\n   - Specify only needed columns\\n\\n2. MISSING INDEXES:\\n   - Queries on non-indexed columns are SLOW\\n   - Add indexes on columns used in WHERE, ORDER BY, JOIN\\n\\n3. LIKE WITH LEADING WILDCARD:\\n   - LIKE '%text' cannot use indexes = full table scan\\n   - LIKE 'text%' CAN use indexes = faster\\n\\n4. NULL COMPARISONS:\\n   - WHERE column = NULL never works!\\n   - Use WHERE column IS NULL instead\\n\\n5. AGGREGATES WITHOUT GROUP BY:\\n   - COUNT(*), SUM(), AVG() without GROUP BY = one result for entire table\\n   - Easily missed mistake in complex queries"
            }
          ],
          "challenges": []
        },
        {
          "id": "epoch-5-lesson-4"`
      );
    }
  },
  lesson4: {
    search: /"title": "Many-to-Many Relationships"[^}]+"\s*\}\s*\],\s*"challenges": \[\]\s*\},\s*\{\s*"id": "epoch-5-lesson-5"/,
    replacement: (match) => {
      return match.replace(
        /\}\s*\],\s*"challenges": \[\]\s*\},\s*\{\s*"id": "epoch-5-lesson-5"/,
        `},
            {
              "type": "WARNING",
              "title": "JOIN Gotchas to Avoid",
              "content": "1. CARTESIAN PRODUCT (Accidental Cross Join):\\n   - Forgetting ON clause = rows multiply!\\n   - 100 x 100 = 10,000 rows unexpectedly\\n\\n2. WRONG JOIN TYPE:\\n   - INNER when you need LEFT = missing rows\\n   - LEFT when you need INNER = unexpected NULLs\\n\\n3. AMBIGUOUS COLUMNS:\\n   - SELECT name FROM A JOIN B = Error if both have 'name'\\n   - Always use table prefixes: SELECT A.name, B.name\\n\\n4. N+1 QUERY PROBLEM:\\n   - Looping through results and querying for each = SLOW\\n   - Use a single JOIN instead of multiple queries\\n\\n5. JOINING ON WRONG COLUMNS:\\n   - Always join PRIMARY KEY to FOREIGN KEY\\n   - Verify column types match (INT to INT, not INT to VARCHAR)"
            }
          ],
          "challenges": []
        },
        {
          "id": "epoch-5-lesson-5"`
      );
    }
  },
  lesson5: {
    search: /"title": "JDBC Best Practices"[^}]+"\s*\}\s*\],\s*"challenges": \[\]\s*\}\s*\]\s*\},\s*\{\s*"id": "module-07"/,
    replacement: (match) => {
      return match.replace(
        /"title": "JDBC Best Practices"/,
        `"type": "WARNING",
              "title": "Critical JDBC Security and Resource Management",
              "content": "SECURITY RISKS:\\n- SQL Injection was the #1 web vulnerability in 2023\\n- NEVER concatenate user input into SQL strings\\n- ALWAYS use PreparedStatement with ? placeholders\\n\\nRESOURCE LEAKS:\\n- Unclosed connections exhaust database pool\\n- Always use try-with-resources (Java 7+)\\n- Close in order: ResultSet, Statement, Connection\\n\\nCONNECTION POOL EXHAUSTION:\\n- Creating connections is expensive (100-500ms each)\\n- Use HikariCP or similar pool in production\\n- Set reasonable pool size (10-20 connections typical)\\n\\nCREDENTIAL EXPOSURE:\\n- Never hardcode database passwords in source code\\n- Use environment variables or secrets management\\n- Rotate credentials regularly"
            },
            {
              "type": "KEY_POINT",
              "title": "JDBC Best Practices"`
      );
    }
  }
};

// Check for existing warnings and apply changes
let modified = false;

if (!content.includes('Database Operations Are Permanent')) {
  const result = content.replace(
    /"title": "SQL - The Database Language",[\s\S]*?"challenges": \[\]\s*\},\s*\{\s*"id": "epoch-5-lesson-2"/,
    (match) => {
      if (match.includes('"type": "WARNING"')) return match;
      return match.replace(
        /\}\s*\],\s*"challenges": \[\]\s*\},\s*\{\s*"id": "epoch-5-lesson-2"/,
        `},
            {
              "type": "WARNING",
              "title": "Database Operations Are Permanent",
              "content": "Unlike variables in your program, database changes are PERMANENT:\\n\\n- DELETE removes data forever (unless you have backups)\\n- UPDATE cannot be undone without restoring from backup\\n- No Ctrl+Z in production databases!\\n\\nALWAYS:\\n- Test queries on development data first\\n- Back up data before running DELETE or UPDATE\\n- Use WHERE clauses carefully - forgetting WHERE deletes ALL rows\\n- In production, use transactions so you can rollback mistakes"
            }
          ],
          "challenges": []
        },
        {
          "id": "epoch-5-lesson-2"`
      );
    }
  );
  if (result !== content) {
    content = result;
    modified = true;
    console.log('Added WARNING to Lesson 1');
  }
}

if (!content.includes('Common SQL Mistakes to Avoid')) {
  const result = content.replace(
    /"title": "Primary Keys - The Unique Identifier"[\s\S]*?"challenges": \[\]\s*\},\s*\{\s*"id": "epoch-5-lesson-3"/,
    (match) => {
      if (match.includes('"type": "WARNING"')) return match;
      return match.replace(
        /\}\s*\],\s*"challenges": \[\]\s*\},\s*\{\s*"id": "epoch-5-lesson-3"/,
        `},
            {
              "type": "WARNING",
              "title": "Common SQL Mistakes to Avoid",
              "content": "1. FORGETTING NOT NULL:\\n   - Fields without NOT NULL can have empty values\\n   - Required fields should always have NOT NULL\\n\\n2. WRONG DATA TYPES:\\n   - Using VARCHAR for numbers = slow sorting, bad comparisons\\n   - Using FLOAT for money = rounding errors (use DECIMAL instead)\\n\\n3. DUPLICATE DATA:\\n   - Storing same data in multiple tables = inconsistency\\n   - Use foreign keys and JOINs instead\\n\\n4. NO PRIMARY KEY:\\n   - Every table needs a PRIMARY KEY\\n   - Without it, you cannot uniquely identify rows\\n\\n5. SELECT * IN PRODUCTION:\\n   - Retrieves all columns, even unused ones\\n   - Wastes bandwidth and memory\\n   - Always specify needed columns: SELECT name, age FROM..."
            }
          ],
          "challenges": []
        },
        {
          "id": "epoch-5-lesson-3"`
      );
    }
  );
  if (result !== content) {
    content = result;
    modified = true;
    console.log('Added WARNING to Lesson 2');
  }
}

if (!content.includes('Query Performance Pitfalls')) {
  const result = content.replace(
    /"title": "Query Order Matters!"[\s\S]*?"challenges": \[\]\s*\},\s*\{\s*"id": "epoch-5-lesson-4"/,
    (match) => {
      if (match.includes('"type": "WARNING"')) return match;
      return match.replace(
        /\}\s*\],\s*"challenges": \[\]\s*\},\s*\{\s*"id": "epoch-5-lesson-4"/,
        `},
            {
              "type": "WARNING",
              "title": "Query Performance Pitfalls",
              "content": "1. AVOID SELECT * ON LARGE TABLES:\\n   - Fetches all columns, wastes memory\\n   - Specify only needed columns\\n\\n2. MISSING INDEXES:\\n   - Queries on non-indexed columns are SLOW\\n   - Add indexes on columns used in WHERE, ORDER BY, JOIN\\n\\n3. LIKE WITH LEADING WILDCARD:\\n   - LIKE '%text' cannot use indexes = full table scan\\n   - LIKE 'text%' CAN use indexes = faster\\n\\n4. NULL COMPARISONS:\\n   - WHERE column = NULL never works!\\n   - Use WHERE column IS NULL instead\\n\\n5. AGGREGATES WITHOUT GROUP BY:\\n   - COUNT(*), SUM(), AVG() without GROUP BY = one result for entire table\\n   - Easily missed mistake in complex queries"
            }
          ],
          "challenges": []
        },
        {
          "id": "epoch-5-lesson-4"`
      );
    }
  );
  if (result !== content) {
    content = result;
    modified = true;
    console.log('Added WARNING to Lesson 3');
  }
}

if (!content.includes('JOIN Gotchas to Avoid')) {
  const result = content.replace(
    /"title": "Many-to-Many Relationships"[\s\S]*?"challenges": \[\]\s*\},\s*\{\s*"id": "epoch-5-lesson-5"/,
    (match) => {
      if (match.includes('"type": "WARNING"')) return match;
      return match.replace(
        /\}\s*\],\s*"challenges": \[\]\s*\},\s*\{\s*"id": "epoch-5-lesson-5"/,
        `},
            {
              "type": "WARNING",
              "title": "JOIN Gotchas to Avoid",
              "content": "1. CARTESIAN PRODUCT (Accidental Cross Join):\\n   - Forgetting ON clause = rows multiply!\\n   - 100 x 100 = 10,000 rows unexpectedly\\n\\n2. WRONG JOIN TYPE:\\n   - INNER when you need LEFT = missing rows\\n   - LEFT when you need INNER = unexpected NULLs\\n\\n3. AMBIGUOUS COLUMNS:\\n   - SELECT name FROM A JOIN B = Error if both have 'name'\\n   - Always use table prefixes: SELECT A.name, B.name\\n\\n4. N+1 QUERY PROBLEM:\\n   - Looping through results and querying for each = SLOW\\n   - Use a single JOIN instead of multiple queries\\n\\n5. JOINING ON WRONG COLUMNS:\\n   - Always join PRIMARY KEY to FOREIGN KEY\\n   - Verify column types match (INT to INT, not INT to VARCHAR)"
            }
          ],
          "challenges": []
        },
        {
          "id": "epoch-5-lesson-5"`
      );
    }
  );
  if (result !== content) {
    content = result;
    modified = true;
    console.log('Added WARNING to Lesson 4');
  }
}

if (!content.includes('Critical JDBC Security')) {
  const result = content.replace(
    /(\{\s*"type": "KEY_POINT",\s*"title": "JDBC Best Practices")/,
    `{
              "type": "WARNING",
              "title": "Critical JDBC Security and Resource Management",
              "content": "SECURITY RISKS:\\n- SQL Injection was the #1 web vulnerability in 2023\\n- NEVER concatenate user input into SQL strings\\n- ALWAYS use PreparedStatement with ? placeholders\\n\\nRESOURCE LEAKS:\\n- Unclosed connections exhaust database pool\\n- Always use try-with-resources (Java 7+)\\n- Close in order: ResultSet, Statement, Connection\\n\\nCONNECTION POOL EXHAUSTION:\\n- Creating connections is expensive (100-500ms each)\\n- Use HikariCP or similar pool in production\\n- Set reasonable pool size (10-20 connections typical)\\n\\nCREDENTIAL EXPOSURE:\\n- Never hardcode database passwords in source code\\n- Use environment variables or secrets management\\n- Rotate credentials regularly"
            },
            {
              "type": "KEY_POINT",
              "title": "JDBC Best Practices"`
  );
  if (result !== content) {
    content = result;
    modified = true;
    console.log('Added WARNING to Lesson 5');
  }
}

if (modified) {
  fs.writeFileSync(filePath, content, 'utf8');
  console.log('File saved successfully');
} else {
  console.log('No changes needed - all warnings already exist');
}
