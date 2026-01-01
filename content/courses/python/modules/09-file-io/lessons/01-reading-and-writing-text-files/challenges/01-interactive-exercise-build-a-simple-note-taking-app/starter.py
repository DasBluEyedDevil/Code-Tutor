def write_note(note):
    """Write a new note (overwrites existing notes)."""
    # TODO: Open notes.txt in WRITE mode
    # TODO: Write the note (add \n at the end)
    # TODO: Close the file
    pass

def append_note(note):
    """Add a note to existing notes."""
    # TODO: Open notes.txt in APPEND mode
    # TODO: Write the note (add \n at the end)
    # TODO: Close the file
    pass

def read_notes():
    """Read and return all notes."""
    try:
        # TODO: Open notes.txt in READ mode
        # TODO: Read all content
        # TODO: Close the file
        # TODO: Return the content
        pass
    except FileNotFoundError:
        return "No notes found. Create a note first!"

# Test your functions
print("=== Testing Note App ===")

# Test 1: Write a new note
print("\n1. Writing first note...")
write_note("Remember to buy milk")
print("✓ Note written")

# Test 2: Read notes
print("\n2. Reading notes...")
print(read_notes())

# Test 3: Append more notes
print("3. Adding more notes...")
append_note("Call dentist tomorrow")
append_note("Finish Python homework")
print("✓ Notes added")

# Test 4: Read all notes
print("\n4. Reading all notes...")
print(read_notes())