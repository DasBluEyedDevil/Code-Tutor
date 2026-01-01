let box = document.querySelector('#box');

box.addEventListener('click', function() {
  // Toggle the highlighted class
  
  // Check if highlighted and update text
  if (box.classList.contains('highlighted')) {
    box.textContent = 'Highlighted!';
  } else {
    box.textContent = 'Click Me!';
  }
});