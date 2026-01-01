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
        # TODO: Add cases for single-word commands
        # case ["north"]:
        #     return "Moving north..."
        
        # TODO: Add cases for movement with guards
        # case ["north"] if row == 0:
        #     return "You can't go that way!"
        
        # TODO: Add cases for two-word commands like ["go", direction]
        
        # TODO: Add case for quit/exit
        
        # TODO: Add wildcard case for unknown commands
        case _:
            return "I don't understand that command"

# Test the command parser
test_commands = [
    "north",
    "look",
    "inventory",
    "quit",
    "go south",
    "take sword",
    "dance"
]

print("=== Testing Command Parser ===")
for cmd in test_commands:
    print(f"Command: '{cmd}'")
    print(f"Response: {parse_command(cmd)}")
    print()