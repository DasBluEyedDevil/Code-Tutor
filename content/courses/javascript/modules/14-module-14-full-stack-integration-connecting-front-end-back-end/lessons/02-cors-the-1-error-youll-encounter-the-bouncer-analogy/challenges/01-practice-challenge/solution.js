// Complete CORS simulation

function parseOrigin(url) {
  let match = url.match(/(https?):\/\/([^:\/]+)(?::(\d+))?/);
  return {
    protocol: match[1],
    domain: match[2],
    port: match[3] || (match[1] === 'https' ? '443' : '80'),
    full: `${match[1]}://${match[2]}:${match[3] || (match[1] === 'https' ? '443' : '80')}`
  };
}

let browser = {
  checkCORS(frontendOrigin, backendOrigin) {
    let f = parseOrigin(frontendOrigin);
    let b = parseOrigin(backendOrigin);
    
    let sameOrigin = f.protocol === b.protocol &&
                     f.domain === b.domain &&
                     f.port === b.port;
    
    console.log(`[Browser] Checking origins...`);
    console.log(`  Frontend: ${f.protocol}://${f.domain}:${f.port}`);
    console.log(`  Backend:  ${b.protocol}://${b.domain}:${b.port}`);
    console.log(`  Same origin? ${sameOrigin}`);
    
    return sameOrigin;
  },
  
  makeRequest(frontendOrigin, backendOrigin, backendObj) {
    let sameOrigin = this.checkCORS(frontendOrigin, backendOrigin);
    
    if (sameOrigin) {
      console.log('[Browser] Same origin - request allowed!\n');
      return { allowed: true, reason: 'Same origin' };
    }
    
    console.log('[Browser] Different origin - checking CORS headers...');
    let result = backendObj.handleRequest(frontendOrigin);
    console.log(`[Browser] ${result}\n`);
    
    return { allowed: result.includes('allowed'), reason: result };
  }
};

let backend = {
  corsEnabled: true,
  allowedOrigins: ['http://localhost:3000', 'https://myapp.com'],
  allowCredentials: false,
  
  handleRequest(origin) {
    console.log(`[Backend] Received request from: ${origin}`);
    
    if (!this.corsEnabled) {
      console.log('[Backend] CORS is disabled - blocking all cross-origin requests');
      return 'CORS Error: No Access-Control-Allow-Origin header';
    }
    
    if (this.allowedOrigins.includes('*')) {
      console.log('[Backend] CORS allows all origins (*)');  
      return 'Request allowed (all origins)';
    }
    
    if (this.allowedOrigins.includes(origin)) {
      console.log(`[Backend] Origin ${origin} is in allowed list`);
      console.log('[Backend] Adding header: Access-Control-Allow-Origin:', origin);
      return 'Request allowed';
    }
    
    console.log(`[Backend] Origin ${origin} is NOT in allowed list`);
    console.log('[Backend] Blocking request');
    return 'CORS Error: Origin not allowed';
  },
  
  enableCORS(options = {}) {
    this.corsEnabled = true;
    if (options.origins) {
      this.allowedOrigins = options.origins;
    }
    if (options.credentials) {
      this.allowCredentials = options.credentials;
    }
    console.log('[Backend] CORS configured:', {
      enabled: this.corsEnabled,
      allowedOrigins: this.allowedOrigins,
      credentials: this.allowCredentials
    });
  }
};

// Simulate different scenarios
console.log('=== Scenario 1: React dev → Express API (CORS enabled) ===\n');
backend.enableCORS({ origins: ['http://localhost:3000'] });
browser.makeRequest('http://localhost:3000', 'http://localhost:4000', backend);

console.log('=== Scenario 2: Same origin (no CORS needed) ===\n');
browser.makeRequest('http://localhost:3000', 'http://localhost:3000', backend);

console.log('=== Scenario 3: Unauthorized origin (CORS blocks) ===\n');
browser.makeRequest('http://evil-site.com', 'http://localhost:4000', backend);

console.log('=== Scenario 4: CORS disabled (everything blocked) ===\n');
backend.corsEnabled = false;
browser.makeRequest('http://localhost:3000', 'http://localhost:4000', backend);

console.log('=== Scenario 5: Allow all origins (*) ===\n');
backend.enableCORS({ origins: ['*'] });
browser.makeRequest('http://any-site.com', 'http://localhost:4000', backend);

// Summary
console.log('\n=== CORS Summary ===\n');
console.log('✓ Same origin = No CORS needed');
console.log('✓ Different origin + CORS enabled + origin in list = Allowed');
console.log('✗ Different origin + CORS disabled = Blocked');
console.log('✗ Different origin + origin not in list = Blocked');
console.log('\n💡 Fix: Add app.use(cors()) to your Express backend!');