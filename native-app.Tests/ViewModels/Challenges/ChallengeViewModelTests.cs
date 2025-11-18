using CodeTutor.Native.Models.Challenges;
using CodeTutor.Native.Services;
using CodeTutor.Native.ViewModels.Challenges;
using CodeTutor.Native.Tests.Helpers;

namespace CodeTutor.Native.Tests.ViewModels.Challenges;

/// <summary>
/// Unit tests for ChallengeViewModelBase
/// </summary>
public class ChallengeViewModelBaseTests
{
    [Fact]
    public void ShowHintCommand_ShowsNextHint_WhenHintsAvailable()
    {
        // Arrange
        var challenge = TestDataGenerator.CreateMultipleChoiceChallenge();
        challenge.Hints = new List<string> { "Hint 1", "Hint 2", "Hint 3" };

        var viewModel = new TestChallengeViewModel(challenge);

        // Act & Assert
        viewModel.CurrentHint.Should().BeNull();
        viewModel.HasMoreHints.Should().BeTrue();

        viewModel.ShowHintCommand.Execute().Subscribe();
        viewModel.CurrentHint.Should().Be("Hint 1");
        viewModel.HintsUsed.Should().Be(1);

        viewModel.ShowHintCommand.Execute().Subscribe();
        viewModel.CurrentHint.Should().Be("Hint 2");
        viewModel.HintsUsed.Should().Be(2);

        viewModel.ShowHintCommand.Execute().Subscribe();
        viewModel.CurrentHint.Should().Be("Hint 3");
        viewModel.HintsUsed.Should().Be(3);
        viewModel.HasMoreHints.Should().BeFalse();
    }

    [Fact]
    public void ShowHintCommand_RaisesHintShownEvent()
    {
        // Arrange
        var challenge = TestDataGenerator.CreateMultipleChoiceChallenge();
        challenge.Hints = new List<string> { "Hint 1" };

        var viewModel = new TestChallengeViewModel(challenge);

        var eventRaised = false;
        viewModel.HintShown += (sender, args) => eventRaised = true;

        // Act
        viewModel.ShowHintCommand.Execute().Subscribe();

        // Assert
        eventRaised.Should().BeTrue();
    }

    [Fact]
    public void ResetCommand_ResetsState()
    {
        // Arrange
        var challenge = TestDataGenerator.CreateMultipleChoiceChallenge();
        challenge.Hints = new List<string> { "Hint 1", "Hint 2" };

        var viewModel = new TestChallengeViewModel(challenge);

        // Setup some state
        viewModel.ShowHintCommand.Execute().Subscribe();
        viewModel.ShowHintCommand.Execute().Subscribe();
        viewModel.SetHasSubmitted(true);

        // Act
        viewModel.ResetCommand.Execute().Subscribe();

        // Assert
        viewModel.HasSubmitted.Should().BeFalse();
        viewModel.Result.Should().BeNull();
        viewModel.CurrentHint.Should().BeNull();
        viewModel.HintsUsed.Should().Be(0);
        viewModel.HasMoreHints.Should().BeTrue();
    }

    [Fact]
    public void HasHints_ReturnsTrue_WhenHintsExist()
    {
        // Arrange
        var challenge = TestDataGenerator.CreateMultipleChoiceChallenge();
        challenge.Hints = new List<string> { "Hint 1" };

        var viewModel = new TestChallengeViewModel(challenge);

        // Assert
        viewModel.HasHints.Should().BeTrue();
    }

    [Fact]
    public void HasHints_ReturnsFalse_WhenNoHints()
    {
        // Arrange
        var challenge = TestDataGenerator.CreateMultipleChoiceChallenge();
        challenge.Hints = new List<string>();

        var viewModel = new TestChallengeViewModel(challenge);

        // Assert
        viewModel.HasHints.Should().BeFalse();
    }

    // Helper test class that exposes protected members
    private class TestChallengeViewModel : ChallengeViewModelBase
    {
        public TestChallengeViewModel(Challenge challenge) : base(challenge) { }

        public void SetHasSubmitted(bool value)
        {
            HasSubmitted = value;
        }
    }
}

/// <summary>
/// Unit tests for MultipleChoiceViewModel
/// </summary>
public class MultipleChoiceViewModelTests
{
    private readonly Mock<IChallengeValidationService> _mockValidationService;
    private readonly Mock<IErrorHandlerService> _mockErrorHandler;

    public MultipleChoiceViewModelTests()
    {
        _mockValidationService = new Mock<IChallengeValidationService>();
        _mockErrorHandler = new Mock<IErrorHandlerService>();
    }

    [Fact]
    public void Constructor_InitializesCorrectly()
    {
        // Arrange & Act
        var challenge = TestDataGenerator.CreateMultipleChoiceChallenge(correctAnswer: 2);
        var viewModel = CreateViewModel(challenge);

        // Assert
        viewModel.Question.Should().Be(challenge.Question);
        viewModel.Options.Should().BeEquivalentTo(challenge.Options);
        viewModel.SelectedOption.Should().BeNull();
        viewModel.HasSubmitted.Should().BeFalse();
    }

    [Fact]
    public void SubmitCommand_CanExecute_OnlyWhenOptionSelected()
    {
        // Arrange
        var challenge = TestDataGenerator.CreateMultipleChoiceChallenge();
        var viewModel = CreateViewModel(challenge);

        // Assert - Can't submit without selection
        viewModel.SubmitCommand.CanExecute.Subscribe(canExecute =>
        {
            canExecute.Should().BeFalse();
        });

        // Act - Select an option
        viewModel.SelectedOption = 0;

        // Assert - Can submit now
        viewModel.SubmitCommand.CanExecute.Subscribe(canExecute =>
        {
            canExecute.Should().BeTrue();
        });
    }

    [Fact]
    public void Submit_ValidatesAnswer_AndSetsResult()
    {
        // Arrange
        var challenge = TestDataGenerator.CreateMultipleChoiceChallenge(correctAnswer: 2);
        var expectedResult = new ChallengeResult
        {
            IsCorrect = true,
            Score = 100,
            Feedback = "Correct!"
        };

        _mockValidationService
            .Setup(x => x.ValidateMultipleChoice(challenge, 2))
            .Returns(expectedResult);

        var viewModel = CreateViewModel(challenge);
        viewModel.SelectedOption = 2;

        // Act
        viewModel.SubmitCommand.Execute().Subscribe();

        // Assert
        viewModel.Result.Should().NotBeNull();
        viewModel.Result!.IsCorrect.Should().BeTrue();
        viewModel.Result.Score.Should().Be(100);
        viewModel.HasSubmitted.Should().BeTrue();

        _mockValidationService.Verify(x => x.ValidateMultipleChoice(challenge, 2), Times.Once);
    }

    [Fact]
    public void Submit_HandlesIncorrectAnswer()
    {
        // Arrange
        var challenge = TestDataGenerator.CreateMultipleChoiceChallenge(correctAnswer: 0);
        var expectedResult = new ChallengeResult
        {
            IsCorrect = false,
            Score = 0,
            Feedback = "Incorrect. Try again!"
        };

        _mockValidationService
            .Setup(x => x.ValidateMultipleChoice(challenge, 2))
            .Returns(expectedResult);

        var viewModel = CreateViewModel(challenge);
        viewModel.SelectedOption = 2;

        // Act
        viewModel.SubmitCommand.Execute().Subscribe();

        // Assert
        viewModel.Result.Should().NotBeNull();
        viewModel.Result!.IsCorrect.Should().BeFalse();
        viewModel.HasSubmitted.Should().BeTrue();
    }

    [Fact]
    public void Reset_ClearsSelectedOption()
    {
        // Arrange
        var challenge = TestDataGenerator.CreateMultipleChoiceChallenge();
        var viewModel = CreateViewModel(challenge);

        viewModel.SelectedOption = 1;

        // Act
        viewModel.ResetCommand.Execute().Subscribe();

        // Assert
        viewModel.SelectedOption.Should().BeNull();
        viewModel.HasSubmitted.Should().BeFalse();
    }

    private MultipleChoiceViewModel CreateViewModel(MultipleChoiceChallenge challenge)
    {
        return new MultipleChoiceViewModel(
            challenge,
            _mockValidationService.Object,
            _mockErrorHandler.Object);
    }
}

/// <summary>
/// Unit tests for TrueFalseViewModel
/// </summary>
public class TrueFalseViewModelTests
{
    private readonly Mock<IChallengeValidationService> _mockValidationService;
    private readonly Mock<IErrorHandlerService> _mockErrorHandler;

    public TrueFalseViewModelTests()
    {
        _mockValidationService = new Mock<IChallengeValidationService>();
        _mockErrorHandler = new Mock<IErrorHandlerService>();
    }

    [Fact]
    public void Constructor_InitializesCorrectly()
    {
        // Arrange
        var challenge = new TrueFalseChallenge
        {
            Id = "tf1",
            Type = "TrueFalse",
            Title = "Test True/False",
            Description = "Test description",
            Points = 10,
            Question = "Is C# a programming language?",
            CorrectAnswer = true,
            Explanation = "C# is indeed a programming language",
            Hints = new List<string>()
        };

        // Act
        var viewModel = CreateViewModel(challenge);

        // Assert
        viewModel.Question.Should().Be(challenge.Question);
        viewModel.SelectedAnswer.Should().BeNull();
        viewModel.HasSubmitted.Should().BeFalse();
    }

    [Fact]
    public void SubmitCommand_CanExecute_OnlyWhenAnswerSelected()
    {
        // Arrange
        var challenge = CreateTrueFalseChallenge(correctAnswer: true);
        var viewModel = CreateViewModel(challenge);

        // Act - Select True
        viewModel.SelectedAnswer = true;

        // Assert - Should be able to submit
        bool canExecute = false;
        viewModel.SubmitCommand.CanExecute.Subscribe(x => canExecute = x);
        canExecute.Should().BeTrue();
    }

    [Fact]
    public void Submit_ValidatesAnswer_AndSetsResult()
    {
        // Arrange
        var challenge = CreateTrueFalseChallenge(correctAnswer: true);
        var expectedResult = new ChallengeResult
        {
            IsCorrect = true,
            Score = 100,
            Feedback = "Correct!"
        };

        _mockValidationService
            .Setup(x => x.ValidateTrueFalse(challenge, true))
            .Returns(expectedResult);

        var viewModel = CreateViewModel(challenge);
        viewModel.SelectedAnswer = true;

        // Act
        viewModel.SubmitCommand.Execute().Subscribe();

        // Assert
        viewModel.Result.Should().NotBeNull();
        viewModel.Result!.IsCorrect.Should().BeTrue();
        viewModel.HasSubmitted.Should().BeTrue();
    }

    [Fact]
    public void Reset_ClearsSelectedAnswer()
    {
        // Arrange
        var challenge = CreateTrueFalseChallenge(correctAnswer: true);
        var viewModel = CreateViewModel(challenge);

        viewModel.SelectedAnswer = false;

        // Act
        viewModel.ResetCommand.Execute().Subscribe();

        // Assert
        viewModel.SelectedAnswer.Should().BeNull();
        viewModel.HasSubmitted.Should().BeFalse();
    }

    private TrueFalseChallenge CreateTrueFalseChallenge(bool correctAnswer)
    {
        return new TrueFalseChallenge
        {
            Id = "tf1",
            Type = "TrueFalse",
            Title = "Test True/False",
            Description = "Test description",
            Points = 10,
            Question = "Is this true?",
            CorrectAnswer = correctAnswer,
            Explanation = "Explanation",
            Hints = new List<string>()
        };
    }

    private TrueFalseViewModel CreateViewModel(TrueFalseChallenge challenge)
    {
        return new TrueFalseViewModel(
            challenge,
            _mockValidationService.Object,
            _mockErrorHandler.Object);
    }
}

/// <summary>
/// Unit tests for ConceptualViewModel
/// </summary>
public class ConceptualViewModelTests
{
    private readonly Mock<IChallengeValidationService> _mockValidationService;
    private readonly Mock<IErrorHandlerService> _mockErrorHandler;

    public ConceptualViewModelTests()
    {
        _mockValidationService = new Mock<IChallengeValidationService>();
        _mockErrorHandler = new Mock<IErrorHandlerService>();
    }

    [Fact]
    public void Constructor_InitializesCorrectly()
    {
        // Arrange
        var challenge = CreateConceptualChallenge();

        // Act
        var viewModel = CreateViewModel(challenge);

        // Assert
        viewModel.Question.Should().Be(challenge.Question);
        viewModel.UserAnswer.Should().BeEmpty();
        viewModel.HasSubmitted.Should().BeFalse();
    }

    [Fact]
    public void SubmitCommand_CanExecute_OnlyWhenAnswerProvided()
    {
        // Arrange
        var challenge = CreateConceptualChallenge();
        var viewModel = CreateViewModel(challenge);

        // Act - Provide answer
        viewModel.UserAnswer = "Encapsulation is...";

        // Assert
        bool canExecute = false;
        viewModel.SubmitCommand.CanExecute.Subscribe(x => canExecute = x);
        canExecute.Should().BeTrue();
    }

    [Fact]
    public void Submit_ValidatesAnswer_AndSetsResult()
    {
        // Arrange
        var challenge = CreateConceptualChallenge();
        var expectedResult = new ChallengeResult
        {
            IsCorrect = true,
            Score = 90,
            Feedback = "Good explanation!"
        };

        _mockValidationService
            .Setup(x => x.ValidateConceptual(challenge, "Encapsulation is bundling data"))
            .Returns(expectedResult);

        var viewModel = CreateViewModel(challenge);
        viewModel.UserAnswer = "Encapsulation is bundling data";

        // Act
        viewModel.SubmitCommand.Execute().Subscribe();

        // Assert
        viewModel.Result.Should().NotBeNull();
        viewModel.Result!.IsCorrect.Should().BeTrue();
        viewModel.HasSubmitted.Should().BeTrue();
    }

    [Fact]
    public void Reset_ClearsUserAnswer()
    {
        // Arrange
        var challenge = CreateConceptualChallenge();
        var viewModel = CreateViewModel(challenge);

        viewModel.UserAnswer = "Some answer";

        // Act
        viewModel.ResetCommand.Execute().Subscribe();

        // Assert
        viewModel.UserAnswer.Should().BeEmpty();
        viewModel.HasSubmitted.Should().BeFalse();
    }

    private ConceptualChallenge CreateConceptualChallenge()
    {
        return new ConceptualChallenge
        {
            Id = "conceptual1",
            Type = "Conceptual",
            Title = "Test Conceptual",
            Description = "Test description",
            Points = 50,
            Question = "What is encapsulation?",
            KeyConcepts = new List<string> { "bundling", "data", "methods" },
            ModelAnswer = "Encapsulation is bundling data and methods",
            Hints = new List<string>()
        };
    }

    private ConceptualViewModel CreateViewModel(ConceptualChallenge challenge)
    {
        return new ConceptualViewModel(
            challenge,
            _mockValidationService.Object,
            _mockErrorHandler.Object);
    }
}
