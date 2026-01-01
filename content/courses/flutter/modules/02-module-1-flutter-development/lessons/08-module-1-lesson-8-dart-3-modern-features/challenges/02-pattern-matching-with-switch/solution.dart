// Solution: Pattern Matching with Switch

String describe(Object value) {
  return switch (value) {
    // Integer patterns
    0 => 'zero',
    int n when n < 0 => 'negative: $n',
    int n => 'positive: $n',
    
    // String patterns
    String s when s.isEmpty => 'empty string',
    String s when s.length <= 5 => 'short string: $s',
    String s => 'long string: $s',
    
    // List patterns
    [] => 'empty list',
    [var single] => 'single element: $single',
    [var first, ...] => 'list starting with: $first',
    
    // Default
    _ => 'unknown: $value',
  };
}

void main() {
  print(describe(42));          // positive: 42
  print(describe(-5));          // negative: -5
  print(describe(0));           // zero
  print(describe('hello'));     // short string: hello
  print(describe(''));          // empty string
  print(describe([1, 2, 3]));   // list starting with: 1
  print(describe([]));          // empty list
}