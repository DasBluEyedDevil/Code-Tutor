// Solution: Age Advice Program

void main() {
  var age = 25;  // Try different ages to test!
  
  if (age < 13) {
    print("You're a child! Enjoy playing!");
  } else if (age >= 13 && age < 20) {
    print("You're a teenager! Study hard!");
  } else if (age >= 20 && age < 65) {
    print("You're an adult! Work hard, but enjoy life!");
  } else {
    print("You're a senior! Time to relax and enjoy retirement!");
  }
}