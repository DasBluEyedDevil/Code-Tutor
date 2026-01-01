---
type: "THEORY"
title: "The Problem: Finding Data by Index is Limiting"
---

With ArrayList, you access by INDEX:

ArrayList<String> students = new ArrayList<>();
students.add("Alice");  // index 0
students.add("Bob");    // index 1

To find Bob, you need to:
- Loop through entire list
- Check each name
- Remember which index was Bob

Real-world scenarios:
- Look up a phone number by NAME (not position)
- Find product price by SKU code
- Get user profile by username

You need: KEY → VALUE lookup

Solution: HashMap!