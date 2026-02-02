// 1. Generic wrapInArray function
function wrapInArray<T>(value: T): T[] {
  return [value];
}

// 2. Generic Result interface
interface Result<T> {
  success: boolean;
  data: T;
  error: string | null;
}

// 3. Generic createSuccess function
function createSuccess<T>(data: T): Result<T> {
  return {
    success: true,
    data: data,
    error: null
  };
}

// 4. Test with different types
console.log(wrapInArray<number>(42));        // [42]
console.log(wrapInArray<string>('hello'));   // ['hello']
console.log(wrapInArray([1, 2, 3]));         // [[1, 2, 3]]

let userResult = createSuccess<{ id: number; name: string }>({ id: 1, name: 'Alice' });
console.log('User result:', userResult);
// { success: true, data: { id: 1, name: 'Alice' }, error: null }

let numberResult = createSuccess<number>(42);
console.log('Number result:', numberResult);
// { success: true, data: 42, error: null }