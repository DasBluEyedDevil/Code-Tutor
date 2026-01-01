---
type: "ANALOGY"
title: "Understanding Authorization"
---

Imagine visiting a theme park where different wristbands grant access to different attractions. A basic wristband lets you enjoy the main rides and shows. A premium wristband adds access to VIP lounges and front-of-line privileges. A staff wristband opens doors to employee-only areas, maintenance tunnels, and control rooms. Each wristband color represents a ROLE with specific permissions.

But theme parks have evolved beyond simple wristband colors. Modern parks issue smart bands that encode CLAIMS - specific facts about you. Your smart band might contain claims like 'height: 140cm' (allowing access to certain roller coasters), 'age: 25' (permitting entry to adult-only venues), 'paid-fastpass: true' (enabling queue skipping), and 'VIP-experience: fireworks-viewing' (granting access to exclusive areas during the show).

Now imagine the security guard at each attraction. They don't just check your wristband color - they evaluate POLICIES. The roller coaster policy might be: 'Allow if (height >= 120cm AND age >= 10) OR (has VIP wristband AND signed waiver)'. This policy combines multiple claims and roles into a single access decision.

Resource-based authorization goes even further. The gift shop policy for employee discounts isn't just 'is employee' - it's 'is employee AND this specific purchase is from their assigned department'. The authorization decision depends on both WHO you are and WHAT you're trying to access.

In software, authentication answers 'Who are you?' but authorization answers 'What are you allowed to do?' Just like the theme park, we layer roles, claims, and policies to create flexible, maintainable access control systems.