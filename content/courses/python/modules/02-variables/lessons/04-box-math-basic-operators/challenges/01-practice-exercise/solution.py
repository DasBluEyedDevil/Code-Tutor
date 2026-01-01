# Time Converter Program - SOLUTION
# Convert total minutes to hours and minutes

# Get input from user
total_minutes = int(input("Enter total minutes: "))

# Calculate hours using floor division (//)
hours = total_minutes // 60

# Calculate remaining minutes using modulo (%)
minutes = total_minutes % 60

# Display the result
print(f"That's {hours} hours and {minutes} minutes")