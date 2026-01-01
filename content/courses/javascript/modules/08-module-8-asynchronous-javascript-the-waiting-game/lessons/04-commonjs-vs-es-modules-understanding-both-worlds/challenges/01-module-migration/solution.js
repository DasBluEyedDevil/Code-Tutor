// calculator.mjs (ES Module)
class Calculator {
  add(a, b) {
    return a + b;
  }
  
  subtract(a, b) {
    return a - b;
  }
}

export function multiply(a, b) {
  return a * b;
}

export default Calculator;

// Usage:
// import Calculator, { multiply } from './calculator.mjs';
// const calc = new Calculator();
// console.log(calc.add(2, 3));    // 5
// console.log(multiply(4, 5));    // 20