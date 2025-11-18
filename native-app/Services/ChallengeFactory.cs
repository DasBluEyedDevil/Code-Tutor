using System;
using CodeTutor.Native.Models.Challenges;
using CodeTutor.Native.ViewModels.Challenges;

namespace CodeTutor.Native.Services;

/// <summary>
/// Factory for creating challenge ViewModels based on challenge type
/// </summary>
public class ChallengeFactory : IChallengeFactory
{
    private readonly IChallengeValidationService _validationService;
    private readonly ICodeExecutor _codeExecutor;

    public ChallengeFactory(
        IChallengeValidationService validationService,
        ICodeExecutor codeExecutor)
    {
        _validationService = validationService;
        _codeExecutor = codeExecutor;
    }

    public ChallengeViewModelBase CreateViewModel(Challenge challenge)
    {
        return challenge switch
        {
            MultipleChoiceChallenge mc => new MultipleChoiceViewModel(mc, _validationService),
            TrueFalseChallenge tf => new TrueFalseViewModel(tf, _validationService),
            CodeOutputChallenge co => new CodeOutputViewModel(co, _validationService, _codeExecutor),
            FreeCodingChallenge fc => new FreeCodingViewModel(fc, _validationService),
            CodeCompletionChallenge cc => new CodeCompletionViewModel(cc, _validationService),
            ConceptualChallenge con => new ConceptualViewModel(con, _validationService),
            _ => throw new NotSupportedException($"Challenge type '{challenge.Type}' is not supported.")
        };
    }
}
