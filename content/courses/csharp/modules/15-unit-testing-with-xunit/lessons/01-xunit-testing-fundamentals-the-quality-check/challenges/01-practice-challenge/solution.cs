using Xunit;

public class StringHelper
{
    public string Reverse(string s)
    {
        return new string(s.Reverse().ToArray());
    }
    
    public bool IsPalindrome(string s)
    {
        return s.ToLower() == Reverse(s.ToLower());
    }
    
    public int CountWords(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return 0;
        return s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
    }
}

public class StringHelperTests
{
    [Fact]
    public void Reverse_NormalString_ReversesCorrectly()
    {
        var helper = new StringHelper();
        string result = helper.Reverse("hello");
        Assert.Equal("olleh", result);
    }
    
    [Theory]
    [InlineData("racecar", true)]
    [InlineData("hello", false)]
    [InlineData("Racecar", true)]
    public void IsPalindrome_VariousInputs_ReturnsCorrectResult(
        string input, bool expected)
    {
        var helper = new StringHelper();
        Assert.Equal(expected, helper.IsPalindrome(input));
    }
    
    [Theory]
    [InlineData("hello world", 2)]
    [InlineData("one", 1)]
    [InlineData("a b c d e", 5)]
    public void CountWords_VariousInputs_ReturnsCorrectCount(
        string input, int expected)
    {
        var helper = new StringHelper();
        Assert.Equal(expected, helper.CountWords(input));
    }
    
    [Fact]
    public void CountWords_EmptyString_ReturnsZero()
    {
        var helper = new StringHelper();
        Assert.Equal(0, helper.CountWords(""));
    }
    
    [Fact]
    public void Reverse_EmptyString_ReturnsEmpty()
    {
        var helper = new StringHelper();
        Assert.Equal("", helper.Reverse(""));
    }
}

Console.WriteLine("Tests defined! Run with: dotnet test");
Console.WriteLine("Test: Reverse('hello') = 'olleh'");
Console.WriteLine("Test: IsPalindrome('racecar') = true");
Console.WriteLine("Test: CountWords('hello world') = 2");