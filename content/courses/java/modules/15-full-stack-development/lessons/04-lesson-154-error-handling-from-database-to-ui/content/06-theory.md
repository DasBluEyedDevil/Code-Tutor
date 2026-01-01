---
type: "THEORY"
title: "Frontend: Handling Errors in JavaScript"
---

MODERN APPROACH - async/await with try-catch:

async function createUser(userData) {
    try {
        const response = await fetch('http://localhost:8080/api/users', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(userData)
        });
        
        // Check HTTP status
        if (!response.ok) {
            // Parse error response
            const error = await response.json();
            
            // Handle different error types
            if (response.status === 409) {
                displayError('Email already exists. Please use a different email.');
            } else if (response.status === 400) {
                displayValidationErrors(error.errors);
            } else if (response.status === 500) {
                displayError('Server error. Please try again later.');
            } else {
                displayError(error.detail || 'An error occurred');
            }
            return null;
        }
        
        // Success
        const user = await response.json();
        displaySuccess('User created successfully!');
        return user;
        
    } catch (error) {
        // Network error (no response from server)
        if (error.name === 'TypeError' && error.message.includes('fetch')) {
            displayError('Cannot connect to server. Check your internet connection.');
        } else {
            displayError('An unexpected error occurred: ' + error.message);
        }
        console.error('Error creating user:', error);
        return null;
    }
}