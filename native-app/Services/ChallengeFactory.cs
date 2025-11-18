using System;
using Microsoft.Extensions.DependencyInjection;
using CodeTutor.Native.Models.Challenges;
using CodeTutor.Native.ViewModels.Challenges;

namespace CodeTutor.Native.Services;

/// <summary>
/// Factory for creating challenge ViewModels based on challenge type
/// </summary>
public class ChallengeFactory : IChallengeFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ChallengeFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ChallengeViewModelBase CreateViewModel(Challenge challenge)
    {
        // Resolve singleton services
        var validationService = _serviceProvider.GetRequiredService<IChallengeValidationService>();
        var codeExecutor = _serviceProvider.GetRequiredService<ICodeExecutor>();
        var errorHandler = _serviceProvider.GetRequiredService<IErrorHandlerService>();

        return challenge switch
        {
            MultipleChoiceChallenge mc => new MultipleChoiceViewModel(mc, validationService, errorHandler),
            TrueFalseChallenge tf => new TrueFalseViewModel(tf, validationService, errorHandler),
            CodeOutputChallenge co => new CodeOutputViewModel(co, validationService, codeExecutor, errorHandler),
            FreeCodingChallenge fc => new FreeCodingViewModel(fc, validationService, errorHandler),
            CodeCompletionChallenge cc => new CodeCompletionViewModel(cc, validationService, errorHandler),
            ConceptualChallenge con => new ConceptualViewModel(con, validationService, errorHandler),
            _ => throw new NotSupportedException($"Challenge type '{challenge.Type}' is not supported.")
        };
    }
}
