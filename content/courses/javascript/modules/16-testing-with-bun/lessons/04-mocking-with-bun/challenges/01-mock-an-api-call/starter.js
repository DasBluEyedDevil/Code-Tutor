// Simulating bun:test mock functions
const mock = (fn) => {
  const mockFn = (...args) => mockFn._returnValue;
  mockFn._calls = [];
  mockFn.mockReturnValue = (val) => { mockFn._returnValue = val; return mockFn; };
  mockFn.mockResolvedValue = (val) => { mockFn._returnValue = Promise.resolve(val); return mockFn; };
  mockFn.mockRejectedValue = (err) => { mockFn._returnValue = Promise.reject(err); return mockFn; };
  return mockFn;
};
const describe = (name, fn) => { console.log(`describe: ${name}`); fn(); };
const it = async (name, fn) => {
  try { await fn(); console.log(`  ✓ ${name}`); }
  catch (e) { console.log(`  ✗ ${name}: ${e.message}`); }
};
const expect = (val) => ({
  toBe: (exp) => { if (val !== exp) throw new Error(`Expected ${exp}, got ${val}`); }
});

// The API function we'll mock
let fetchWeather = async (city) => {
  // In reality this would call an API
  return { temp: 72, condition: 'sunny' };
};

// Function to test
async function getWeatherMessage(city) {
  try {
    const weather = await fetchWeather(city);
    return `It's ${weather.temp}°F and ${weather.condition} in ${city}`;
  } catch (error) {
    return `Weather unavailable for ${city}`;
  }
}

describe('getWeatherMessage', () => {
  it('returns formatted weather message on success', async () => {
    // Mock fetchWeather to return controlled data
    fetchWeather = mock().mockResolvedValue({ temp: 75, condition: 'cloudy' });
    
    const message = await getWeatherMessage('Seattle');
    
    expect(message).toBe("It's 75°F and cloudy in Seattle");
  });

  it('returns fallback message on API error', async () => {
    // YOUR CODE: Mock fetchWeather to reject with an error
    // Then call getWeatherMessage and verify the fallback message
  });
});