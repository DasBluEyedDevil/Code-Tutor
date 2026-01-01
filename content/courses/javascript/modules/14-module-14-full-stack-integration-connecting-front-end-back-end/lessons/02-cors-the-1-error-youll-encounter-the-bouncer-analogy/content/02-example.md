---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// CORS - The #1 Full-Stack Error (and how to fix it!)

console.log('=== CORS Error Simulation ===\n');

// THE PROBLEM: Different origins
let origins = {
  frontend: 'http://localhost:3000',
  backend: 'http://localhost:4000'
};

console.log('Frontend origin:', origins.frontend);
console.log('Backend origin:', origins.backend);
console.log('\nAre these the same origin?', origins.frontend === origins.backend);
console.log('Result: DIFFERENT ORIGINS → Browser blocks by default!\n');

// What makes origins different?
function analyzeOrigins() {
  let examples = [
    {
      url1: 'http://localhost:3000',
      url2: 'http://localhost:4000',
      same: false,
      reason: 'Different PORT'
    },
    {
      url1: 'http://example.com',
      url2: 'https://example.com',
      same: false,
      reason: 'Different PROTOCOL (http vs https)'
    },
    {
      url1: 'http://example.com',
      url2: 'http://api.example.com',
      same: false,
      reason: 'Different SUBDOMAIN'
    },
    {
      url1: 'http://localhost:3000/page1',
      url2: 'http://localhost:3000/page2',
      same: true,
      reason: 'Same protocol, domain, port (path doesn\'t matter)'
    }
  ];
  
  console.log('=== What Makes Origins Different? ===\n');
  examples.forEach(ex => {
    console.log(`${ex.url1}`);
    console.log(`${ex.url2}`);
    console.log(`Same origin? ${ex.same ? 'YES' : 'NO'}`);
    console.log(`Reason: ${ex.reason}\n`);
  });
}

analyzeOrigins();

// THE ERROR students will see
function simulateCORSError() {
  console.log('=== The Error Message ===\n');
  console.log('Access to fetch at \'http://localhost:4000/api/users\' from origin');
  console.log('   \'http://localhost:3000\' has been blocked by CORS policy:');
  console.log('   No \'Access-Control-Allow-Origin\' header is present on the');
  console.log('   requested resource.\n');
  console.log('Translation: Your backend didn\'t give permission!\n');
}

simulateCORSError();

// THE SOLUTION (Hono)
console.log('=== The Fix (Hono Backend Code) ===\n');
console.log('// Hono has built-in CORS middleware - no extra package needed!\n');

console.log('// Import Hono and its CORS middleware');
console.log('import { Hono } from "hono";');
console.log('import { cors } from "hono/cors";  // Built-in!\n');

console.log('const app = new Hono();\n');

console.log('// THIS ONE LINE FIXES THE ERROR!');
console.log('app.use("*", cors());  // Allows ALL origins (dev only!)\n');

console.log('// Now your routes work');
console.log('app.get("/api/users", (c) => {');
console.log('  return c.json([{ id: 1, name: "Alice" }]);');
console.log('});\n');

// More secure CORS configuration
console.log('=== Production CORS (More Secure) ===\n');
console.log('// Only allow specific origin');
console.log('app.use("*", cors({');
console.log('  origin: "https://myapp.com",  // Only this domain allowed');
console.log('  credentials: true,             // Allow cookies');
console.log('  allowMethods: ["GET", "POST", "PUT", "DELETE"]  // Allowed HTTP methods');
console.log('}));\n');

console.log('// Multiple allowed origins');
console.log('const allowedOrigins = [');
console.log('  "http://localhost:3000",  // Dev');
console.log('  "https://myapp.com",      // Production');
console.log('  "https://staging.myapp.com"  // Staging');
console.log('];\n');

console.log('app.use("*", cors({');
console.log('  origin: (origin) => {');
console.log('    if (allowedOrigins.includes(origin)) {');
console.log('      return origin;');
console.log('    }');
console.log('    return null;  // Deny other origins');
console.log('  }');
console.log('}));\n');

// What CORS actually does
console.log('=== What CORS Does (Under the Hood) ===\n');
console.log('When you use app.use("*", cors()), it adds these headers:\n');

let corsHeaders = {
  'Access-Control-Allow-Origin': '*',
  'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, OPTIONS',
  'Access-Control-Allow-Headers': 'Content-Type, Authorization'
};

for (let [header, value] of Object.entries(corsHeaders)) {
  console.log(`${header}: ${value}`);
}

console.log('\nThese headers tell the browser: "It\'s OK, I allow this!"\n');

// Common CORS scenarios
console.log('=== Common CORS Scenarios ===\n');

let scenarios = [
  {
    scenario: 'React dev server calling Hono API',
    frontend: 'http://localhost:3000',
    backend: 'http://localhost:4000',
    needsCORS: true,
    solution: 'app.use("*", cors())'
  },
  {
    scenario: 'Production frontend calling production API',
    frontend: 'https://myapp.com',
    backend: 'https://api.myapp.com',
    needsCORS: true,
    solution: 'app.use("*", cors({ origin: "https://myapp.com" }))'
  },
  {
    scenario: 'React and API on same domain (proxy)',
    frontend: 'https://myapp.com',
    backend: 'https://myapp.com/api',
    needsCORS: false,
    solution: 'No CORS needed - same origin!'
  }
];

scenarios.forEach((s, i) => {
  console.log(`Scenario ${i + 1}: ${s.scenario}`);
  console.log(`  Frontend: ${s.frontend}`);
  console.log(`  Backend:  ${s.backend}`);
  console.log(`  CORS needed? ${s.needsCORS ? 'YES' : 'NO'}`);
  console.log(`  Solution: ${s.solution}\n`);
});

// Debugging CORS
console.log('=== Debugging CORS Errors ===\n');
let debugSteps = [
  '1. Check if both frontend and backend are running',
  '2. Verify the URLs match (no typos)',
  '3. Check backend has app.use("*", cors()) BEFORE routes',
  '4. Look in browser Network tab -> Response headers',
  '5. Should see: Access-Control-Allow-Origin header',
  '6. Try curl or Postman (they bypass CORS) to test API',
  '7. Clear browser cache and restart dev servers'
];

debugSteps.forEach(step => console.log(step));
```
