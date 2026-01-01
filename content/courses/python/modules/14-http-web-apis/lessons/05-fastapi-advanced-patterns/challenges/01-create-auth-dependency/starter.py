from fastapi import FastAPI, Depends, HTTPException, Header

app = FastAPI()

# Valid API keys (in real app, store in database)
VALID_API_KEYS = {"key123", "key456"}

# TODO: Create dependency function
def verify_api_key(x_api_key: str = Header(...)):
    # Check if key is valid
    # Raise HTTPException(403) if invalid
    # Return the key if valid
    pass

@app.get("/public/")
def public_endpoint():
    return {"message": "Anyone can see this"}

# TODO: Add protected endpoint that uses verify_api_key
@app.get("/protected/")
def protected_endpoint():
    pass

print("API Key auth demo")