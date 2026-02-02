// Solution: Star Pattern

void main() {
  // Print star triangle
  for (var row = 1; row <= 5; row++) {
    var stars = '';
    for (var col = 1; col <= row; col++) {
      stars += '*';
    }
    print(stars);
  }
  
  print('');  // Blank line
  
  // Bonus: FizzBuzz
  print('FizzBuzz:');
  for (var i = 1; i <= 15; i++) {
    if (i % 3 == 0 && i % 5 == 0) {
      print('FizzBuzz');
    } else if (i % 3 == 0) {
      print('Fizz');
    } else if (i % 5 == 0) {
      print('Buzz');
    } else {
      print(i);
    }
  }
}