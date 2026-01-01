---
type: "THEORY"
title: "The Problem: Hardcoded Values Everywhere"
---

Bad code with hardcoded values:

@Service
public class EmailService {
    private String smtpHost = "smtp.gmail.com";     // Hardcoded!
    private int smtpPort = 587;                      // Hardcoded!
    private String apiKey = "abc123xyz";            // Hardcoded!
    
    public void sendEmail(String to, String message) {
        // Use hardcoded values...
    }
}

PROBLEMS:
❌ Different values for dev/test/production
❌ Must recompile to change values
❌ Secrets exposed in code (security risk!)
❌ Can't configure without modifying source

Real apps need:
- Dev: localhost:3306 for database
- Test: test-server:3306
- Production: prod-server.com:3306

Solution: EXTERNAL CONFIGURATION