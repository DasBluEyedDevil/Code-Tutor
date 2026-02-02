print("Start")
try:
    x = input("Prompt: ")
    print(f"End {x}")
except EOFError:
    print("EOFError caught")
except Exception as e:
    print(f"Error: {e}")
