let heading = document.getElementById('heading');
let button = document.getElementById('changeButton');

button.addEventListener('click', function() {
  heading.textContent = 'Hello, JavaScript!';
});