using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Cooking.Contracts.Models;
using Cooking.Contracts.Services;
using Cooking.Desktop.Shared;

namespace Cooking.Desktop.DishArea;

public interface ICatalogueViewModel
{
    IAsyncRelayCommand ShowNewDishViewCommand { get; }

    IAsyncRelayCommand ShowDishEditViewCommand { get; }

    ObservableCollection<Dish> Dishes { get; }

    Dish? SelectedDish { get; }
}

internal partial class CatalogueViewModel : ViewModelBase, ICatalogueViewModel, IInitializable
{
    private readonly INavigator _navigator;
    private readonly IMenuService _menuService;
    private readonly IDishesService _dishesService;

    public CatalogueViewModel(INavigator navigator, IMenuService menuService, IDishesService dishesService)
	{
        _navigator = navigator;
        _menuService = menuService;
        _dishesService = dishesService;
    }

    public ObservableCollection<Dish> Dishes { get; set; } = new();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ShowDishEditViewCommand))]
    private Dish _selectedDish;

    [RelayCommand]
    private async Task ShowNewDishView()
    {
        var tags = await FetchTags();
        _navigator.OpenDishEditor(initialize: vm =>
            vm.Operate(new Dish(), operationOptions: new (OnCreate, new DishEditorParams(tags))));
    }

    private async Task<IList<DishTag>> FetchTags() => await _dishesService.GetTags();

    private void OnCreate(Dish newDish)
    {
        Dishes.Add(newDish);
        _navigator.CloseDialog();
    }

    [RelayCommand(CanExecute = nameof(CanShowDishEditView))]
    private async Task ShowDishEditView()
    {
        var tags = await FetchTags();
        _navigator.OpenDishEditor(initialize: vm =>
        {
            var selectableTags = tags.ExceptBy(SelectedDish.Tags, x => x.Tag).ToArray();
            vm.Operate(SelectedDish, operationOptions: new(OnEdit, new DishEditorParams(selectableTags)));
        });
    }

    private bool CanShowDishEditView() => SelectedDish is not null;

    private void OnEdit(Dish editedDish)
    {
        OnPropertyChanged(nameof(Dishes));
    }

    public async Task Initialize()
    {
        var dishes = await _dishesService.GetTaggedDishList();
        Dishes = new ObservableCollection<Dish>(dishes);
        OnPropertyChanged(nameof(Dishes));
    }
}
