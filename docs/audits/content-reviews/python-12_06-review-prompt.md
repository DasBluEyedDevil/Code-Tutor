# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** Decorators
- **Lesson:** Regular Expressions (ID: 12_06)
- **Difficulty:** advanced
- **Estimated Time:** 40 minutes

## Current Lesson Content

{
    "id":  "12_06",
    "contentSections":  [
                            {
                                "type":  "ANALOGY",
                                "title":  "The Concept: Pattern Matching",
                                "content":  "**Regular Expressions (regex) = Advanced find/replace**\n\n**Think of a search filter:**\n\n❌ **Simple string matching:**\n```python\nif \u0027@\u0027 in email and \u0027.\u0027 in email:\n    # Too simple!\n    pass\n```\n\n✅ **Regex pattern:**\n```python\nimport re\nif re.match(r\u0027^[\\w.-]+@[\\w.-]+\\.\\w+$\u0027, email):\n    # Precise pattern!\n    pass\n```\n\n**What regex can do:**\n\n1. **Validate** ✓\n   - Email addresses\n   - Phone numbers\n   - Passwords\n   - URLs\n\n2. **Extract** 🔍\n   - Dates from text\n   - Phone numbers from documents\n   - URLs from HTML\n\n3. **Replace** 🔄\n   - Format phone numbers\n   - Remove unwanted characters\n   - Transform text patterns\n\n4. **Split** ✂️\n   - Complex delimiters\n   - Multiple separators\n   - Conditional splitting\n\n**Common patterns:**\n- `.` - Any character (except newline)\n- `\\d` - Digit (0-9)\n- `\\w` - Word character (a-z, A-Z, 0-9, _)\n- `\\s` - Whitespace (space, tab, newline)\n- `*` - 0 or more\n- `+` - 1 or more\n- `?` - 0 or 1 (optional)\n- `[]` - Character class\n- `()` - Group\n- `^` - Start of string\n- `# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** Decorators
- **Lesson:** Regular Expressions (ID: 12_06)
- **Difficulty:** advanced
- **Estimated Time:** 40 minutes

## Current Lesson Content

 - End of string"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example: Basic Regular Expressions",
                                "content":  "**Key regex functions:**\n\n**1. re.match(pattern, string):**\n- Matches at START of string\n- Returns Match object or None\n```python\nre.match(r\"Hello\", \"Hello World\")  # Matches\nre.match(r\"World\", \"Hello World\")  # Doesn\u0027t match\n```\n\n**2. re.search(pattern, string):**\n- Searches ANYWHERE in string\n- Returns first match\n```python\nre.search(r\"World\", \"Hello World\")  # Matches\n```\n\n**3. re.findall(pattern, string):**\n- Returns list of ALL matches\n```python\nre.findall(r\"\\d+\", \"10 cats, 20 dogs\")  # [\u002710\u0027, \u002720\u0027]\n```\n\n**4. re.sub(pattern, replacement, string):**\n- Replace matches\n```python\nre.sub(r\"\\d+\", \"X\", \"10 cats\")  # \"X cats\"\n```\n\n**Raw strings (r\"\"):**\n- Use r\"\" for regex patterns\n- Prevents backslash escaping issues\n- r\"\\d\" not \"\\\\d\"",
                                "code":  "import re\n\nprint(\"=== Basic Matching ===\")\n\n# re.match() - Match at start of string\ntext = \"Hello World\"\nif re.match(r\"Hello\", text):\n    print(f\"\u0027{text}\u0027 starts with \u0027Hello\u0027\")\n\nif not re.match(r\"World\", text):\n    print(f\"\u0027{text}\u0027 does NOT start with \u0027World\u0027\")\n\n# re.search() - Find anywhere in string\nif re.search(r\"World\", text):\n    print(f\"\u0027{text}\u0027 contains \u0027World\u0027\")\n\n# re.findall() - Find all occurrences\ntext = \"The price is $10, $20, and $30\"\nprices = re.findall(r\"\\$\\d+\", text)\nprint(f\"Found prices: {prices}\")\n\nprint(\"\\n=== Character Classes ===\")\n\n# \\d - digits\ntext = \"My phone: 555-1234\"\ndigits = re.findall(r\"\\d\", text)\nprint(f\"All digits: {\u0027\u0027.join(digits)}\")\n\n# \\d+ - one or more digits\nnumbers = re.findall(r\"\\d+\", text)\nprint(f\"Number groups: {numbers}\")\n\n# \\w - word characters\ntext = \"Hello, World! 123\"\nwords = re.findall(r\"\\w+\", text)\nprint(f\"Words: {words}\")\n\n# \\s - whitespace\ntext = \"Split   on    spaces\"\nparts = re.split(r\"\\s+\", text)\nprint(f\"Split parts: {parts}\")\n\nprint(\"\\n=== Quantifiers ===\")\n\n# * - zero or more\npattern = r\"a*b\"\nprint(f\"\u0027b\u0027 matches: {bool(re.match(pattern, \u0027b\u0027))}\")\nprint(f\"\u0027ab\u0027 matches: {bool(re.match(pattern, \u0027ab\u0027))}\")\nprint(f\"\u0027aaab\u0027 matches: {bool(re.match(pattern, \u0027aaab\u0027))}\")\n\n# + - one or more\npattern = r\"a+b\"\nprint(f\"\\n\u0027b\u0027 matches a+b: {bool(re.match(pattern, \u0027b\u0027))}\")\nprint(f\"\u0027ab\u0027 matches a+b: {bool(re.match(pattern, \u0027ab\u0027))}\")\n\n# ? - zero or one (optional)\npattern = r\"colou?r\"  # Matches color or colour\nprint(f\"\\n\u0027color\u0027 matches: {bool(re.match(pattern, \u0027color\u0027))}\")\nprint(f\"\u0027colour\u0027 matches: {bool(re.match(pattern, \u0027colour\u0027))}\")\n\n# {n} - exactly n times\npattern = r\"\\d{3}\"  # Exactly 3 digits\nprint(f\"\\n\u0027123\u0027 matches \\\\d{{3}}: {bool(re.match(pattern, \u0027123\u0027))}\")\nprint(f\"\u002712\u0027 matches \\\\d{{3}}: {bool(re.match(pattern, \u002712\u0027))}\")\n\n# {n,m} - between n and m times\npattern = r\"\\d{2,4}\"  # 2 to 4 digits\nprint(f\"\\n\u002712\u0027 matches \\\\d{{2,4}}: {bool(re.match(pattern, \u002712\u0027))}\")\nprint(f\"\u0027123\u0027 matches \\\\d{{2,4}}: {bool(re.match(pattern, \u0027123\u0027))}\")\n\nprint(\"\\n=== Anchors and Boundaries ===\")\n\n# ^ - start of string\npattern = r\"^Hello\"\nprint(f\"\u0027Hello World\u0027 starts with Hello: {bool(re.match(pattern, \u0027Hello World\u0027))}\")\nprint(f\"\u0027Say Hello\u0027 starts with Hello: {bool(re.match(pattern, \u0027Say Hello\u0027))}\")\n\n# $ - end of string\npattern = r\"World$\"\nprint(f\"\\n\u0027Hello World\u0027 ends with World: {bool(re.search(pattern, \u0027Hello World\u0027))}\")\nprint(f\"\u0027World Hello\u0027 ends with World: {bool(re.search(pattern, \u0027World Hello\u0027))}\")\n\n# Combining ^ and $ - full string match\npattern = r\"^\\d{3}$\"  # Exactly 3 digits, nothing else\nprint(f\"\\n\u0027123\u0027 is exactly 3 digits: {bool(re.match(pattern, \u0027123\u0027))}\")\nprint(f\"\u00271234\u0027 is exactly 3 digits: {bool(re.match(pattern, \u00271234\u0027))}\")\n\nprint(\"\\n=== Character Classes ===\")\n\n# [abc] - any of these characters\npattern = r\"[aeiou]\"\nvowels = re.findall(pattern, \"hello world\")\nprint(f\"Vowels in \u0027hello world\u0027: {vowels}\")\n\n# [a-z] - range\npattern = r\"[a-z]+\"\nwords = re.findall(pattern, \"Hello123World456\")\nprint(f\"Lowercase words: {words}\")\n\n# [^abc] - NOT these characters\npattern = r\"[^aeiou]+\"\nconsonants = re.findall(pattern, \"hello world\")\nprint(f\"Consonant groups: {consonants}\")",
                                "language":  "python"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Syntax Breakdown",
                                "content":  "**Pattern syntax quick reference:**\n\n**Special characters:**\n```\n.   Any character (except newline)\n\\d  Digit [0-9]\n\\D  Not digit [^0-9]\n\\w  Word char [a-zA-Z0-9_]\n\\W  Not word char\n\\s  Whitespace [ \\t\\n\\r\\f\\v]\n\\S  Not whitespace\n```\n\n**Quantifiers:**\n```\n*      0 or more\n+      1 or more\n?      0 or 1 (optional)\n{3}    Exactly 3\n{2,5}  Between 2 and 5\n{2,}   2 or more\n```\n\n**Anchors:**\n```\n^   Start of string\n$   End of string\n\\b  Word boundary\n```\n\n**Groups:**\n```\n(...)   Capturing group\n(?:...)  Non-capturing group\n|       OR (alternation)\n```\n\n**Character classes:**\n```\n[abc]    Any of a, b, c\n[a-z]    Range (a through z)\n[^abc]   NOT a, b, or c\n```\n\n**Escaping special chars:**\n```\n\\.  Literal dot\n\\$  Literal dollar sign\n\\*  Literal asterisk\nUse \\ before special characters\n```"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example: Groups and Practical Patterns",
                                "content":  "**Capturing groups:**\n```python\npattern = r\"(\\d{3})-(\\d{3})-(\\d{4})\"\nmatch = re.search(pattern, \"555-123-4567\")\nmatch.group(0)  # Full match: \"555-123-4567\"\nmatch.group(1)  # First group: \"555\"\nmatch.groups()  # All groups: (\u0027555\u0027, \u0027123\u0027, \u00274567\u0027)\n```\n\n**Named groups:**\n```python\npattern = r\"(?P\u003carea\u003e\\d{3})-(?P\u003cnum\u003e\\d{3})\"\nmatch.group(\u0027area\u0027)  # By name\nmatch.groupdict()    # Dict of named groups\n```\n\n**Substitution with groups:**\n```python\n# Swap first and last name\nre.sub(r\u0027(\\w+) (\\w+)\u0027, r\u0027\\2, \\1\u0027, \"John Doe\")\n# Result: \"Doe, John\"\n```\n\n**Flags:**\n```python\nre.IGNORECASE  # Case-insensitive\nre.MULTILINE   # ^ and $ match line boundaries\nre.DOTALL      # . matches newlines too\n```",
                                "code":  "import re\n\nprint(\"=== Capturing Groups ===\")\n\n# Groups with ()\npattern = r\"(\\d{3})-(\\d{3})-(\\d{4})\"\nphone = \"555-123-4567\"\nmatch = re.search(pattern, phone)\n\nif match:\n    print(f\"Full match: {match.group(0)}\")\n    print(f\"Area code: {match.group(1)}\")\n    print(f\"Exchange: {match.group(2)}\")\n    print(f\"Number: {match.group(3)}\")\n    print(f\"All groups: {match.groups()}\")\n\n# Named groups\npattern = r\"(?P\u003carea\u003e\\d{3})-(?P\u003cexchange\u003e\\d{3})-(?P\u003cnumber\u003e\\d{4})\"\nmatch = re.search(pattern, phone)\n\nif match:\n    print(f\"\\nNamed groups:\")\n    print(f\"Area: {match.group(\u0027area\u0027)}\")\n    print(f\"Exchange: {match.group(\u0027exchange\u0027)}\")\n    print(f\"Number: {match.group(\u0027number\u0027)}\")\n    print(f\"Dict: {match.groupdict()}\")\n\nprint(\"\\n=== Email Validation ===\")\n\nemail_pattern = r\u0027^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$\u0027\n\nemails = [\n    \"user@example.com\",\n    \"test.user@domain.co.uk\",\n    \"invalid@\",\n    \"@invalid.com\",\n    \"no-at-sign.com\"\n]\n\nfor email in emails:\n    valid = bool(re.match(email_pattern, email))\n    print(f\"{email:25} {\u0027✓ Valid\u0027 if valid else \u0027✗ Invalid\u0027}\")\n\nprint(\"\\n=== URL Extraction ===\")\n\ntext = \"\"\"\nCheck out https://www.example.com and http://test.org\nAlso visit ftp://files.example.com for downloads.\n\"\"\"\n\nurl_pattern = r\u0027https?://[\\w.-]+\\.[a-zA-Z]{2,}\u0027\nurls = re.findall(url_pattern, text)\nprint(f\"Found URLs: {urls}\")\n\nprint(\"\\n=== Phone Number Formatting ===\")\n\ndef format_phone(phone):\n    \"\"\"Extract and format phone number\"\"\"\n    # Remove all non-digits\n    digits = re.sub(r\u0027\\D\u0027, \u0027\u0027, phone)\n    \n    # Format as (XXX) XXX-XXXX\n    if len(digits) == 10:\n        return f\"({digits[:3]}) {digits[3:6]}-{digits[6:]}\"\n    return phone\n\nphones = [\n    \"5551234567\",\n    \"555-123-4567\",\n    \"(555) 123-4567\",\n    \"555.123.4567\"\n]\n\nfor phone in phones:\n    formatted = format_phone(phone)\n    print(f\"{phone:20} → {formatted}\")\n\nprint(\"\\n=== Date Extraction ===\")\n\ntext = \"Meeting on 2024-01-15, deadline 03/20/2024, event: 12-25-2024\"\n\n# Multiple date formats\ndate_patterns = [\n    r\u0027\\d{4}-\\d{2}-\\d{2}\u0027,  # YYYY-MM-DD\n    r\u0027\\d{2}/\\d{2}/\\d{4}\u0027,  # MM/DD/YYYY\n    r\u0027\\d{2}-\\d{2}-\\d{4}\u0027,  # MM-DD-YYYY\n]\n\nall_dates = []\nfor pattern in date_patterns:\n    dates = re.findall(pattern, text)\n    all_dates.extend(dates)\n\nprint(f\"Dates found: {all_dates}\")\n\nprint(\"\\n=== Text Substitution ===\")\n\n# Replace multiple spaces with single space\ntext = \"Too    many     spaces\"\ncleaned = re.sub(r\u0027\\s+\u0027, \u0027 \u0027, text)\nprint(f\"Original: \u0027{text}\u0027\")\nprint(f\"Cleaned:  \u0027{cleaned}\u0027\")\n\n# Remove HTML tags\nhtml = \"\u003cp\u003eHello \u003cb\u003eWorld\u003c/b\u003e!\u003c/p\u003e\"\nplain = re.sub(r\u0027\u003c[^\u003e]+\u003e\u0027, \u0027\u0027, html)\nprint(f\"\\nHTML: {html}\")\nprint(f\"Plain: {plain}\")\n\n# Censor words\ntext = \"This is damn bad stuff\"\ncensored = re.sub(r\u0027\\b(damn|bad)\\b\u0027, \u0027***\u0027, text, flags=re.IGNORECASE)\nprint(f\"\\nOriginal: {text}\")\nprint(f\"Censored: {censored}\")\n\nprint(\"\\n=== Split with Regex ===\")\n\n# Split on multiple delimiters\ntext = \"apple;banana,cherry:date|elderberry\"\nfruits = re.split(r\u0027[;,:|\n]+\u0027, text)\nprint(f\"Fruits: {fruits}\")\n\n# Split but keep delimiter\ntext = \"Question? Answer! Statement.\"\nparts = re.split(r\u0027([.!?])\u0027, text)\nprint(f\"With punctuation: {parts}\")\n\nprint(\"\\n=== Password Validation ===\")\n\ndef validate_password(password):\n    \"\"\"Validate password strength\"\"\"\n    checks = {\n        \u0027length\u0027: len(password) \u003e= 8,\n        \u0027uppercase\u0027: bool(re.search(r\u0027[A-Z]\u0027, password)),\n        \u0027lowercase\u0027: bool(re.search(r\u0027[a-z]\u0027, password)),\n        \u0027digit\u0027: bool(re.search(r\u0027\\d\u0027, password)),\n        \u0027special\u0027: bool(re.search(r\u0027[!@#$%^\u0026*()]\u0027, password))\n    }\n    \n    return all(checks.values()), checks\n\npasswords = [\n    \"weak\",\n    \"StrongPass123!\",\n    \"NoDigits!\",\n    \"ALLUPPER123!\"\n]\n\nfor pwd in passwords:\n    valid, checks = validate_password(pwd)\n    status = \"✓ Valid\" if valid else \"✗ Invalid\"\n    print(f\"{pwd:20} {status}\")\n    if not valid:\n        failed = [k for k, v in checks.items() if not v]\n        print(f\"{\u0027\u0027:20} Missing: {\u0027, \u0027.join(failed)}\")",
                                "language":  "python"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Key Takeaways",
                                "content":  "- **re.search() finds anywhere, re.match() only at start** - Most common confusion\n- **Always use raw strings r\u0027\u0027** - Prevents backslash escaping issues\n- **\\d digit, \\w word, \\s space** - Most common character classes\n- **+ means 1+, * means 0+, ? means 0 or 1** - Quantifiers\n- **^ start, $ end** - Anchors for full string matching\n- **() creates groups** - Access with match.group(1), match.groups()\n- **Named groups: (?P\u003cname\u003e...)** - Better than numbers\n- **Compile patterns if reusing:** pattern = re.compile(r\u0027...\u0027)"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "12_06-challenge-4",
                           "title":  "Interactive Exercise",
                           "description":  "Create a log parser that extracts:\n1. Timestamp (YYYY-MM-DD HH:MM:SS)\n2. Log level (INFO, WARNING, ERROR)\n3. Message\nFrom log lines like: \"2024-01-15 10:30:45 [ERROR] Database connection failed\"",
                           "instructions":  "Create a log parser that extracts:\n1. Timestamp (YYYY-MM-DD HH:MM:SS)\n2. Log level (INFO, WARNING, ERROR)\n3. Message\nFrom log lines like: \"2024-01-15 10:30:45 [ERROR] Database connection failed\"",
                           "starterCode":  "import re\n\ndef parse_log_line(line):\n    # TODO: Create regex pattern with named groups\n    pattern = r\u0027\u0027\n    \n    # TODO: Match and extract groups\n    match = None\n    \n    if match:\n        return match.groupdict()\n    return None\n\nlogs = [\n    \"2024-01-15 10:30:45 [ERROR] Database connection failed\",\n    \"2024-01-15 10:31:12 [INFO] Application started\",\n    \"2024-01-15 10:35:22 [WARNING] High memory usage\"\n]\n\nfor log in logs:\n    parsed = parse_log_line(log)\n    print(parsed)",
                           "solution":  "import re\n\n# Log Parser with Regex\n# This solution demonstrates named groups in regular expressions\n\ndef parse_log_line(line):\n    \"\"\"Parse a log line and extract components.\n    \n    Expected format: YYYY-MM-DD HH:MM:SS [LEVEL] Message\n    \"\"\"\n    # Create regex pattern with named groups\n    pattern = r\u0027\u0027\u0027\n        (?P\u003ctimestamp\u003e\\d{4}-\\d{2}-\\d{2}\\s\\d{2}:\\d{2}:\\d{2})  # Date and time\n        \\s+                                                     # Whitespace\n        \\[(?P\u003clevel\u003e\\w+)\\]                                     # Log level in brackets\n        \\s+                                                     # Whitespace\n        (?P\u003cmessage\u003e.+)                                         # Message (rest of line)\n    \u0027\u0027\u0027\n    \n    # Match using VERBOSE flag for readable pattern\n    match = re.match(pattern, line, re.VERBOSE)\n    \n    if match:\n        return match.groupdict()\n    return None\n\n# Alternative simpler pattern (single line)\ndef parse_log_simple(line):\n    \"\"\"Simpler version without verbose mode.\"\"\"\n    pattern = r\u0027(?P\u003ctimestamp\u003e[\\d-]+\\s[\\d:]+)\\s\\[(?P\u003clevel\u003e\\w+)\\]\\s(?P\u003cmessage\u003e.+)\u0027\n    match = re.match(pattern, line)\n    return match.groupdict() if match else None\n\n# Test logs\nlogs = [\n    \"2024-01-15 10:30:45 [ERROR] Database connection failed\",\n    \"2024-01-15 10:31:12 [INFO] Application started\",\n    \"2024-01-15 10:35:22 [WARNING] High memory usage\",\n    \"2024-01-15 11:00:00 [DEBUG] Processing request #42\"\n]\n\nprint(\"=== Log Parser Demo ===\")\n\n# Parse and display each log\nfor log in logs:\n    parsed = parse_log_line(log)\n    if parsed:\n        print(f\"\\nTimestamp: {parsed[\u0027timestamp\u0027]}\")\n        print(f\"Level:     {parsed[\u0027level\u0027]}\")\n        print(f\"Message:   {parsed[\u0027message\u0027]}\")\n\n# Count by level\nprint(\"\\n=== Summary ===\")\nlevels = [parse_log_line(log)[\u0027level\u0027] for log in logs]\nfor level in set(levels):\n    count = levels.count(level)\n    print(f\"  {level}: {count}\")",
                           "language":  "python",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Code runs without errors",
                                                 "expectedOutput":  "",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Use named groups: (?P\u003cname\u003epattern). Timestamp: \\d{4}-\\d{2}-\\d{2} \\d{2}:\\d{2}:\\d{2}. Level: \\w+."
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Forgetting the colon after if/for/while",
                                                      "consequence":  "SyntaxError",
                                                      "correction":  "Add : at the end of the line"
                                                  },
                                                  {
                                                      "mistake":  "Using = instead of == for comparison",
                                                      "consequence":  "Assignment instead of comparison",
                                                      "correction":  "Use == for equality checks"
                                                  },
                                                  {
                                                      "mistake":  "Incorrect indentation",
                                                      "consequence":  "IndentationError",
                                                      "correction":  "Use consistent 4-space indentation"
                                                  }
                                              ],
                           "difficulty":  "beginner"
                       }
                   ],
    "difficulty":  "advanced",
    "title":  "Regular Expressions",
    "estimatedMinutes":  40
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current python documentation
- Search the web for the latest python version and verify examples work with it
- Flag any deprecated APIs, syntax, or patterns

### 2. Completeness Check
- Does the lesson cover all concepts needed for a beginner to understand this topic?
- Are there missing explanations between concepts that would confuse a learner?
- Does the lesson have:
  - [ ] A clear analogy or real-world example (ANALOGY section)
  - [ ] Theoretical explanation (THEORY section)
  - [ ] Working code example (EXAMPLE section)
  - [ ] Common mistakes to avoid (WARNING section)
  - [ ] At least one practice challenge

### 3. Freshness Check
- Search for "python Regular Expressions 2024 2025" to find latest practices
- Compare lesson content against current best practices
- Identify any outdated patterns or recommendations

### 4. Pedagogical Gap Analysis
- What prerequisite knowledge is assumed but not explained?
- What follow-up questions would a learner likely have?
- What practical applications or use cases are missing?
- Are the challenges appropriately scaffolded for the difficulty level?

## Output Format

Provide your review as structured JSON:

```json
{
  "lessonId": "12_06",
  "reviewDate": "YYYY-MM-DD",
  "overallScore": 1-10,
  "accuracy": {
    "score": 1-10,
    "issues": ["issue 1", "issue 2"],
    "recommendations": ["fix 1", "fix 2"]
  },
  "completeness": {
    "score": 1-10,
    "missingSections": ["section type needed"],
    "gaps": ["gap 1", "gap 2"],
    "recommendations": ["add X", "expand Y"]
  },
  "freshness": {
    "score": 1-10,
    "outdatedItems": ["item 1"],
    "currentVersion": "language version checked",
    "recommendations": ["update X to Y"]
  },
  "pedagogicalGaps": {
    "score": 1-10,
    "missingPrerequisites": ["concept 1"],
    "unansweredQuestions": ["question learner would have"],
    "missingUseCases": ["practical application"],
    "recommendations": ["add section on X"]
  },
  "contentLengthIssues": {
    "shortSections": [
      {"sectionTitle": "title", "currentLength": 42, "recommendation": "expand to explain X"}
    ]
  },
  "suggestedNewContent": [
    {
      "sectionType": "THEORY|EXAMPLE|WARNING|etc",
      "title": "suggested title",
      "contentOutline": "what this section should cover"
    }
  ],
  "priority": "HIGH|MEDIUM|LOW"
}
```

