using Cooking.Desktop.DishArea;
using Cooking.Desktop.MenuArea;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cooking.Desktop;

public interface IMainViewModel
{
    IDishEditorViewModel NewDishViewModel { get; }

    INewMenuViewModel NewMenuViewModel { get; }

    IMenuHistoryViewModel MenuHistoryViewModel { get; }

    ICatalogueViewModel CatalogueViewModel { get; }
}

internal partial class MainViewModel : ViewModelBase, IMainViewModel
{
    public MainViewModel(
        IDishEditorViewModel newDishViewModel,
        IMenuHistoryViewModel menuHistoryViewModel,
        INewMenuViewModel newMenuViewModel,
        ICatalogueViewModel catalogueViewModel)
    {
        NewDishViewModel = newDishViewModel;
        NewMenuViewModel = newMenuViewModel;
        MenuHistoryViewModel = menuHistoryViewModel;
        CatalogueViewModel = catalogueViewModel;
    }

    [ObservableProperty]
    private IDishEditorViewModel _newDishViewModel;

    [ObservableProperty]
    private INewMenuViewModel _newMenuViewModel;

    [ObservableProperty]
    private IMenuHistoryViewModel _menuHistoryViewModel;

    [ObservableProperty]
    private ICatalogueViewModel _catalogueViewModel;
}
