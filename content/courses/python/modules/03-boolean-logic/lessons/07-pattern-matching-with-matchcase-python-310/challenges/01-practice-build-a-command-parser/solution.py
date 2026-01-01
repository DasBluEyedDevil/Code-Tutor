# Command Parser using match/case (Python 3.10+)

def parse_command(command, player_position=(5, 5)):
    """Parse a text adventure game command.
    
    Args:
        command: The user's input string
        player_position: Tuple of (row, col) for the player
    
    Returns:
        String response to the command
    """
    row, col = player_position
    
    # Split command into words for complex commands
    words = command.lower().split()
    
    match words:
        # Movement commands with boundary checks using guards
        case ["north"] if row == 0:
            return "You can't go that way! (at northern boundary)"
        case ["south"] if row == 9:
            return "You can't go that way! (at southern boundary)"
        case ["east"] if col == 9:
            return "You can't go that way! (at eastern boundary)"
        case ["west"] if col == 0:
            return "You can't go that way! (at western boundary)"
        
        # Basic movement commands
        case ["north"] | ["n"]:
            return "Moving north..."
        case ["south"] | ["s"]:
            return "Moving south..."
        case ["east"] | ["e"]:
            return "Moving east..."
        case ["west"] | ["w"]:
            return "Moving west..."
        
        # Two-word movement: "go [direction]"
        case ["go", direction] if direction in ["north", "south", "east", "west"]:
            return f"Moving {direction}..."
        case ["go", _]:
            return "Go where? Use: go north/south/east/west"
        
        # Action commands
        case ["look"] | ["l"]:
            return "You look around and see a dark forest."
        case ["inventory"] | ["i"]:
            return "You are carrying: sword, torch, map"
        case ["help"] | ["h"] | ["?"]:
            return "Commands: north, south, east, west, look, inventory, take [item], quit"
        
        # Take command
        case ["take", item]:
            return f"You pick up the {item}."
        case ["take"]:
            return "Take what?"
        
        # Quit commands
        case ["quit"] | ["exit"] | ["q"]:
            return "Goodbye! Thanks for playing."
        
        # Unknown command
        case _:
            return "I don't understand that command. Type 'help' for options."

# Test the command parser
test_commands = [
    "north",
    "look",
    "inventory",
    "quit",
    "go south",
    "take sword",
    "dance",
    "help"
]

print("=== Testing Command Parser ===")
for cmd in test_commands:
    print(f"Command: '{cmd}'")
    print(f"Response: {parse_command(cmd)}")
    print()

# Test boundary conditions
print("=== Testing Boundaries ===")
print(f"At north boundary: {parse_command('north', (0, 5))}")
print(f"At south boundary: {parse_command('south', (9, 5))}")