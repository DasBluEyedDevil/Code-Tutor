using System.Threading.Tasks;
using CodeTutor.Native.Models;
using CodeTutor.Native.Models.Challenges;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for validating challenge answers
/// </summary>
public interface IChallengeValidationService
{
    /// <summary>
    /// Validate a multiple choice answer
    /// </summary>
    ChallengeResult ValidateMultipleChoice(MultipleChoiceChallenge challenge, int selectedAnswer);

    /// <summary>
    /// Validate a true/false answer
    /// </summary>
    ChallengeResult ValidateTrueFalse(TrueFalseChallenge challenge, bool answer);

    /// <summary>
    /// Validate a code output answer
    /// </summary>
    ChallengeResult ValidateCodeOutput(CodeOutputChallenge challenge, string userOutput);

    /// <summary>
    /// Validate a free coding answer by running test cases
    /// </summary>
    Task<ChallengeResult> ValidateFreeCodingAsync(FreeCodingChallenge challenge, string code);

    /// <summary>
    /// Validate a code completion answer by running test cases
    /// </summary>
    Task<ChallengeResult> ValidateCodeCompletionAsync(CodeCompletionChallenge challenge, string code);

    /// <summary>
    /// Validate a conceptual answer (basic validation - manual grading may be needed)
    /// </summary>
    ChallengeResult ValidateConceptual(ConceptualChallenge challenge, string answer);
}
