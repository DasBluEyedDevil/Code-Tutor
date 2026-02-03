// NOTE: This challenge uses Bun.password (built-in password hashing).
// The simulation below lets you practice the API patterns in any runtime.
// When running with Bun, remove the simulation and use Bun.password directly.

// --- Simulation for non-Bun runtimes ---
const _crypto = await import('crypto');
const Bun = {
  password: {
    async hash(password) {
      const salt = _crypto.randomBytes(16).toString('hex');
      const hash = _crypto.createHash('sha256').update(salt + password).digest('hex');
      return `$sim$${salt}$${hash}`;
    },
    async verify(password, stored) {
      const [, , salt, hash] = stored.split('$');
      const check = _crypto.createHash('sha256').update(salt + password).digest('hex');
      return check === hash;
    }
  }
};
// --- End simulation (in production, Bun.password uses argon2id/bcrypt) ---

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