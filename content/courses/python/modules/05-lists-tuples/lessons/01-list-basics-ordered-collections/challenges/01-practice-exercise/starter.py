# Music Playlist Manager - Starter Code

print("=== My Playlist ===")
print()

# YOUR CODE: Create a playlist list with 5 songs
playlist = [  # Fill in with song names

]

# YOUR CODE: Display statistics
print(f"Total songs: {  }")
print(f"Now playing: {  }")  # First song
print(f"Final song: {  }")   # Last song
print(f"Middle song: {  }")  # Middle song (use len(playlist)//2)

print()
print("Full Playlist:")

# YOUR CODE: Print all songs with position numbers (1-based)
for   # Use enumerate with start=1
    print(f"{  }. {  }")

print()

# YOUR CODE: Get user input for song position
position = int(input(f"Enter song position (1-{len(playlist)}): "))

# YOUR CODE: Convert to 0-based index and validate
index =   # Position 1 = index 0

if   # Check if index is valid
    print(f"Playing: {  }")
else:
    print("Invalid position!")

print()

# YOUR CODE: Calculate average title length
total_length = 0
for   # Iterate through songs
    total_length = total_length +   # Add length of each song title

average_length =   # Total length / number of songs
print(f"Average title length: {average_length:.1f} characters")