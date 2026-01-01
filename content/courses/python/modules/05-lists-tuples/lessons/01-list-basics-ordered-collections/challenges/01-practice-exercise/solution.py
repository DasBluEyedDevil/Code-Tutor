# Music Playlist Manager - COMPLETE SOLUTION

print("=== My Playlist ===")
print()

# Create a playlist list with 5 songs
playlist = [
    "Bohemian Rhapsody",
    "Stairway to Heaven",
    "Hotel California",
    "Imagine",
    "Sweet Child O' Mine"
]

# Display statistics
print(f"Total songs: {len(playlist)}")
print(f"Now playing: {playlist[0]}")  # First song
print(f"Final song: {playlist[-1]}")   # Last song
print(f"Middle song: {playlist[len(playlist)//2]}")  # Middle song

print()
print("Full Playlist:")

# Print all songs with position numbers (1-based)
for position, song in enumerate(playlist, start=1):
    print(f"{position}. {song}")

print()

# Get user input for song position
position = int(input(f"Enter song position (1-{len(playlist)}): "))

# Convert to 0-based index and validate
index = position - 1  # Position 1 = index 0

if 0 <= index < len(playlist):
    print(f"Playing: {playlist[index]}")
else:
    print("Invalid position!")

print()

# Calculate average title length
total_length = 0
for song in playlist:
    total_length = total_length + len(song)

average_length = total_length / len(playlist)
print(f"Average title length: {average_length:.1f} characters")