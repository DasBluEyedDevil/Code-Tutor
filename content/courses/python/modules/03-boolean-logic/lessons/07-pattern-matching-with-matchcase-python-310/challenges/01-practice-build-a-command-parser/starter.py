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

# Main Game Loop
print("=== Adventure Game Command Parser ===")
print("Type 'quit' to exit.")

while True:
    user_input = input("Enter command: ")
    if user_input.lower() in ["quit", "exit"]:
        print("Goodbye!")
        break

    response = parse_command(user_input)
    print(response)
