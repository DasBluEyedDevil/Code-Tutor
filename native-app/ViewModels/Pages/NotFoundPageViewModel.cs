using System.Reactive;
using ReactiveUI;
using CodeTutor.Native.Services;

namespace CodeTutor.Native.ViewModels.Pages;

/// <summary>
/// View model for the 404 not found page
/// </summary>
public class NotFoundPageViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    public NotFoundPageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        GoHomeCommand = ReactiveCommand.Create(GoHome);
        GoBackCommand = ReactiveCommand.Create(() => _navigationService.GoBack());
    }

    public ReactiveCommand<Unit, Unit> GoHomeCommand { get; }
    public ReactiveCommand<Unit, Unit> GoBackCommand { get; }

    private void GoHome()
    {
        _navigationService.NavigateTo<LandingPageViewModel>();
    }
}
