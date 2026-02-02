let box = document.querySelector('#box');

box.addEventListener('click', function() {
  box.classList.toggle('highlighted');
  
  if (box.classList.contains('highlighted')) {
    box.textContent = 'Highlighted!';
  } else {
    box.textContent = 'Click Me!';
  }
});