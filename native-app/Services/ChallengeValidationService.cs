using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CodeTutor.Native.Models;
using CodeTutor.Native.Models.Challenges;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for validating challenge answers
/// </summary>
public class ChallengeValidationService : IChallengeValidationService
{
    private readonly ICodeExecutor _codeExecutor;

    public ChallengeValidationService(ICodeExecutor codeExecutor)
    {
        _codeExecutor = codeExecutor;
    }

    public ChallengeResult ValidateMultipleChoice(MultipleChoiceChallenge challenge, int selectedAnswer)
    {
        var isCorrect = selectedAnswer == challenge.CorrectAnswer;

        return new ChallengeResult
        {
            IsCorrect = isCorrect,
            Score = isCorrect ? challenge.Points : 0,
            MaxScore = challenge.Points,
            Feedback = isCorrect
                ? $"Correct! {challenge.Explanation}"
                : $"Incorrect. {challenge.Explanation}"
        };
    }

    public ChallengeResult ValidateTrueFalse(TrueFalseChallenge challenge, bool answer)
    {
        var isCorrect = answer == challenge.CorrectAnswer;

        return new ChallengeResult
        {
            IsCorrect = isCorrect,
            Score = isCorrect ? challenge.Points : 0,
            MaxScore = challenge.Points,
            Feedback = isCorrect
                ? $"Correct! {challenge.Explanation}"
                : $"Incorrect. {challenge.Explanation}"
        };
    }

    public ChallengeResult ValidateCodeOutput(CodeOutputChallenge challenge, string userOutput)
    {
        var normalizedUser = NormalizeOutput(userOutput);
        var normalizedExpected = NormalizeOutput(challenge.ExpectedOutput);

        var isCorrect = normalizedUser == normalizedExpected;

        return new ChallengeResult
        {
            IsCorrect = isCorrect,
            Score = isCorrect ? challenge.Points : 0,
            MaxScore = challenge.Points,
            Feedback = isCorrect
                ? $"Correct! {challenge.Explanation}"
                : $"Incorrect. Expected: {challenge.ExpectedOutput}\nYour answer: {userOutput}\n\n{challenge.Explanation}"
        };
    }

    public async Task<ChallengeResult> ValidateFreeCodingAsync(FreeCodingChallenge challenge, string code)
    {
        var testResults = new List<TestCaseResult>();
        var passedTests = 0;

        foreach (var testCase in challenge.TestCases)
        {
            var result = await ExecuteTestCaseAsync(challenge.Language, code, testCase);
            testResults.Add(result);

            if (result.Passed)
            {
                passedTests++;
            }
        }

        var totalTests = challenge.TestCases.Count;
        var allPassed = passedTests == totalTests;

        return new ChallengeResult
        {
            IsCorrect = allPassed,
            Score = allPassed ? challenge.Points : (int)((double)passedTests / totalTests * challenge.Points),
            MaxScore = challenge.Points,
            Feedback = allPassed
                ? $"All {totalTests} test cases passed! Great job!"
                : $"Passed {passedTests}/{totalTests} test cases. Keep trying!",
            TestResults = testResults
        };
    }

    public async Task<ChallengeResult> ValidateCodeCompletionAsync(CodeCompletionChallenge challenge, string code)
    {
        var testResults = new List<TestCaseResult>();
        var passedTests = 0;

        foreach (var testCase in challenge.TestCases)
        {
            var result = await ExecuteTestCaseAsync(challenge.Language, code, testCase);
            testResults.Add(result);

            if (result.Passed)
            {
                passedTests++;
            }
        }

        var totalTests = challenge.TestCases.Count;
        var allPassed = passedTests == totalTests;

        return new ChallengeResult
        {
            IsCorrect = allPassed,
            Score = allPassed ? challenge.Points : (int)((double)passedTests / totalTests * challenge.Points),
            MaxScore = challenge.Points,
            Feedback = allPassed
                ? $"All {totalTests} test cases passed! Code completed successfully!"
                : $"Passed {passedTests}/{totalTests} test cases. Check your TODO sections.",
            TestResults = testResults
        };
    }

    public ChallengeResult ValidateConceptual(ConceptualChallenge challenge, string answer)
    {
        // Basic validation - check minimum word count if specified
        var wordCount = answer.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
        var meetsMinWords = !challenge.MinWords.HasValue || wordCount >= challenge.MinWords.Value;

        // Check if key points are mentioned (simple keyword matching)
        var mentionedKeyPoints = 0;
        if (challenge.KeyPoints != null)
        {
            var lowerAnswer = answer.ToLowerInvariant();
            foreach (var keyPoint in challenge.KeyPoints)
            {
                // Simple keyword extraction and matching
                var keywords = keyPoint.ToLowerInvariant()
                    .Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(w => w.Length > 3); // Only significant words

                if (keywords.Any(keyword => lowerAnswer.Contains(keyword)))
                {
                    mentionedKeyPoints++;
                }
            }
        }

        var keyPointsCoverage = challenge.KeyPoints != null && challenge.KeyPoints.Count > 0
            ? (double)mentionedKeyPoints / challenge.KeyPoints.Count
            : 1.0;

        // Award partial credit based on word count and key point coverage
        var score = (int)(challenge.Points * (meetsMinWords ? 0.5 : 0.3) + challenge.Points * keyPointsCoverage * 0.5);
        score = Math.Max(0, Math.Min(score, challenge.Points));

        var feedback = "Your answer has been submitted. ";
        if (!meetsMinWords)
        {
            feedback += $"Consider expanding your answer (minimum {challenge.MinWords} words). ";
        }
        if (keyPointsCoverage < 0.5 && challenge.KeyPoints != null)
        {
            feedback += "Try to cover more key concepts. ";
        }

        return new ChallengeResult
        {
            IsCorrect = meetsMinWords && keyPointsCoverage >= 0.5,
            Score = score,
            MaxScore = challenge.Points,
            Feedback = feedback
        };
    }

    private async Task<TestCaseResult> ExecuteTestCaseAsync(string language, string code, TestCase testCase)
    {
        try
        {
            // Inject input if provided (for languages that support stdin)
            var codeToExecute = code;
            if (!string.IsNullOrEmpty(testCase.Input))
            {
                // This is a simplified approach - in production you'd want proper stdin injection
                codeToExecute = InjectInput(language, code, testCase.Input);
            }

            var executionResult = await _codeExecutor.ExecuteAsync(language, codeToExecute);

            if (!executionResult.Success)
            {
                return new TestCaseResult
                {
                    Description = testCase.Description,
                    Passed = false,
                    Error = executionResult.Error
                };
            }

            var normalizedActual = NormalizeOutput(executionResult.Output);
            var normalizedExpected = NormalizeOutput(testCase.ExpectedOutput);
            var passed = normalizedActual == normalizedExpected;

            return new TestCaseResult
            {
                Description = testCase.Description,
                Passed = passed,
                ActualOutput = executionResult.Output,
                ExpectedOutput = testCase.ExpectedOutput
            };
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            return new TestCaseResult
            {
                Description = testCase.Description,
                Passed = false,
                Error = $"Execution error: {ex.Message}"
            };
        }
    }

    private string NormalizeOutput(string output)
    {
        if (string.IsNullOrEmpty(output))
        {
            return string.Empty;
        }

        // Normalize line endings and trim whitespace
        return Regex.Replace(output.Trim(), @"\s+", " ");
    }

    private string InjectInput(string language, string code, string input)
    {
        // For test cases with input, we need to simulate stdin
        // We do this by prepending code that provides the input values

        var inputLines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        return language.ToLowerInvariant() switch
        {
            "python" => InjectPythonInput(code, inputLines),
            "javascript" or "js" => InjectJavaScriptInput(code, inputLines),
            "java" => InjectJavaInput(code, inputLines),
            "csharp" or "cs" => InjectCSharpInput(code, inputLines),
            "rust" => InjectRustInput(code, inputLines),
            _ => code // Fallback: return original code
        };
    }

    private string InjectPythonInput(string code, string[] inputLines)
    {
        // Replace input() calls with hardcoded values
        // Simple approach: create a mock input function
        var mockInput = "# Mock input for testing\n" +
                       $"_test_inputs = {System.Text.Json.JsonSerializer.Serialize(inputLines)}\n" +
                       "_test_input_index = 0\n" +
                       "def input(prompt=''):\n" +
                       "    global _test_input_index\n" +
                       "    if _test_input_index < len(_test_inputs):\n" +
                       "        val = _test_inputs[_test_input_index]\n" +
                       "        _test_input_index += 1\n" +
                       "        return val\n" +
                       "    return ''\n\n";

        return mockInput + code;
    }

    private string InjectJavaScriptInput(string code, string[] inputLines)
    {
        // Mock readline/prompt functionality
        var inputsJson = System.Text.Json.JsonSerializer.Serialize(inputLines);
        var mockReadline = $"// Mock input for testing\n" +
                          $"const _testInputs = {inputsJson};\n" +
                          "let _testInputIndex = 0;\n" +
                          "const readline = () => {\n" +
                          "  if (_testInputIndex < _testInputs.length) {\n" +
                          "    return _testInputs[_testInputIndex++];\n" +
                          "  }\n" +
                          "  return '';\n" +
                          "};\n\n";

        return mockReadline + code;
    }

    private string InjectJavaInput(string code, string[] inputLines)
    {
        // For Java, we need to mock Scanner input
        var inputsJoined = string.Join("\\n", inputLines);
        var mockScanner = $"// Mock Scanner for testing\n" +
                         "import java.io.ByteArrayInputStream;\n" +
                         "import java.util.Scanner;\n" +
                         "class MockInput {{\n" +
                         $"    static final String INPUT = \"{inputsJoined}\";\n" +
                         "    static Scanner getScanner() {{\n" +
                         "        return new Scanner(new ByteArrayInputStream(INPUT.getBytes()));\n" +
                         "    }}\n" +
                         "}}\n\n";

        // Replace Scanner(System.in) with MockInput.getScanner()
        var modifiedCode = code.Replace("new Scanner(System.in)", "MockInput.getScanner()");

        return mockScanner + modifiedCode;
    }

    private string InjectCSharpInput(string code, string[] inputLines)
    {
        // Mock Console.ReadLine() for C#
        var inputsJson = System.Text.Json.JsonSerializer.Serialize(inputLines);
        var mockConsole = $"// Mock Console.ReadLine for testing\n" +
                         "using System;\n" +
                         "using System.Linq;\n" +
                         "class MockConsole {{\n" +
                         $"    static string[] inputs = {inputsJson};\n" +
                         "    static int index = 0;\n" +
                         "    public static string ReadLine() {{\n" +
                         "        return index < inputs.Length ? inputs[index++] : \"\";\n" +
                         "    }}\n" +
                         "}}\n\n";

        // Replace Console.ReadLine() with MockConsole.ReadLine()
        var modifiedCode = code.Replace("Console.ReadLine()", "MockConsole.ReadLine()");

        return mockConsole + modifiedCode;
    }

    private string InjectRustInput(string code, string[] inputLines)
    {
        // For Rust, mock stdin reading
        var inputsJoined = string.Join("\\n", inputLines);
        var mockStdin = $"// Mock stdin for testing\n" +
                       "use std::io::{{self, BufRead}};\n" +
                       $"const TEST_INPUT: &str = \"{inputsJoined}\";\n" +
                       "fn read_line() -> String {{\n" +
                       "    static mut INDEX: usize = 0;\n" +
                       "    let lines: Vec<&str> = TEST_INPUT.lines().collect();\n" +
                       "    unsafe {{\n" +
                       "        if INDEX < lines.len() {{\n" +
                       "            let line = lines[INDEX].to_string();\n" +
                       "            INDEX += 1;\n" +
                       "            line\n" +
                       "        }} else {{\n" +
                       "            String::new()\n" +
                       "        }}\n" +
                       "    }}\n" +
                       "}}\n\n";

        return mockStdin + code;
    }
}
