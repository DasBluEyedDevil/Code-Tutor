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
  // Use iterator helpers to:
  // 1. Filter for active users
  // 2. Filter for premium tier
  // 3. Map to uppercase email
  // 4. Take first 5
  // 5. Convert to array
}

console.log(getActivePremiumEmails(generateUsers()));