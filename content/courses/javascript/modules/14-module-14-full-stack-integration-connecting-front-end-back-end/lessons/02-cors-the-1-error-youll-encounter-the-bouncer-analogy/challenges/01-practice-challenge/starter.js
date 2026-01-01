// CORS Simulation

function parseOrigin(url) {
  // Extract protocol, domain, port
  let match = url.match(/(https?):\/\/([^:\/]+)(?::(\d+))?/);
  return {
    protocol: match[1],
    domain: match[2],
    port: match[3] || (match[1] === 'https' ? '443' : '80')
  };
}

let browser = {
  checkCORS(frontendOrigin, backendOrigin) {
    let f = parseOrigin(frontendOrigin);
    let b = parseOrigin(backendOrigin);
    
    let sameOrigin = f.protocol === b.protocol &&
                     f.domain === b.domain &&
                     f.port === b.port;
    
    return sameOrigin;
  }
};

let backend = {
  corsEnabled: true,
  allowedOrigins: ['http://localhost:3000', 'https://myapp.com'],
  
  handleRequest(origin) {
    if (!this.corsEnabled) {
      return 'CORS Error: Blocked!';
    }
    
    if (this.allowedOrigins.includes(origin)) {
      return 'Request allowed';
    }
    
    return 'CORS Error: Origin not allowed';
  }
};

// Test scenarios
console.log('=== CORS Tests ===\n');

let tests = [
  ['http://localhost:3000', 'http://localhost:4000'],
  ['http://localhost:3000', 'http://localhost:3000'],
  ['https://myapp.com', 'https://api.myapp.com']
];

tests.forEach(([frontend, api]) => {
  console.log(`Frontend: ${frontend}`);
  console.log(`Backend:  ${api}`);
  console.log(`Same origin? ${browser.checkCORS(frontend, api)}`);
  console.log(`Backend says: ${backend.handleRequest(frontend)}\n`);
});