---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Forgetting the space after //: While //comment works, // comment is more readable.

2. Trying to nest multi-line comments: /* /* nested */ */ doesn't work. Once the computer sees */, it thinks the comment is over.

3. Not closing a multi-line comment: If you forget the */, everything after /* will be treated as a comment, and your code won't run!

4. Over-commenting: Don't comment every single line. Comment the WHY and the complex parts, not the obvious stuff.