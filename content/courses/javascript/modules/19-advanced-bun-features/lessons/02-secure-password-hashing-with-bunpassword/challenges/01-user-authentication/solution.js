const users = new Map();

async function signup(username, password) {
  const hash = await Bun.password.hash(password);
  users.set(username, hash);
}

async function login(username, password) {
  const hash = users.get(username);
  if (!hash) return false;
  return await Bun.password.verify(password, hash);
}

await signup('alice', 'secret123');
console.log(await login('alice', 'secret123'));
console.log(await login('alice', 'wrong'));