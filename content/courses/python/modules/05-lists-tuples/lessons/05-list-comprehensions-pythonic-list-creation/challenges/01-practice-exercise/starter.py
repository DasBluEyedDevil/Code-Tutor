# Data Processor - Starter Code

print("=== Data Processor ===")
print()

# 1. Temperature Conversion
celsius = [0, 10, 20, 30, 40]
print("1. Temperature Conversion:")
print(f"   Celsius: {celsius}")

# YOUR CODE: Convert to Fahrenheit using comprehension
fahrenheit = [  # Formula: C * 9/5 + 32
print(f"   Fahrenheit: {fahrenheit}")

print()

# 2. Price Discounts
prices = [45.00, 60.00, 25.00, 80.00, 55.00]
print("2. Price Discounts:")
print(f"   Original: {prices}")

# YOUR CODE: Apply 20% discount to items > $50, keep others same
discounted = [  # if price > 50, multiply by 0.8, else keep same
print(f"   Discounted: {discounted}")

print()

# 3. Email Cleaning
emails_raw = [' Alice@EXAMPLE.com ', 'BOB@test.COM', '  charlie@mail.org']
print("3. Email Cleaning:")
print(f"   Raw: {emails_raw}")

# YOUR CODE: Strip whitespace and lowercase
emails_clean = [  # Use .strip() and .lower()
print(f"   Clean: {emails_clean}")

print()

# 4. Grade Processing
scores = [85, 92, 55, 78, 95, 45, 88]
print("4. Grade Processing:")
print(f"   Scores: {scores}")

# YOUR CODE: Get passing grades only (>=60)
passing = [
print(f"   Passing: {passing}")

# YOUR CODE: Convert passing grades to letters
# A: >=90, B: >=80, C: >=70, D: >=60
letters = [  # Use nested if-else
print(f"   Letters: {letters}")

print()

# 5. Multiplication Table
print("5. Multiplication Table (5x5):")

# YOUR CODE: Create 5x5 table using nested comprehension
table = [  # [[row*col for col in range(1,6)] for row in range(1,6)]
for row in table:
    print(f"   {row}")

print()

# 6. Long Words
sentence = "The quick brown fox jumps over the lazy dog"
print("6. Long Words:")
print(f"   Sentence: {sentence}")

words = sentence.split()  # Split into list of words

# YOUR CODE: Get words > 4 chars, uppercased
long_words = [
print(f"   Long words (>4): {long_words}")

print()

# 7. Number Data Tuples
print("7. Number Data:")

# YOUR CODE: Create list of (number, square, cube) tuples for 1-10
number_data = [  # (n, n**2, n**3)
print(f"   {number_data}")