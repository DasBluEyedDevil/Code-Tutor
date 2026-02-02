function withTimeout(promise, ms) {
  const { promise: timeoutPromise, reject } = Promise.withResolvers();
  
  const timer = setTimeout(() => reject(new Error('Timeout')), ms);
  
  return Promise.race([promise, timeoutPromise])
    .finally(() => clearTimeout(timer));
}

const slow = new Promise(r => setTimeout(() => r('done'), 5000));
withTimeout(slow, 1000).catch(err => console.log(err.message));