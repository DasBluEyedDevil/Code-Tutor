---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine airport security checkpoints:

Without proper checks:
- Anyone could go anywhere
- Passengers and crew mixed together
- No way to know who has what access
- Chaos and security risks

With security checkpoints (Type Guards):
- Check ID: 'Are you a passenger or crew member?'
- Once verified as crew, you can access crew areas
- Once verified as passenger, you go through different process
- Each check NARROWS DOWN who you are

Type guards work the same way in TypeScript. When you have a value that could be multiple types (like `string | number`), a type guard checks which type it actually is. Once checked, TypeScript KNOWS the specific type and gives you the right autocomplete and safety!