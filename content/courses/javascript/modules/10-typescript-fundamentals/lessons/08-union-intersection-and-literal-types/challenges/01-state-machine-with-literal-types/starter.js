// Define TrafficLight type with literal values
type TrafficLight = // your code here

// Transition function: returns the next state
function nextLight(current: TrafficLight): TrafficLight {
  // Implement the state transitions
}

// Test it
let light: TrafficLight = 'red';
console.log(`Current: ${light}`);

light = nextLight(light);
console.log(`Next: ${light}`);

light = nextLight(light);
console.log(`Next: ${light}`);

light = nextLight(light);
console.log(`Next: ${light}`);

// This should cause a TypeScript error (uncomment to test):
// light = 'purple';