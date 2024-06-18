using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Cooking.CookingApp.Services;
using Cooking.Domain.Models;

namespace Cooking.CookingApp.Dishes;

public partial class DishCatalogueViewModel : ObservableObject
{
    private readonly IDishesService dishesRetriever;

    [ObservableProperty]
    private List<Dish> dishes;

    public DishCatalogueViewModel(IDishesService dishesRetriever)
    {
        this.dishesRetriever = dishesRetriever;
    }

    [RelayCommand]
    public async Task RefreshDishList()
    {
        Dishes = await dishesRetriever.GetTaggedDishList();
    }
}
