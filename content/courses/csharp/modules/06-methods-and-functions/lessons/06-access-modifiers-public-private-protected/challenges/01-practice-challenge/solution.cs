class User
{
    private string password;
    private int loginAttempts = 0;
    
    public string Username { get; set; }
    
    public User(string username, string password)
    {
        this.Username = username;
        this.password = password;
    }
    
    public bool Login(string pwd)
    {
        if (IsLockedOut())
        {
            Console.WriteLine("Account locked! Too many attempts.");
            return false;
        }
        
        if (pwd == password)
        {
            loginAttempts = 0;
            Console.WriteLine("Login successful!");
            return true;
        }
        else
        {
            loginAttempts++;
            Console.WriteLine("Wrong password! Attempts: " + loginAttempts);
            return false;
        }
    }
    
    private bool IsLockedOut()
    {
        return loginAttempts >= 3;
    }
}

User user = new User("alice", "secret123");
user.Login("wrong");
user.Login("wrong2");
user.Login("secret123");