---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`collection.Where(x => condition)`**: Where() tests each item. 'x' is each item (you choose the name!). Returns IEnumerable<T> with only items where condition is true.

**`Lambda: x => expression`**: Lambda is anonymous function. 'x' is parameter. '=>' is lambda operator. Expression returns bool (true/false). Read: 'x such that expression'.

**`Multiple conditions`**: Combine with && (AND), || (OR), ! (NOT). Example: 'x => x > 5 && x < 10' means 'between 5 and 10'.

**`Object properties in lambdas`**: Access properties inside lambda: 'p => p.Age > 30'. Can call methods too: 'p => p.Name.Contains("a")'.