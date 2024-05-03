using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Cooking.Contracts.Models;
using Cooking.Contracts.Services;
using Cooking.Desktop.Shared;
using Cooking.Desktop.Shared.Operations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RelayCommand = CommunityToolkit.Mvvm.Input.RelayCommand;

namespace Cooking.Desktop.DishArea;

public interface IDishEditorViewModel : IObjectOperator<Dish>
{
    IOkCancelViewModel OkCancelViewModel { get; }

    IAsyncRelayCommand<string> CreateNewTagCommand { get; }

    string DishName { get; set; }

    ObservableCollection<string> CurrentTags { get; }

    ObservableCollection<DishTag> ExistingTags { get; }
}

internal partial class DishEditorViewModel : ViewModelBase, IDishEditorViewModel
{
    private readonly IDishesService _dishesService;

    public DishEditorViewModel(IDishesService dishesService, IReadOnlyCollection<DishTag> tags)
    {
        _dishesService = dishesService;
        ExistingTags = new(tags);
        OkCancelViewModel = new OkCancelViewModel
        {
            OkCommand = new RelayCommand(Ok, CanInvokeOk),
        };
    }

    protected Action<Dish>? CurrentOperationCallback { get; set; }

    public IOkCancelViewModel OkCancelViewModel { get; }

    public ObservableCollection<string> CurrentTags { get; private set; } = new();

    public ObservableCollection<DishTag> ExistingTags { get; private set; } = new();

    public bool HasChanges { get; set; }

    [ObservableProperty]
    private string _dishName = string.Empty;

    public void Operate(Dish dish, OperationOptions<Dish> options)
    {
        CurrentTags = new(dish.Tags);
        DishName = dish.Name;
        CurrentOperationCallback = options.OperationResultCallback;
    }

    [RelayCommand]
    private async Task CreateNewTag(string tag)
    {
        var result = await _dishesService.AddTag(tag);
        CurrentTags.Add(tag);
        ExistingTags.Add(result);
    }

    protected Dish GetResult() => new Dish { Name = DishName, Tags = CurrentTags.ToArray(), };

    protected void Ok() => CurrentOperationCallback?.Invoke(GetResult());
    protected bool CanInvokeOk() => !string.IsNullOrEmpty(DishName);
}
