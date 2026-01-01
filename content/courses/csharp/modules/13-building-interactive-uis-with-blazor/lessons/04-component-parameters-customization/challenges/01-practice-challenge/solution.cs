using System;

Console.WriteLine(@"
=== CUSTOMBUTTON COMPONENT ===");
Console.WriteLine(@"
// CustomButton.razor
<button 
    class=""btn btn-@Color @(IsDisabled ? \""disabled\"" : """")"" 
    disabled=""@IsDisabled""
    @onclick=""HandleClick""
    style=""margin: 5px;"">
    @Text
</button>

@code {
    [Parameter]
    public string Text { get; set; } = ""Button"";
    
    [Parameter]
    public string Color { get; set; } = ""primary"";
    
    [Parameter]
    public bool IsDisabled { get; set; } = false;
    
    [Parameter]
    public EventCallback<string> OnClick { get; set; }
    
    private async Task HandleClick()
    {
        if (!IsDisabled)
        {
            Console.WriteLine($""Button '{Text}' clicked!"");
            await OnClick.InvokeAsync(Text);
        }
    }
}
"");

Console.WriteLine(@"
=== BUTTONGROUP COMPONENT ===");
Console.WriteLine(@"
// ButtonGroup.razor
<div class=""button-group-demo"">
    <h4>Action Buttons</h4>
    
    <div class=""btn-group"">
        <CustomButton 
            Text=""Save"" 
            Color=""success" 
            IsDisabled=""false"
            OnClick=""HandleAction"" />
        
        <CustomButton 
            Text=""Delete"" 
            Color=""danger" 
            IsDisabled=""false"
            OnClick=""HandleAction"" />
        
        <CustomButton 
            Text=""Cancel"" 
            Color=""secondary" 
            IsDisabled=""false"
            OnClick=""HandleAction"" />
        
        <CustomButton 
            Text=""Disabled"" 
            Color=""primary" 
            IsDisabled=""true"
            OnClick=""HandleAction"" />
    </div>
    
    <div class=""alert alert-info mt-3"">
        <strong>Last Action:</strong> @lastAction
    </div>
    
    @if (actionCount > 0)
    {
        <p><small>Total actions: @actionCount</small></p>
    }
</div>

@code {
    private string lastAction = ""None"";
    private int actionCount = 0;
    
    private void HandleAction(string buttonText)
    {
        lastAction = buttonText;
        actionCount++;
        Console.WriteLine($""Action performed: {buttonText} (Total: {actionCount})"");
    }
}
"");

Console.WriteLine(@"
=== PARAMETER BENEFITS ===");
Console.WriteLine("✓ ONE CustomButton component");
Console.WriteLine("✓ MULTIPLE instances with different:");
Console.WriteLine("  - Text (\"Save\", \"Delete\", \"Cancel\")");
Console.WriteLine("  - Color (success, danger, secondary)");
Console.WriteLine("  - IsDisabled (true/false)");
Console.WriteLine("  - OnClick (different actions)");
Console.WriteLine("\n✓ Reusability + Flexibility = Powerful components!");