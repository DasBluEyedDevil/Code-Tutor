function timed(target, context) {
  // Your decorator implementation
}

class DataProcessor {
  @timed
  processData(data) {
    // Simulate work
    let sum = 0;
    for (let i = 0; i < 1000000; i++) sum += i;
    return sum;
  }
}

const processor = new DataProcessor();
processor.processData([1, 2, 3]);