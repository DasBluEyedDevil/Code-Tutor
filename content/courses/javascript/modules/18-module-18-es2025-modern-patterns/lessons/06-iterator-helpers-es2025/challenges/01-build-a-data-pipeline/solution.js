function* generateUsers() {
  const statuses = ['active', 'inactive'];
  const tiers = ['free', 'premium'];
  for (let i = 1; i <= 1000; i++) {
    yield {
      id: i,
      email: `user${i}@example.com`,
      status: statuses[i % 2],
      tier: tiers[Math.floor(i / 3) % 2]
    };
  }
}

function getActivePremiumEmails(userGenerator) {
  return userGenerator
    .filter(user => user.status === 'active')
    .filter(user => user.tier === 'premium')
    .map(user => user.email.toUpperCase())
    .take(5)
    .toArray();
}

console.log(getActivePremiumEmails(generateUsers()));