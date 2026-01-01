/**
 * Fetches a user by their ID
 * @param {number} id - The user's ID
 * @returns {Promise<{ id: number, name: string }>} The user object
 */
async function getUser(id) {
  const response = await fetch(`/api/users/${id}`);
  return response.json();
}

const user = await getUser(42);
console.log(user.name);