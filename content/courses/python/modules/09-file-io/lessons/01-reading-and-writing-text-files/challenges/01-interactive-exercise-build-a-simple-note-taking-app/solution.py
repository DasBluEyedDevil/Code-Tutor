# Simple Note-Taking App
# This solution demonstrates basic file I/O operations

def write_note(note):
    """Write a new note (overwrites existing notes)."""
    # Step 1: Open file in write mode ('w')
    file = open('notes.txt', 'w')
    # Step 2: Write the note with newline
    file.write(note + '\n')
    # Step 3: Close the file to save changes
    file.close()

def append_note(note):
    """Add a note to existing notes."""
    # Step 1: Open file in append mode ('a')
    file = open('notes.txt', 'a')
    # Step 2: Write the note with newline
    file.write(note + '\n')
    # Step 3: Close the file
    file.close()

def read_notes():
    """Read and return all notes."""
    try:
        # Step 1: Open file in read mode ('r')
        file = open('notes.txt', 'r')
        # Step 2: Read all content
        content = file.read()
        # Step 3: Close the file
        file.close()
        # Step 4: Return the content
        return content
    except FileNotFoundError:
        # Handle case when file doesn't exist
        return "No notes found. Create a note first!"

# Test the note-taking app
print("=== Testing Note App ===")

# Test 1: Write a new note
print("\n1. Writing first note...")
write_note("Remember to buy milk")
print("Note written")

# Test 2: Read notes
print("\n2. Reading notes...")
print(read_notes())

# Test 3: Append more notes
print("3. Adding more notes...")
append_note("Call dentist tomorrow")
append_note("Finish Python homework")
print("Notes added")

# Test 4: Read all notes
print("\n4. Reading all notes...")
print(read_notes())