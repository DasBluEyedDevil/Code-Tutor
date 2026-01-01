# BMI Calculator with Category Classification - SOLUTION
# Calculate BMI and categorize into health ranges

print("=== BMI Calculator ===")
print()

# Get user input
weight_kg = float(input("Enter your weight (kg): "))
height_m = float(input("Enter your height (meters): "))

print()

# Calculate BMI
bmi = weight_kg / (height_m ** 2)

# Categorize using elif chain
if bmi < 18.5:
    category = "Underweight"
    recommendation = "Consider consulting a healthcare provider."
elif bmi < 25:
    category = "Normal weight"
    recommendation = "Maintain your current lifestyle!"
elif bmi < 30:
    category = "Overweight"
    recommendation = "Consider a balanced diet and exercise."
else:
    category = "Obese"
    recommendation = "Please consult a healthcare provider for guidance."

# Display results
print(f"Your BMI: {bmi:.1f}")
print(f"Category: {category}")
print(f"Recommendation: {recommendation}")