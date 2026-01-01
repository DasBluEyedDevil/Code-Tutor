---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
=== Nested Dictionary Structure ===
{'alice': {'age': 16, 'grades': {'math': 95, 'science': 88}}, 'bob': {'age': 17, 'grades': {'math': 78, 'science': 85}}}

=== Accessing Nested Data ===
Alice's age: 16
Alice's math grade: 95
Bob's science grade: 85

=== Looping Through Nested Data ===

alice (age 16):
  math: 95
  science: 88

bob (age 17):
  math: 78
  science: 85

=== Modifying Nested Data ===
Before: {'math': 95, 'science': 88}
After: {'math': 95, 'science': 88, 'english': 92}

=== Complex Real-World Example ===

API Response Data:
  User: alice_dev
  Name: Alice Johnson
  Repos:
    - python-utils (Python) - 45 stars
    - web-app (JavaScript) - 12 stars
    - data-analysis (Python) - 78 stars
```

```python
# Nested Dictionaries and Complex Data

print("=== Nested Dictionary Structure ===")

students = {
    "alice": {
        "age": 16,
        "grades": {"math": 95, "science": 88}
    },
    "bob": {
        "age": 17,
        "grades": {"math": 78, "science": 85}
    }
}

print(students)

print("\n=== Accessing Nested Data ===")

# Chain keys to access nested values
alice_age = students["alice"]["age"]
alice_math = students["alice"]["grades"]["math"]
bob_science = students["bob"]["grades"]["science"]

print(f"Alice's age: {alice_age}")
print(f"Alice's math grade: {alice_math}")
print(f"Bob's science grade: {bob_science}")

print("\n=== Looping Through Nested Data ===")

for student_name, student_data in students.items():
    print(f"\n{student_name} (age {student_data['age']}):")
    for subject, score in student_data["grades"].items():
        print(f"  {subject}: {score}")

print("\n=== Modifying Nested Data ===")

# Add a new grade for Alice
print(f"Before: {students['alice']['grades']}")
students["alice"]["grades"]["english"] = 92
print(f"After: {students['alice']['grades']}")

print("\n=== Complex Real-World Example ===")

# Simulating an API response (like from GitHub)
api_response = {
    "user": {
        "username": "alice_dev",
        "name": "Alice Johnson",
        "followers": 1250
    },
    "repos": [
        {"name": "python-utils", "language": "Python", "stars": 45},
        {"name": "web-app", "language": "JavaScript", "stars": 12},
        {"name": "data-analysis", "language": "Python", "stars": 78}
    ]
}

# Extract and display data
print("\nAPI Response Data:")
print(f"  User: {api_response['user']['username']}")
print(f"  Name: {api_response['user']['name']}")
print("  Repos:")
for repo in api_response["repos"]:
    print(f"    - {repo['name']} ({repo['language']}) - {repo['stars']} stars")
```
