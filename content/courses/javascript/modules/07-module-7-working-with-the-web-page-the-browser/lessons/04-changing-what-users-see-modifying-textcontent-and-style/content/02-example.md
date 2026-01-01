---
type: "EXAMPLE"
title: "Updating the UI"
---

```html
<h1 id="status-text">System Offline</h1>
<div id="box" style="width: 100px; height: 100px; background: grey;"></div>
<button id="update-btn">Activate System</button>

<style>
  .active-state {
    border: 5px solid gold;
    box-shadow: 0 0 10px yellow;
  }
</style>

<script>
  const status = document.querySelector('#status-text');
  const box = document.querySelector('#box');
  const btn = document.querySelector('#update-btn');

  // 1. Changing Text
  status.textContent = "System Online!";

  // 2. Changing Style Directly (Inline Styles)
  // Note: CSS properties with dashes like 'background-color' 
  // become camelCase in JS: 'backgroundColor'
  box.style.backgroundColor = 'limegreen';
  box.style.borderRadius = '50%';

  // 3. Using ClassList (Best Practice)
  // Instead of changing 10 styles, just add one class
  box.classList.add('active-state');

  // 4. Toggling
  // If it's there, remove it. If it's not, add it.
  btn.addEventListener('click', () => {
    box.classList.toggle('active-state');
  });
</script>
```