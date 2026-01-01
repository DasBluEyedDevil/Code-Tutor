// Greeting.razor

<h3>Welcome to Blazor!</h3>

<p>Year: @DateTime.Now.Year</p>

<div>
    <label>Your Name:</label>
    <!-- Input field here -->
</div>

<button @onclick="GreetUser">Greet Me</button>

<p>@greeting</p>

@code {
    private string name = "";
    private string greeting = "";
    
    private void GreetUser()
    {
        // Set greeting message
    }
}