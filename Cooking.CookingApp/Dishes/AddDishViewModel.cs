using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Cooking.CookingApp.Extensions;
using Cooking.CookingApp.Services;
using Cooking.Domain.Models;
using System.Collections.ObjectModel;

namespace Cooking.CookingApp.Dishes;

public partial class AddDishViewModel : ObservableObject
{
    private readonly IDishesService _dishesService;
    private readonly ITagService _tagService;

    [ObservableProperty]
    private Dish dish = new();

    [ObservableProperty]
    private DishTag tag = new();

    [ObservableProperty]
    private ObservableCollection<DishTag> allTags = new();

    [ObservableProperty]
    private ObservableCollection<DishTag> dishTags = new();

    public AddDishViewModel(
        IDishesService dishesService,
        ITagService tagService)
    {
        _dishesService = dishesService;
        _tagService = tagService;
        _tagService
            .GetAllTags()
            .FireAndForget(successCallback: t => AllTags = new ObservableCollection<DishTag>(t.Result));
    }

    [RelayCommand]
    public async Task SaveDish()
    {
        Dish.Tags = DishTags.ToList();
        await _dishesService.Add(Dish);
        Dish = new();
    }

    [RelayCommand]
    public Task NewDish()
    {
        return Task.CompletedTask;
    }
}
