from fastapi import FastAPI, Depends, HTTPException, Header

app = FastAPI()

VALID_API_KEYS = {"key123", "key456"}

def verify_api_key(x_api_key: str = Header(...)):
    """Dependency to verify API key"""
    if x_api_key not in VALID_API_KEYS:
        raise HTTPException(
            status_code=403,
            detail="Invalid API key"
        )
    return x_api_key

@app.get("/public/")
def public_endpoint():
    return {"message": "Anyone can see this"}

@app.get("/protected/")
def protected_endpoint(api_key: str = Depends(verify_api_key)):
    return {
        "message": "Secret data!",
        "authenticated_with": api_key[:4] + "..."
    }

@app.get("/admin/")
def admin_endpoint(api_key: str = Depends(verify_api_key)):
    # Can reuse the same dependency
    return {"message": "Admin area", "key": api_key}

print("API Key auth ready!")
print("Test: curl -H 'X-API-Key: key123' localhost:8000/protected/")