---
type: "KEY_POINT"
title: "Why React?"
---

React is a JavaScript library for building user interfaces.

INSTEAD OF:
- Manually creating DOM elements
- Tracking what needs to update
- Managing event listeners
- Syncing data with UI

REACT PROVIDES:
- Declarative UI (describe what you want, not how)
- Component-based architecture
- Automatic DOM updates when data changes
- Efficient rendering (Virtual DOM)

VANILLA JS (imperative):
const button = document.createElement('button');
button.textContent = 'Count: 0';
let count = 0;
button.addEventListener('click', () => {
    count++;
    button.textContent = 'Count: ' + count;
});
document.body.appendChild(button);

REACT (declarative):
function Counter() {
    const [count, setCount] = useState(0);
    return (
        <button onClick={() => setCount(count + 1)}>
            Count: {count}
        </button>
    );
}

React is used by Facebook, Instagram, Netflix, Airbnb, and millions of other applications.