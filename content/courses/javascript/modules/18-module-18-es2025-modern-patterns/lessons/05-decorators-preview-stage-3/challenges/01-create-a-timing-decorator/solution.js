function timed(target, context) {
  return function(...args) {
    const start = performance.now();
    const result = target.apply(this, args);
    const end = performance.now();
    console.log(`${context.name} took ${(end - start).toFixed(2)}ms`);
    return result;
  };
}

class DataProcessor {
  @timed
  processData(data) {
    let sum = 0;
    for (let i = 0; i < 1000000; i++) sum += i;
    return sum;
  }
}

const processor = new DataProcessor();
processor.processData([1, 2, 3]);