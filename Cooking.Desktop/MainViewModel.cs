using Cooking.Desktop.DishArea;
using Cooking.Desktop.MenuArea;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cooking.Desktop;

public interface IMainViewModel
{
    object CurrentView { get; }

    INewMenuViewModel NewMenuViewModel { get; }

    IMenuHistoryViewModel MenuHistoryViewModel { get; }

    ICatalogueViewModel CatalogueViewModel { get; }
}

internal partial class MainViewModel : ViewModelBase, IMainViewModel
{
    public MainViewModel(
        IMenuHistoryViewModel menuHistoryViewModel,
        INewMenuViewModel newMenuViewModel,
        ICatalogueViewModel catalogueViewModel)
    {
        NewMenuViewModel = newMenuViewModel;
        MenuHistoryViewModel = menuHistoryViewModel;
        CatalogueViewModel = catalogueViewModel;

        CurrentView = CatalogueViewModel;
    }

    [ObservableProperty]
    private object _currentView;

    [ObservableProperty]
    private INewMenuViewModel _newMenuViewModel;

    [ObservableProperty]
    private IMenuHistoryViewModel _menuHistoryViewModel;

    [ObservableProperty]
    private ICatalogueViewModel _catalogueViewModel;

    [RelayCommand]
    private void OpenCatalogue() => CurrentView = CatalogueViewModel;
}
