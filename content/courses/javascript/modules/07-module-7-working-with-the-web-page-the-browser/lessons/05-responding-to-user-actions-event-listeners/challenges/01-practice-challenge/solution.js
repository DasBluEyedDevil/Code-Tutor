let form = document.querySelector('#signupForm');
let message = document.querySelector('#message');

form.addEventListener('submit', function(event) {
  event.preventDefault();
  
  let username = document.querySelector('#username').value;
  let email = document.querySelector('#email').value;
  
  if (username === '') {
    message.textContent = 'Username is required';
    message.style.color = 'red';
  } else if (!email.includes('@')) {
    message.textContent = 'Please enter a valid email';
    message.style.color = 'red';
  } else {
    message.textContent = 'Sign up successful!';
    message.style.color = 'green';
  }
});