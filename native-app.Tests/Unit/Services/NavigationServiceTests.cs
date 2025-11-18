using Microsoft.Extensions.DependencyInjection;
using CodeTutor.Native.Services;
using CodeTutor.Native.ViewModels;
using CodeTutor.Native.ViewModels.Pages;

namespace CodeTutor.Native.Tests.Unit.Services;

/// <summary>
/// Unit tests for NavigationService
/// </summary>
public class NavigationServiceTests
{
    private readonly ServiceProvider _serviceProvider;

    public NavigationServiceTests()
    {
        // Setup DI container with test ViewModels
        var services = new ServiceCollection();
        services.AddScoped<TestViewModel>();
        services.AddScoped<TestViewModel2>();
        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public void NavigateTo_CreatesNewScope_AndResolvesViewModel()
    {
        // Arrange
        var navigationService = new NavigationService(_serviceProvider);

        // Act
        navigationService.NavigateTo<TestViewModel>();

        // Assert
        navigationService.CurrentViewModel.Should().NotBeNull();
        navigationService.CurrentViewModel.Should().BeOfType<TestViewModel>();
        navigationService.CanGoBack.Should().BeFalse();
    }

    [Fact]
    public void NavigateTo_WithParameter_PassesParameterToViewModel()
    {
        // Arrange
        var navigationService = new NavigationService(_serviceProvider);
        var parameter = "test-param";

        // Act
        navigationService.NavigateTo<TestViewModel>(parameter);

        // Assert
        var viewModel = navigationService.CurrentViewModel as TestViewModel;
        viewModel.Should().NotBeNull();
        viewModel!.ReceivedParameter.Should().Be(parameter);
    }

    [Fact]
    public void NavigateTo_PushesPreviousViewModel_ToStack()
    {
        // Arrange
        var navigationService = new NavigationService(_serviceProvider);

        // Act - Navigate to first view
        navigationService.NavigateTo<TestViewModel>();
        var firstViewModel = navigationService.CurrentViewModel;

        // Navigate to second view
        navigationService.NavigateTo<TestViewModel2>();

        // Assert
        navigationService.CurrentViewModel.Should().BeOfType<TestViewModel2>();
        navigationService.CanGoBack.Should().BeTrue();
    }

    [Fact]
    public void GoBack_RestoresPreviousViewModel()
    {
        // Arrange
        var navigationService = new NavigationService(_serviceProvider);

        navigationService.NavigateTo<TestViewModel>();
        var firstViewModel = navigationService.CurrentViewModel;

        navigationService.NavigateTo<TestViewModel2>();

        // Act
        navigationService.GoBack();

        // Assert
        navigationService.CurrentViewModel.Should().BeSameAs(firstViewModel);
        navigationService.CanGoBack.Should().BeFalse();
    }

    [Fact]
    public void GoBack_CallsOnNavigatedBack()
    {
        // Arrange
        var navigationService = new NavigationService(_serviceProvider);

        navigationService.NavigateTo<TestViewModel>();
        var firstViewModel = navigationService.CurrentViewModel as TestViewModel;

        navigationService.NavigateTo<TestViewModel2>();

        // Act
        navigationService.GoBack();

        // Assert
        firstViewModel!.NavigatedBackCalled.Should().BeTrue();
    }

    [Fact]
    public void GoBack_DoesNothing_WhenNoHistory()
    {
        // Arrange
        var navigationService = new NavigationService(_serviceProvider);
        navigationService.NavigateTo<TestViewModel>();

        // Act
        navigationService.GoBack();

        // Assert
        navigationService.CurrentViewModel.Should().BeOfType<TestViewModel>();
        navigationService.CanGoBack.Should().BeFalse();
    }

    [Fact]
    public void ClearHistory_RemovesAllPreviousViewModels()
    {
        // Arrange
        var navigationService = new NavigationService(_serviceProvider);

        navigationService.NavigateTo<TestViewModel>();
        navigationService.NavigateTo<TestViewModel2>();
        navigationService.NavigateTo<TestViewModel>();

        // Act
        navigationService.ClearHistory();

        // Assert
        navigationService.CanGoBack.Should().BeFalse();
    }

    [Fact]
    public void CurrentViewModelChanged_Raised_OnNavigation()
    {
        // Arrange
        var navigationService = new NavigationService(_serviceProvider);

        ViewModelBase? changedViewModel = null;
        navigationService.CurrentViewModelChanged += (sender, vm) => changedViewModel = vm;

        // Act
        navigationService.NavigateTo<TestViewModel>();

        // Assert
        changedViewModel.Should().NotBeNull();
        changedViewModel.Should().BeOfType<TestViewModel>();
    }

    [Fact]
    public void MultipleNavigations_MaintainsCorrectStack()
    {
        // Arrange
        var navigationService = new NavigationService(_serviceProvider);

        // Act - Create navigation history
        navigationService.NavigateTo<TestViewModel>();        // 1st
        navigationService.NavigateTo<TestViewModel2>();       // 2nd
        navigationService.NavigateTo<TestViewModel>();        // 3rd
        navigationService.NavigateTo<TestViewModel2>();       // 4th (current)

        // Assert
        navigationService.CurrentViewModel.Should().BeOfType<TestViewModel2>();
        navigationService.CanGoBack.Should().BeTrue();

        // Go back through history
        navigationService.GoBack();
        navigationService.CurrentViewModel.Should().BeOfType<TestViewModel>();

        navigationService.GoBack();
        navigationService.CurrentViewModel.Should().BeOfType<TestViewModel2>();

        navigationService.GoBack();
        navigationService.CurrentViewModel.Should().BeOfType<TestViewModel>();

        navigationService.CanGoBack.Should().BeFalse();
    }

    // Test ViewModels

    private class TestViewModel : ViewModelBase, INavigableViewModel
    {
        public object? ReceivedParameter { get; private set; }
        public bool NavigatedBackCalled { get; private set; }

        public void OnNavigatedTo(object parameter)
        {
            ReceivedParameter = parameter;
        }

        public void OnNavigatedBack()
        {
            NavigatedBackCalled = true;
        }
    }

    private class TestViewModel2 : ViewModelBase, INavigableViewModel
    {
        public object? ReceivedParameter { get; private set; }

        public void OnNavigatedTo(object parameter)
        {
            ReceivedParameter = parameter;
        }

        public void OnNavigatedBack()
        {
        }
    }
}
