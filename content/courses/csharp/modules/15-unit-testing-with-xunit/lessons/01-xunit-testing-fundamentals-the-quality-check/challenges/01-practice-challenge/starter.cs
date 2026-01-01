using Xunit;

public class StringHelper
{
    public string Reverse(string s)
    {
        // Implement: reverse the string
        return new string(s.Reverse().ToArray());
    }
    
    public bool IsPalindrome(string s)
    {
        // Implement: check if palindrome
        return s == Reverse(s);
    }
    
    public int CountWords(string s)
    {
        // Implement: count words
        if (string.IsNullOrWhiteSpace(s)) return 0;
        return s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
    }
}

public class StringHelperTests
{
    [Fact]
    public void Reverse_NormalString_ReversesCorrectly()
    {
        // Arrange
        var helper = new StringHelper();
        
        // Act
        string result = helper.Reverse("hello");
        
        // Assert
        Assert.Equal(/* expected */, result);
    }
    
    [Theory]
    [InlineData("racecar", true)]
    [InlineData("hello", false)]
    public void IsPalindrome_VariousInputs_ReturnsCorrectResult(
        string input, bool expected)
    {
        // Write test
    }
    
    // Add more tests for CountWords and edge cases!
}