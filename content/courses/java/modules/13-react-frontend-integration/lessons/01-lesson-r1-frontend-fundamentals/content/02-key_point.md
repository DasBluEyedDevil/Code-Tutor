---
type: "KEY_POINT"
title: "The Web Trilogy: HTML, CSS, JavaScript"
---

Every web page is built from three technologies:

HTML (Structure):
- The skeleton of your page
- Defines WHAT content exists
- Headings, paragraphs, buttons, forms, images

<button id="loginBtn">Login</button>

CSS (Style):
- The clothing of your page
- Defines HOW content looks
- Colors, fonts, spacing, animations

#loginBtn {
    background-color: blue;
    color: white;
    padding: 10px 20px;
    border-radius: 5px;
}

JavaScript (Behavior):
- The brain of your page
- Defines WHAT HAPPENS when users interact
- Click handlers, API calls, dynamic updates

document.getElementById('loginBtn').addEventListener('click', () => {
    fetch('/api/login', { method: 'POST', body: formData });
});

Think of building a house:
- HTML = walls, rooms, doors
- CSS = paint, furniture, decorations
- JavaScript = electrical system, plumbing