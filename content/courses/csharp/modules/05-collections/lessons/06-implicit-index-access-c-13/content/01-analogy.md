---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine numbering seats in a theater from both ends. Seat 1 from the front is easy, but what about 'the last seat' or 'second from the end'? Instead of counting all seats first, you'd love to just say 'last seat' or 'seat -2 from end'!

C# 8 introduced the 'from end' operator: ^1 means 'last item', ^2 means 'second to last', etc. But there was a limitation - you couldn't use this in object initializers!

C# 13 fixes this! Now you can use ^index directly when initializing collections in object initializers. This is perfect for:
- Setting values at the END of arrays without knowing the exact size
- Initializing collections in reverse order
- Creating patterns that work from both ends

Think of it as finally being able to say 'fill in the last 3 seats' while setting up a seating chart!