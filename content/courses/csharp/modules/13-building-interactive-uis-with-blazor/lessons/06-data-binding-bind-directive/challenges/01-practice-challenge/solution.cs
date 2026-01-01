Console.WriteLine(@"
// ProfileEditor.razor
using System;

<div class=""profile-editor container"">
    <div class=""row"">
        <div class=""col-md-6"">
            <h3>✏️ Edit Profile</h3>
            
            <div class=""mb-3"">
                <label class=""form-label"">Name:</label>
                <input @bind=""profile.Name"" @bind:event=""oninput"" 
                       class=""form-control"" placeholder=""Enter your name"" />
            </div>
            
            <div class=""mb-3"">
                <label class=""form-label"">Email:</label>
                <input @bind=""profile.Email"" type=""email"" 
                       class=""form-control"" placeholder=""your@email.com"" />
            </div>
            
            <div class=""mb-3"">
                <label class=""form-label"">Age:</label>
                <input @bind=""profile.Age"" type=""number"" 
                       class=""form-control"" min=""0"" max=""120"" />
            </div>
            
            <div class=""mb-3"">
                <label class=""form-label"">Join Date:</label>
                <input @bind=""profile.JoinDate"" type=""date"" 
                       class=""form-control"" />
            </div>
            
            <div class=""mb-3 form-check"">
                <input @bind=""profile.IsActive"" type=""checkbox"" 
                       class=""form-check-input"" id=""activeCheck"" />
                <label class=""form-check-label"" for=""activeCheck"">Active Member</label>
            </div>
            
            <div class=""mb-3"">
                <label class=""form-label"">Bio:</label>
                <textarea @bind=""profile.Bio"" @bind:event=""oninput""
                          class=""form-control"" rows=""4"" 
                          placeholder=""Tell us about yourself..."">
                </textarea>
                <small class=""text-muted"">Characters: @profile.Bio.Length / 500</small>
            </div>
        </div>
        
        <div class=""col-md-6"">
            <h3>👁️ Live Preview</h3>
            <div class=""card"">
                <div class=""card-body"">
                    <h5 class=""card-title"">@(string.IsNullOrEmpty(profile.Name) ? ""(No name)"" : profile.Name)</h5>
                    
                    <p class=""card-text"">
                        <strong>Email:</strong> @(string.IsNullOrEmpty(profile.Email) ? ""(Not provided)"" : profile.Email)<br/>
                        <strong>Age:</strong> @profile.Age years<br/>
                        <strong>Member Since:</strong> @profile.JoinDate.ToString(""MMM dd, yyyy"")<br/>
                        <strong>Status:</strong> 
                        <span class=""badge bg-@(profile.IsActive ? \""success\"" : \""secondary\"")"">
                            @(profile.IsActive ? ""Active"" : ""Inactive"")
                        </span>
                    </p>
                    
                    @if (!string.IsNullOrEmpty(profile.Bio))
                    {
                        <hr />
                        <p class=""card-text"">
                            <strong>Bio:</strong><br/>
                            @profile.Bio
                        </p>
                    }
                </div>
            </div>
        </div>
    </div>
</div>

@code {
    private class UserProfile {
        public string Name { get; set; } = """";
        public string Email { get; set; } = """";
        public int Age { get; set; } = 18;
        public DateTime JoinDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public string Bio { get; set; } = """";
    }
    
    private UserProfile profile = new();
}
"");

Console.WriteLine(@"
=== DATA BINDING FEATURES ===");
Console.WriteLine("✓ @bind for two-way sync");
Console.WriteLine("✓ @bind:event=\"oninput\" for live updates");
Console.WriteLine("✓ Works with: string, int, DateTime, bool");
Console.WriteLine("✓ Input, textarea, checkbox, select all supported");
Console.WriteLine("✓ Live preview updates as you type!");
Console.WriteLine("\n✓ No manual event handlers needed!");