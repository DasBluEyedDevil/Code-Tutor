---
type: "WARNING"
title: "Common Records Pitfalls"
---

1. USING getTitle() INSTEAD OF title():
Record accessors do NOT use get prefix!
book.getTitle()  // ERROR - method not found
book.title()     // CORRECT

2. TRYING TO MODIFY RECORD FIELDS:
Records are IMMUTABLE - fields cannot be changed after creation.
book.title = "New Title";  // ERROR - cannot assign
// Create new record instead:
var updated = new Book("New Title", book.author(), book.pages());

3. RECORDS WITH MUTABLE FIELDS:
If a record contains a mutable object (List, array), the record is still logically mutable!
record Team(List<String> members) {}
// The list can be modified externally!
// Use defensive copies in compact constructor.

4. FORGETTING COMPACT CONSTRUCTOR SYNTAX:
record Age(int years) {
    public Age(int years) { ... }  // Verbose
    public Age { ... }              // Compact - preferred
}

5. RECORDS CANNOT EXTEND CLASSES:
record Point(int x, int y) extends Shape {}  // ERROR!
// But records CAN implement interfaces:
record Point(int x, int y) implements Serializable {}