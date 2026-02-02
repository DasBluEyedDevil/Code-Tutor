// Add JSDoc here
async function getUser(id) {
  const response = await fetch(`/api/users/${id}`);
  return response.json();
}

const user = await getUser(42);
console.log(user.name);