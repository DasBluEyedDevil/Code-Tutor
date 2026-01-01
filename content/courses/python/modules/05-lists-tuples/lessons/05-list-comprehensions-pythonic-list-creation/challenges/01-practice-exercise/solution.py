# Data Processor - COMPLETE SOLUTION

print("=== Data Processor ===")
print()

# 1. Temperature Conversion
celsius = [0, 10, 20, 30, 40]
print("1. Temperature Conversion:")
print(f"   Celsius: {celsius}")

fahrenheit = [(c * 9/5) + 32 for c in celsius]
print(f"   Fahrenheit: {fahrenheit}")

print()

# 2. Price Discounts
prices = [45.00, 60.00, 25.00, 80.00, 55.00]
print("2. Price Discounts:")
print(f"   Original: {prices}")

discounted = [price * 0.8 if price > 50 else price for price in prices]
print(f"   Discounted: {discounted}")

print()

# 3. Email Cleaning
emails_raw = [' Alice@EXAMPLE.com ', 'BOB@test.COM', '  charlie@mail.org']
print("3. Email Cleaning:")
print(f"   Raw: {emails_raw}")

emails_clean = [email.strip().lower() for email in emails_raw]
print(f"   Clean: {emails_clean}")

print()

# 4. Grade Processing
scores = [85, 92, 55, 78, 95, 45, 88]
print("4. Grade Processing:")
print(f"   Scores: {scores}")

passing = [score for score in scores if score >= 60]
print(f"   Passing: {passing}")

letters = ['A' if s >= 90 else 'B' if s >= 80 else 'C' if s >= 70 else 'D' for s in passing]
print(f"   Letters: {letters}")

print()

# 5. Multiplication Table
print("5. Multiplication Table (5x5):")

table = [[row * col for col in range(1, 6)] for row in range(1, 6)]
for row in table:
    print(f"   {row}")

print()

# 6. Long Words
sentence = "The quick brown fox jumps over the lazy dog"
print("6. Long Words:")
print(f"   Sentence: {sentence}")

words = sentence.split()
long_words = [word.upper() for word in words if len(word) > 4]
print(f"   Long words (>4): {long_words}")

print()

# 7. Number Data Tuples
print("7. Number Data:")

number_data = [(n, n**2, n**3) for n in range(1, 11)]
print(f"   {number_data}")