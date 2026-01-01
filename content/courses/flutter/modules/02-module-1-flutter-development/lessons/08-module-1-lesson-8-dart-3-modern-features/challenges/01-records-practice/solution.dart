// Solution: Records Practice

// Function returning a named record
({String name, int age, String email}) getPersonDetails() {
  return (
    name: 'Alice Johnson',
    age: 28,
    email: 'alice@example.com',
  );
}

void main() {
  // Call and destructure the record
  var (:name, :age, :email) = getPersonDetails();
  
  // Print each field
  print('Name: $name');
  print('Age: $age');
  print('Email: $email');
}