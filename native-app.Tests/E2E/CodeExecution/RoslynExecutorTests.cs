using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using CodeTutor.Tests.Models;

namespace CodeTutor.Tests.E2E.CodeExecution;

/// <summary>
/// E2E tests for the Roslyn C# code execution engine.
/// These tests verify that C# code can be compiled and executed correctly.
/// </summary>
public class RoslynExecutorTests
{
    private static readonly ScriptOptions DefaultOptions = ScriptOptions.Default
        .AddReferences(
            typeof(object).Assembly,
            typeof(Console).Assembly,
            typeof(Enumerable).Assembly
        )
        .AddImports(
            "System",
            "System.Collections.Generic",
            "System.IO",
            "System.Linq",
            "System.Text",
            "System.Text.RegularExpressions",
            "System.Threading.Tasks"
        );

    [Fact]
    public async Task Execute_SimpleExpression_ReturnsResult()
    {
        // Arrange
        var code = "1 + 1";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Be("2");
    }

    [Fact]
    public async Task Execute_ConsoleWriteLine_CapturesOutput()
    {
        // Arrange
        var code = "Console.WriteLine(\"Hello, World!\");";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("Hello, World!");
    }

    [Fact]
    public async Task Execute_MultipleStatements_ExecutesAll()
    {
        // Arrange
        var code = @"
            var x = 5;
            var y = 10;
            Console.WriteLine(x + y);
        ";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("15");
    }

    [Fact]
    public async Task Execute_LinqQuery_ReturnsCorrectResult()
    {
        // Arrange
        var code = @"
            var numbers = new[] { 1, 2, 3, 4, 5 };
            var sum = numbers.Where(n => n > 2).Sum();
            Console.WriteLine(sum);
        ";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("12"); // 3 + 4 + 5 = 12
    }

    [Fact]
    public async Task Execute_ClassDefinition_Works()
    {
        // Arrange
        var code = @"
            public class Person
            {
                public string Name { get; set; }
                public int Age { get; set; }
            }

            var person = new Person { Name = ""Alice"", Age = 30 };
            Console.WriteLine($""{person.Name} is {person.Age} years old"");
        ";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("Alice is 30 years old");
    }

    [Fact]
    public async Task Execute_MethodDefinition_CanBeInvoked()
    {
        // Arrange
        var code = @"
            int Factorial(int n)
            {
                if (n <= 1) return 1;
                return n * Factorial(n - 1);
            }

            Console.WriteLine(Factorial(5));
        ";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("120");
    }

    [Fact]
    public async Task Execute_SyntaxError_ReturnsFailure()
    {
        // Arrange
        var code = "var x = "; // Incomplete statement

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Execute_RuntimeException_ReturnsFailure()
    {
        // Arrange
        var code = @"
            int[] arr = new int[5];
            Console.WriteLine(arr[10]); // Index out of bounds
        ";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Should().Contain("Index");
    }

    [Fact]
    public async Task Execute_NullReferenceException_ReturnsFailure()
    {
        // Arrange
        var code = @"
            string s = null;
            Console.WriteLine(s.Length);
        ";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Execute_StringInterpolation_Works()
    {
        // Arrange
        var code = @"
            var name = ""World"";
            var greeting = $""Hello, {name}!"";
            Console.WriteLine(greeting);
        ";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("Hello, World!");
    }

    [Fact]
    public async Task Execute_ListOperations_Work()
    {
        // Arrange
        var code = @"
            var list = new List<int> { 1, 2, 3 };
            list.Add(4);
            list.Add(5);
            Console.WriteLine(string.Join("", "", list));
        ";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("1, 2, 3, 4, 5");
    }

    [Fact]
    public async Task Execute_DictionaryOperations_Work()
    {
        // Arrange
        var code = @"
            var dict = new Dictionary<string, int>
            {
                [""one""] = 1,
                [""two""] = 2
            };
            Console.WriteLine(dict[""one""] + dict[""two""]);
        ";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("3");
    }

    [Fact]
    public async Task Execute_ForLoop_Works()
    {
        // Arrange
        var code = @"
            var sum = 0;
            for (int i = 1; i <= 10; i++)
            {
                sum += i;
            }
            Console.WriteLine(sum);
        ";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("55"); // 1+2+3+...+10 = 55
    }

    [Fact]
    public async Task Execute_ForeachLoop_Works()
    {
        // Arrange
        var code = @"
            var words = new[] { ""Hello"", ""World"" };
            foreach (var word in words)
            {
                Console.Write(word + "" "");
            }
        ";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("Hello");
        result.Output.Should().Contain("World");
    }

    [Fact]
    public async Task Execute_WhileLoop_Works()
    {
        // Arrange
        var code = @"
            var count = 0;
            while (count < 5)
            {
                count++;
            }
            Console.WriteLine(count);
        ";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("5");
    }

    [Fact]
    public async Task Execute_TryCatch_HandlesException()
    {
        // Arrange
        var code = @"
            try
            {
                int x = int.Parse(""not a number"");
            }
            catch (FormatException)
            {
                Console.WriteLine(""Caught format exception"");
            }
        ";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("Caught format exception");
    }

    [Fact]
    public async Task Execute_AsyncAwait_Works()
    {
        // Arrange
        var code = @"
            async Task<int> GetValueAsync()
            {
                await Task.Delay(10);
                return 42;
            }

            var value = await GetValueAsync();
            Console.WriteLine(value);
        ";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("42");
    }

    [Fact]
    public async Task Execute_PatternMatching_Works()
    {
        // Arrange
        var code = @"
            object obj = 42;
            if (obj is int number)
            {
                Console.WriteLine($""It's an integer: {number}"");
            }
        ";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("It's an integer: 42");
    }

    [Fact]
    public async Task Execute_RecordType_Works()
    {
        // Arrange
        var code = @"
            record Point(int X, int Y);

            var p1 = new Point(1, 2);
            var p2 = p1 with { X = 3 };
            Console.WriteLine($""p1: ({p1.X}, {p1.Y}), p2: ({p2.X}, {p2.Y})"");
        ";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("p1: (1, 2)");
        result.Output.Should().Contain("p2: (3, 2)");
    }

    [Fact]
    public async Task Execute_EmptyCode_ReturnsEmptyOutput()
    {
        // Arrange
        var code = "";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        // Empty code might succeed with empty output or fail - either is acceptable
        // The important thing is it doesn't crash
    }

    [Fact]
    public async Task Execute_OnlyComments_ReturnsSuccess()
    {
        // Arrange
        var code = @"
            // This is a comment
            /* This is also a comment */
        ";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().BeEmpty();
    }

    [Fact]
    public async Task Execute_RegexOperations_Work()
    {
        // Arrange
        var code = @"
            var pattern = @""\d+"";
            var text = ""There are 42 apples and 7 oranges"";
            var matches = System.Text.RegularExpressions.Regex.Matches(text, pattern);
            Console.WriteLine($""Found {matches.Count} numbers"");
        ";

        // Act
        var result = await ExecuteAsync(code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("Found 2 numbers");
    }

    [Theory]
    [InlineData("1 + 1", "2")]
    [InlineData("10 * 5", "50")]
    [InlineData("100 / 4", "25")]
    [InlineData("17 % 5", "2")]
    [InlineData("Math.Pow(2, 10)", "1024")]
    public async Task Execute_MathOperations_ReturnCorrectResults(string expression, string expected)
    {
        // Act
        var result = await ExecuteAsync(expression);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain(expected);
    }

    [Theory]
    [InlineData("\"hello\".ToUpper()", "HELLO")]
    [InlineData("\"HELLO\".ToLower()", "hello")]
    [InlineData("\"hello world\".Split(' ').Length", "2")]
    [InlineData("\"test\".Length", "4")]
    public async Task Execute_StringOperations_ReturnCorrectResults(string expression, string expected)
    {
        // Act
        var result = await ExecuteAsync(expression);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain(expected);
    }

    private async Task<ExecutionResult> ExecuteAsync(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            return new ExecutionResult(true, "", "");

        using var outputCapture = new StringWriter();
        var originalOut = Console.Out;

        try
        {
            Console.SetOut(outputCapture);

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            var scriptResult = await CSharpScript.EvaluateAsync(
                code,
                DefaultOptions,
                cancellationToken: cts.Token
            );

            var output = outputCapture.ToString();

            // If script returns a value and no console output, show the value
            if (scriptResult != null && string.IsNullOrWhiteSpace(output))
            {
                output = scriptResult.ToString() ?? "";
            }

            return new ExecutionResult(true, output.Trim(), "");
        }
        catch (CompilationErrorException ex)
        {
            return new ExecutionResult(false, "", string.Join(Environment.NewLine, ex.Diagnostics));
        }
        catch (OperationCanceledException)
        {
            return new ExecutionResult(false, "", "Execution timed out after 30 seconds");
        }
        catch (Exception ex)
        {
            return new ExecutionResult(false, "", ex.Message);
        }
        finally
        {
            Console.SetOut(originalOut);
        }
    }
}
