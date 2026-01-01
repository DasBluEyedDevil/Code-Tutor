// ProfileEditor.razor
<div class="profile-editor">
    <h3>Edit Profile</h3>
    
    <div class="form-group">
        <label>Name:</label>
        <input @bind="profile.Name" @bind:event="oninput" class="form-control" />
    </div>
    
    <!-- Add other inputs -->
    
    <h4>Preview</h4>
    <div class="card">
        <h5>@profile.Name</h5>
        <p>Email: @profile.Email</p>
        <!-- Show other fields -->
    </div>
</div>

@code {
    private class UserProfile {
        public string Name { get; set; } = "";
        // Add other properties
    }
    
    private UserProfile profile = new();
}