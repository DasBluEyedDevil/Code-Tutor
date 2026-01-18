---
type: "THEORY"
title: "The Security Guard's Clipboard"
---

Imagine a security guard standing at a gate. They have a strict list of rules for who can enter:

1.  "Is your age **greater than or equal to** 18?" (`>=`)
2.  "Is your invitation code **exactly** 'VIP-2025'?" (`===`)
3.  "Is your name **NOT** on the 'Banned' list?" (`!==`)

Every rule is a **Comparison**. The guard doesn't care about your life story; they only care about a "Yes" or "No" answer to those specific questions.

Comparison operators are the symbols we use to ask these questions. When you use one, you are performing a "test." The result of that test is always a **Boolean** (`true` or `false`).