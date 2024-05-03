using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Cooking.Contracts.Models;
using Cooking.Contracts.Services;
using Cooking.Desktop.Shared;
using Cooking.Desktop.Shared.Operations;

namespace Cooking.Desktop.DishArea;

public interface ICatalogueViewModel
{
    IRelayCommand ShowNewDishViewCommand { get; }

    IRelayCommand ShowDishEditorViewCommand { get; }

    ObservableCollection<Dish> Dishes { get; }
}

internal partial class CatalogueViewModel : ViewModelBase, ICatalogueViewModel
{
    private readonly INavigator _navigator;
    private readonly IMenuService _menuService;

    public CatalogueViewModel(INavigator navigator, IMenuService menuService)
	{
        _navigator = navigator;
        _menuService = menuService;
    }

    public ObservableCollection<Dish> Dishes { get; set; } = new();

    [ObservableProperty]
    private Dish _selectedDish;

    [RelayCommand]
    private void ShowNewDishView()
    {
        _navigator.ShowDishEditor(new Dish(), new OperationOptions<Dish>(OnCreate));
    }

    private void OnCreate(Dish newDish)
    {
    }

    [RelayCommand(CanExecute = nameof(CanShowDishEditorView))]
    private void ShowDishEditorView()
    {
        _navigator.ShowDishEditor(SelectedDish ?? new (), new OperationOptions<Dish>(OnEdit));
    }

    private bool CanShowDishEditorView() => SelectedDish is not null;

    private void OnEdit(Dish editedDish)
    {
    }
}
