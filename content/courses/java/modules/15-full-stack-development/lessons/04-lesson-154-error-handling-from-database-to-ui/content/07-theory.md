---
type: "THEORY"
title: "Displaying Errors to Users"
---

Create user-friendly error displays:

HTML:
<div id="errorContainer" class="error-message" style="display: none;">
    <span id="errorText"></span>
    <button onclick="closeError()">×</button>
</div>

<div id="successContainer" class="success-message" style="display: none;">
    <span id="successText"></span>
</div>

CSS:
.error-message {
    background-color: #f8d7da;
    color: #721c24;
    padding: 12px;
    border: 1px solid #f5c6cb;
    border-radius: 4px;
    margin: 10px 0;
}

.success-message {
    background-color: #d4edda;
    color: #155724;
    padding: 12px;
    border: 1px solid #c3e6cb;
    border-radius: 4px;
    margin: 10px 0;
}

JavaScript:
function displayError(message) {
    const errorContainer = document.getElementById('errorContainer');
    const errorText = document.getElementById('errorText');
    errorText.textContent = message;
    errorContainer.style.display = 'block';
    
    // Auto-hide after 5 seconds
    setTimeout(() => {
        errorContainer.style.display = 'none';
    }, 5000);
}

function displaySuccess(message) {
    const successContainer = document.getElementById('successContainer');
    const successText = document.getElementById('successText');
    successText.textContent = message;
    successContainer.style.display = 'block';
    
    setTimeout(() => {
        successContainer.style.display = 'none';
    }, 3000);
}

function displayValidationErrors(errors) {
    const messages = Object.entries(errors)
        .map(([field, message]) => `${field}: ${message}`)
        .join('\n');
    displayError(messages);
}