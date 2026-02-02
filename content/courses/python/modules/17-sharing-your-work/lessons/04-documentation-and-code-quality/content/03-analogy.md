---
type: "ANALOGY"
title: "Code Review Checklist"
---

**Code review = Quality gate before merging**

**What to look for:**

**1. Functionality** ✅
- Does it work as intended?
- Are edge cases handled?
- Are errors handled properly?

**2. Tests** 🧪
- Are there tests?
- Do tests cover edge cases?
- Do all tests pass?

**3. Code Quality** 💎
- Is code readable?
- Are names descriptive?
- Is logic clear?
- DRY (Don't Repeat Yourself)?

**4. Documentation** 📚
- Are functions documented?
- Is README updated?
- Are comments helpful?

**5. Security** 🔒
- No hardcoded secrets?
- Input validation?
- SQL injection prevention?

**6. Performance** ⚡
- Efficient algorithms?
- No unnecessary loops?
- Database queries optimized?

**Review comments:**

**Good:**
- "Consider using a dict here for O(1) lookup"
- "Great job handling this edge case!"
- "Could we add a test for the error case?"

**Bad:**
- "This is wrong"
- "Why did you do it this way?"
- "Fix this"