---
type: "THEORY"
title: "Part 2 Capstone Project: Contact Management System"
---


Now it's time to put everything together! You'll build a complete contact management system using all the concepts from Part 2.

### Project Requirements

Build a console application that manages contacts with these features:

1. **Add Contact**: Store name, phone, and email
2. **View All Contacts**: Display all contacts
3. **Search Contact**: Find by name
4. **Update Contact**: Modify phone or email
5. **Delete Contact**: Remove a contact
6. **Statistics**: Show total contacts, contacts with/without email
7. **Menu System**: User-friendly interface with loops

**Concepts used:**
- ✅ If statements (validation)
- ✅ When expressions (menu choices)
- ✅ For loops (displaying contacts)
- ✅ While/do-while loops (menu loop)
- ✅ Lists (managing multiple fields)
- ✅ Maps (storing contacts)

### Capstone Solution

<details>
<summary>Click to see complete solution</summary>


**Sample Run:**

**Key features:**
- ✅ Data class for structured contact info
- ✅ Input validation
- ✅ Error handling
- ✅ User-friendly messages with emojis
- ✅ Confirmation for destructive actions
- ✅ Smart search with suggestions
- ✅ Comprehensive statistics
- ✅ Clean code organization with functions
</details>

### Challenge Extensions

Want to go further? Try adding:

1. **Export/Import**: Save contacts to a file
2. **Sorting**: View contacts alphabetically
3. **Groups**: Categorize contacts (family, work, friends)
4. **Favorites**: Mark important contacts
5. **Birthday tracking**: Store and remind birthdays
6. **Multiple phones**: Support home, work, mobile

---



```kotlin
╔═══════════════════════════════════╗
║  CONTACT MANAGEMENT SYSTEM v1.0   ║
╚═══════════════════════════════════╝

=== MAIN MENU ===
1. Add Contact
2. View All Contacts
3. Search Contact
4. Update Contact
5. Delete Contact
6. Statistics
7. Exit

Enter choice (1-7): 1

=== ADD NEW CONTACT ===
Enter name: Alice
Enter phone: 555-1234
Enter email (optional): alice@email.com
✅ Contact 'Alice' added successfully!

=== MAIN MENU ===
1. Add Contact
2. View All Contacts
3. Search Contact
4. Update Contact
5. Delete Contact
6. Statistics
7. Exit

Enter choice (1-7): 2

=== ALL CONTACTS (1) ===

[1] Alice
    📞 Phone: 555-1234
    📧 Email: alice@email.com

=== MAIN MENU ===
...
```
