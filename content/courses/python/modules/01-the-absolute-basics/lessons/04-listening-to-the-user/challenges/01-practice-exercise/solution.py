# Mad Libs Story Generator - SOLUTION

print("Let's create a silly story together!")
print("I'll ask for some words, and you provide them.\n")

# Collect the words
adjective1 = input("Give me an adjective (describing word): ")
noun = input("Give me a noun (a thing): ")
verb = input("Give me a verb ending in 'ing' (an action): ")
adjective2 = input("Give me another adjective: ")
place = input("Give me a place: ")

# Create the story using f-strings
print("\n" + "=" * 50)
print("           YOUR SILLY STORY")
print("=" * 50)
print(f"Once upon a time, there was a {adjective1} {noun}.")
print(f"It loved {verb} in {place}.")
print(f"One day, it became very {adjective2}!")
print(f"The {noun} lived happily ever after.")
print("=" * 50)

# Example output:
# Once upon a time, there was a sparkly banana.
# It loved dancing in the moon.
# One day, it became very confused!
# The banana lived happily ever after.