---
type: "KEY_POINT"
title: "Dual Syntax: Modern Java 25+ vs Traditional Java 21 LTS"
---

Here's a complete comparison of the features in this lesson:

COMPACT SOURCE FILE (Java 25+, JEP 512):
void main() {
    IO.println("Hello!");
}

TRADITIONAL MAIN (Java 8-21 LTS):
public class Main {
    public static void main(String[] args) {
        System.out.println("Hello!");
    }
}

UNNAMED VARIABLES (Java 22+, JEP 456):
catch (Exception _) { ... }
for (var _ : list) { ... }

TRADITIONAL UNUSED VARIABLES (Java 8-21):
catch (Exception ignored) { ... }
for (var unused : list) { ... }

MODULE IMPORTS (Java 23+, JEP 476):
import module java.base;

TRADITIONAL IMPORTS (Java 8-21):
import java.util.List;
import java.util.Map;
import java.io.IOException;
// ... one import per class

IMPORTANT: Most enterprise codebases use Java 17 or 21 LTS. Java 25 is the newest LTS (September 2025). When you start a job, you'll likely work with traditional syntax. This course teaches modern syntax first (it's easier to learn!), but you MUST recognize both to read real-world code.