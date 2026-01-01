// Define TrafficLight type with literal values
type TrafficLight = 'red' | 'yellow' | 'green';

// Transition function: returns the next state
function nextLight(current: TrafficLight): TrafficLight {
  switch (current) {
    case 'red':
      return 'green';
    case 'green':
      return 'yellow';
    case 'yellow':
      return 'red';
  }
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

// This should cause a TypeScript error:
// light = 'purple';  // Error: Type '"purple"' is not assignable to type 'TrafficLight'