// Create password length variable
int passwordLength = 3;

// While loop to check password strength
while (passwordLength < 8)
{
    Console.WriteLine("Password too short: " + passwordLength + " characters");
    passwordLength++;
}

// After loop ends
Console.WriteLine("Password is strong!");