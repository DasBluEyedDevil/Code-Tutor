# Safe number converter
# This solution demonstrates try/except for handling invalid input

print("Enter a number:")
user_input = "not a number"  # Simulate user input

# Step 1: Try to convert the input
try:
    # Attempt to convert user_input to an integer
    number = int(user_input)
    print("Conversion successful!")
except ValueError:
    # Step 2: Handle the error gracefully
    print(f"'{user_input}' is not a valid number. Using default value.")
    number = 0

# Step 3: Print the final result
print(f"The number is: {number}")