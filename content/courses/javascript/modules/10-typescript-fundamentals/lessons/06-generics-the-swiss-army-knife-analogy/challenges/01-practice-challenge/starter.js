// 1. Create the wrapInArray function
function wrapInArray(value) {
  return [value];
}

// 2. Create the Result interface
interface Result {
  success: boolean;
  data: any;
  error: string | null;
}

// 3. Create the createSuccess function
function createSuccess(data) {
  return {
    success: true,
    data: data,
    error: null
  };
}

// 4. Test your code
console.log(wrapInArray(42));
console.log(wrapInArray('hello'));
console.log(createSuccess({ id: 1, name: 'Alice' }));