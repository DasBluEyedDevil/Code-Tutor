// Solution: My Profile

void main() {
  // Variables for profile information
  String name = 'Alex Johnson';
  int age = 28;
  String favoriteFood = 'Pizza';
  bool likesProgramming = true;
  double height = 5.9;
  
  // Print profile
  print('=== My Profile ===');
  print('Name: $name');
  print('Age: $age');
  print('Favorite Food: $favoriteFood');
  print('Likes Programming: $likesProgramming');
  print('Height: $height feet');
  
  // Bonus: Calculate age from birth year
  var currentYear = 2025;
  var birthYear = 1997;
  var calculatedAge = currentYear - birthYear;
  print('Calculated age: $calculatedAge');
}