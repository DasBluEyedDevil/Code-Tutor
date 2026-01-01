// Variables
string operation = "+";
int num1 = 10;
int num2 = 5;

// Write your switch statement here
switch (operation)
{
    case "+":
        Console.WriteLine(num1 + num2);
        break;
    case "-":
        Console.WriteLine(num1 - num2);
        break;
    case "*":
        Console.WriteLine(num1 * num2);
        break;
    case "/":
        Console.WriteLine(num1 / num2);
        break;
    default:
        Console.WriteLine("Unknown operation");
        break;
}