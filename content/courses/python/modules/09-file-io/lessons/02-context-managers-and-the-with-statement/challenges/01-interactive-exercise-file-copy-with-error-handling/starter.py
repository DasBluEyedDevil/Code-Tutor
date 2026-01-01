def copy_file(source, destination):
    """Copy file with header using context managers.
    
    Args:
        source: Source filename
        destination: Destination filename
        
    Returns:
        bool: True if successful, False if source not found
    """
    try:
        # TODO: Use with to open source file in read mode
        # TODO: Read all content from source
        
        # TODO: Use with to open destination file in write mode
        # TODO: Write header: "--- Copy of [source] ---\n"
        # TODO: Write the content from source
        
        return True
    
    except FileNotFoundError:
        print(f"Error: Source file '{source}' not found")
        return False

# Test your function
print("=== Testing File Copy ===")

# Create source file
with open("original.txt", "w") as f:
    f.write("This is the original content.\n")
    f.write("It has multiple lines.\n")
    f.write("All should be copied.\n")

print("✓ Created source file\n")

# Test 1: Copy existing file
print("Test 1: Copy existing file")
if copy_file("original.txt", "copy.txt"):
    with open("copy.txt", "r") as f:
        print("Copied content:")
        print(f.read())

# Test 2: Copy non-existent file
print("\nTest 2: Copy non-existent file")
copy_file("missing.txt", "copy2.txt")