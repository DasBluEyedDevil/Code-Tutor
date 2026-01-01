// Properly typed variables
let username: string = 'Alice';
let age: number = 20;
let isStudent: boolean = true;
let grades: number[] = [85, 92, 78];

// Calculate average
let sum: number = 0;
for (let grade of grades) {
  sum = sum + grade;
}
let average: number = sum / grades.length;

console.log('Username:', username);  // Alice
console.log('Age:', age);            // 20
console.log('Student?', isStudent);  // true
console.log('Grades:', grades);      // [85, 92, 78]
console.log('Average:', average);    // 85