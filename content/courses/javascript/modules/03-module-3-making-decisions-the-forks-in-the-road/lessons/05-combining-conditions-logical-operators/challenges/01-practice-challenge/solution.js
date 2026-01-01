let isEmployee = true;
let hasKeycard = true;
let securityLevel = 2;

if ((isEmployee && hasKeycard) || securityLevel === 3) {
  console.log('Access Granted');
} else {
  console.log('Access Denied');
}