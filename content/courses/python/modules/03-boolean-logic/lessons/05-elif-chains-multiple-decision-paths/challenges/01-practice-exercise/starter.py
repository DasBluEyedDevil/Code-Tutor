# BMI Calculator with Category Classification
# Calculate BMI and categorize into health ranges

print("=== BMI Calculator ===")
print()

# Get user input
weight_kg = float(input("Enter your weight (kg): "))
height_m = float(input("Enter your height (meters): "))

print()

# YOUR CODE HERE:
# Calculate BMI
bmi = 

# Categorize using elif chain
# Order matters! Most specific to most general

if :  # Underweight: BMI < 18.5
    category = "Underweight"
    recommendation = "Consider consulting a healthcare provider."
elif :  # Normal: BMI < 25 (automatically means >= 18.5)
    category = 
    recommendation = 
elif :  # Overweight: BMI < 30
    category = 
    recommendation = 
else:  # Obese: BMI >= 30
    category = 
    recommendation = 

# Display results
print(f"Your BMI: {bmi:.1f}")
print(f"Category: {category}")
print(f"Recommendation: {recommendation}")