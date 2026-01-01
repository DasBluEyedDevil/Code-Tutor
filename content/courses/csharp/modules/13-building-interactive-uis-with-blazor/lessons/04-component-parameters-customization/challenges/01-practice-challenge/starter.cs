// CustomButton.razor
<button 
    class="btn btn-@Color" 
    disabled="@IsDisabled"
    @onclick="HandleClick">
    @Text
</button>

@code {
    [Parameter]
    public string Text { get; set; } = "Button";
    
    [Parameter]
    public string Color { get; set; } = "primary";
    
    [Parameter]
    public bool IsDisabled { get; set; } = false;
    
    [Parameter]
    public EventCallback OnClick { get; set; }
    
    private async Task HandleClick()
    {
        if (!IsDisabled)
            await OnClick.InvokeAsync();
    }
}

// ButtonGroup.razor
<div>
    <h4>Actions</h4>
    
    <CustomButton 
        Text="Save" 
        Color="success" 
        OnClick="() => HandleAction(\"Save\")" />
    
    <!-- Add Delete and Cancel buttons -->
    
    <p class="mt-3">Last Action: @lastAction</p>
</div>

@code {
    private string lastAction = "None";
    
    private void HandleAction(string action)
    {
        lastAction = action;
    }
}