// Greeting.razor
using System;

Console.WriteLine(@"
=== BLAZOR COMPONENT EXAMPLE ===");
Console.WriteLine(@"
<h3>Welcome to Blazor!</h3>

<p>Year: @DateTime.Now.Year</p>

<div>
    <label>Your Name:</label>
    <input @bind=""name"" placeholder=""Enter your name"" />
</div>

<button class=""btn btn-primary"" @onclick=""GreetUser"">Greet Me</button>

@if (!string.IsNullOrEmpty(greeting))
{
    <div class=""alert alert-success"">
        <p>@greeting</p>
    </div>
}

@code {
    private string name = """";
    private string greeting = """";
    
    private void GreetUser()
    {
        if (!string.IsNullOrEmpty(name))
        {
            greeting = $""Hello, {name}! Welcome to Blazor!"";
        }
        else
        {
            greeting = ""Please enter your name first!"";
        }
    }
}
"");

Console.WriteLine(@"
=== HOW IT WORKS ===");
Console.WriteLine("1. @bind=\"name\" creates two-way data binding");
Console.WriteLine("2. User types → name variable updates automatically");
Console.WriteLine("3. @onclick=\"GreetUser\" calls C# method on click");
Console.WriteLine("4. GreetUser() updates greeting variable");
Console.WriteLine("5. @greeting displays the message (re-renders automatically!)");
Console.WriteLine("\n✓ All logic in C#, no JavaScript needed!");