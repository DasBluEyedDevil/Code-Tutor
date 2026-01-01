// Add type annotations to these variables
let username = 'YourName';
let age = 0;
let isStudent = true;
let grades = [85, 92, 78];

// Calculate average
let sum = 0;
for (let grade of grades) {
  sum = sum + grade;
}
let average = sum / grades.length;

console.log('Username:', username);
console.log('Age:', age);
console.log('Student?', isStudent);
console.log('Grades:', grades);
console.log('Average:', average);