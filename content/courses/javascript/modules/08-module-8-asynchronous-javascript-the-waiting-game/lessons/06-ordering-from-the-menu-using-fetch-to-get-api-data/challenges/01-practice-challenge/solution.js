async function getRandomUser() {
  try {
    let response = await fetch('https://randomuser.me/api/');
    
    if (!response.ok) {
      throw new Error('Request failed');
    }
    
    let data = await response.json();
    let firstName = data.results[0].name.first;
    
    return firstName;
  } catch (error) {
    console.log('Error:', error);
    return null;
  }
}

getRandomUser().then(name => console.log('Random user:', name));