---
type: "THEORY"
title: "Maven Project Structure"
---

Maven enforces a STANDARD structure:

my-project/
├── pom.xml                 (Project config file)
├── src/
│   ├── main/
│   │   ├── java/          (Your application code)
│   │   │   └── com/yourcompany/App.java
│   │   └── resources/     (Config files, properties)
│   └── test/
│       ├── java/          (Your test code)
│       │   └── com/yourcompany/AppTest.java
│       └── resources/     (Test resources)
└── target/                 (Compiled output - Maven creates this)

BENEFITS:
✓ Every Maven project looks the same
✓ New developers know where everything is
✓ Tools work automatically

This is "Convention Over Configuration"