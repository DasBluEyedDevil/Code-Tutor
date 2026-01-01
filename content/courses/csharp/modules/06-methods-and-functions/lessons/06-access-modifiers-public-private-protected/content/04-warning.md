---
type: "WARNING"
title: "Common Pitfalls"
---

**Making everything public is dangerous!** `public string Password;` exposes sensitive data. Always use private fields with controlled access methods.

**Default is PRIVATE, not public!** If you forget the modifier, class members are private by default. `string name;` inside a class is private - you can't access it from outside.

**Protected is not public!** `protected` members are only accessible in the same class OR derived classes - not from unrelated code. Don't confuse it with public.

**internal vs private:** There's also `internal` (accessible within the same assembly/project). For beginners, stick to public and private until you need more control.