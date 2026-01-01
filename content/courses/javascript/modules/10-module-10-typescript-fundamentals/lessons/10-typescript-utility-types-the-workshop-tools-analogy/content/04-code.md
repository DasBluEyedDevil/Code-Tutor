---
type: "CODE"
title: "Record<K, V>"
---

Record<K, V> creates an object type where every key of type K maps to a value of type V. It's perfect for lookup tables, dictionaries, and mapping enum values to data. Think of it as a type-safe way to say 'an object where all keys are X and all values are Y'.

```typescript
// RECORD<K, V> - Create an object type with specific key-value pairs

// Simple lookup table: string keys, number values
type PriceList = Record<string, number>;

let prices: PriceList = {
  apple: 1.50,
  banana: 0.75,
  orange: 2.00
};

console.log(prices['apple']);  // 1.5

// Type-safe enum-to-value mapping
type Status = 'pending' | 'active' | 'completed' | 'cancelled';
type StatusInfo = { label: string; color: string };

// Record ensures EVERY status has info defined
const statusConfig: Record<Status, StatusInfo> = {
  pending: { label: 'Pending', color: 'yellow' },
  active: { label: 'Active', color: 'green' },
  completed: { label: 'Completed', color: 'blue' },
  cancelled: { label: 'Cancelled', color: 'red' }
  // If you forget one, TypeScript errors!
};

function getStatusBadge(status: Status): string {
  const info = statusConfig[status];
  return `<span style="color: ${info.color}">${info.label}</span>`;
}

console.log(getStatusBadge('active'));
// <span style="color: green">Active</span>

// Permission system with Record
type Role = 'admin' | 'editor' | 'viewer';
type Permission = 'create' | 'read' | 'update' | 'delete';

// Each role has a set of permissions
const rolePermissions: Record<Role, Permission[]> = {
  admin: ['create', 'read', 'update', 'delete'],
  editor: ['create', 'read', 'update'],
  viewer: ['read']
};

function canPerform(role: Role, action: Permission): boolean {
  return rolePermissions[role].includes(action);
}

console.log(canPerform('editor', 'read'));   // true
console.log(canPerform('viewer', 'delete')); // false

// Caching with Record
type CacheEntry<T> = {
  data: T;
  timestamp: number;
  ttl: number;
};

type Cache<T> = Record<string, CacheEntry<T>>;

let userCache: Cache<{ name: string; email: string }> = {};

function cacheUser(id: string, user: { name: string; email: string }): void {
  userCache[id] = {
    data: user,
    timestamp: Date.now(),
    ttl: 60000  // 1 minute
  };
}

function getCachedUser(id: string): { name: string; email: string } | null {
  const entry = userCache[id];
  if (!entry) return null;
  if (Date.now() - entry.timestamp > entry.ttl) {
    delete userCache[id];
    return null;
  }
  return entry.data;
}

cacheUser('user-1', { name: 'Alice', email: 'alice@test.com' });
console.log(getCachedUser('user-1'));
// { name: 'Alice', email: 'alice@test.com' }

// Record with index for counting
function countWords(text: string): Record<string, number> {
  const words = text.toLowerCase().split(/\s+/);
  const counts: Record<string, number> = {};
  
  for (const word of words) {
    counts[word] = (counts[word] || 0) + 1;
  }
  
  return counts;
}

console.log(countWords('the quick brown fox jumps over the lazy dog'));
// { the: 2, quick: 1, brown: 1, fox: 1, jumps: 1, over: 1, lazy: 1, dog: 1 }

// Nested Record for complex mappings
type Language = 'en' | 'es' | 'fr';
type TranslationKey = 'greeting' | 'farewell' | 'thanks';

const translations: Record<Language, Record<TranslationKey, string>> = {
  en: { greeting: 'Hello', farewell: 'Goodbye', thanks: 'Thank you' },
  es: { greeting: 'Hola', farewell: 'Adios', thanks: 'Gracias' },
  fr: { greeting: 'Bonjour', farewell: 'Au revoir', thanks: 'Merci' }
};

function translate(key: TranslationKey, lang: Language): string {
  return translations[lang][key];
}

console.log(translate('greeting', 'fr'));  // 'Bonjour'
```
