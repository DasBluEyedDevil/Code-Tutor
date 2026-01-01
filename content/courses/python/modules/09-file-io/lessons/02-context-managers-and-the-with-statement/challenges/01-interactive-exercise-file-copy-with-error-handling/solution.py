# File Copy with Error Handling
# This solution demonstrates the 'with' statement for safe file handling

def copy_file(source, destination):
    """Copy file with header using context managers."""
    try:
        # Step 1: Open source file and read content
        with open(source, 'r') as src_file:
            content = src_file.read()
        
        # Step 2: Open destination file and write with header
        with open(destination, 'w') as dst_file:
            # Write the header line
            dst_file.write(f"--- Copy of {source} ---\n")
            # Write the original content
            dst_file.write(content)
        
        print(f"Successfully copied '{source}' to '{destination}'")
        return True
    
    except FileNotFoundError:
        print(f"Error: Source file '{source}' not found")
        return False

# Test the copy function
print("=== Testing File Copy ===")

# Create source file
with open("original.txt", "w") as f:
    f.write("This is the original content.\n")
    f.write("It has multiple lines.\n")
    f.write("All should be copied.\n")

print("Created source file\n")

# Test 1: Copy existing file
print("Test 1: Copy existing file")
if copy_file("original.txt", "copy.txt"):
    with open("copy.txt", "r") as f:
        print("Copied content:")
        print(f.read())

# Test 2: Copy non-existent file
print("\nTest 2: Copy non-existent file")
copy_file("missing.txt", "copy2.txt")