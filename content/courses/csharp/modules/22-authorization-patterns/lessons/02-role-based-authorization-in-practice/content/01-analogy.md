---
type: "ANALOGY"
title: "Roles as Job Titles"
---

Imagine you work in a large corporate office building. When you were hired, Human Resources assigned you a job title: Software Developer. This title determines which doors you can access. As a Software Developer, you have a badge that opens the main entrance, your department floor, the break room, and meeting rooms. But you cannot access the server room - that requires an IT Operations title. You cannot enter the executive floor - that requires a VP or C-level title. And you certainly cannot access the security control room - only Security Personnel can enter there.

Now consider the office manager. Her job title grants her a master key that opens most doors in the building. She can enter supply closets, maintenance areas, and storage rooms that regular employees cannot access. But even she cannot open the CEO's private office or the financial records vault. Those require the specific titles of CEO or Chief Financial Officer.

The cleaning staff work the night shift. Their job title grants them access to every floor and most rooms for cleaning purposes, but they cannot log into any computers or access file cabinets. Their physical access is broad, but their data access is non-existent. Meanwhile, the IT Administrator can log into every system in the building but might not have a key to the executive restroom.

This is exactly how role-based authorization works in software. A user's role is their job title in your application. An 'Admin' role might grant access to user management and system settings. A 'Manager' role opens department reports and team oversight features. A 'Customer' role provides shopping and order history access. Each role bundles together a set of permissions that make sense for that job function.

The beauty of roles is their simplicity. You do not need to track hundreds of individual permissions per user. Instead, you assign one or two roles, and the permissions follow automatically. When someone gets promoted from Support Agent to Support Supervisor, you change their role rather than updating fifty individual permission flags. When a new feature launches, you add its permission to the appropriate roles, and everyone in those roles gains access immediately.