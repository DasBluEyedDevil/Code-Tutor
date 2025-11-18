using CodeTutor.Native.Models.Challenges;
using CodeTutor.Native.ViewModels.Challenges;

namespace CodeTutor.Native.Services;

/// <summary>
/// Factory for creating challenge ViewModels based on challenge type
/// </summary>
public interface IChallengeFactory
{
    /// <summary>
    /// Create a challenge ViewModel for the given challenge
    /// </summary>
    ChallengeViewModelBase CreateViewModel(Challenge challenge);
}
