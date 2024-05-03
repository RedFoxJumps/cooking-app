using Cooking.Contracts.Models;
using Cooking.Desktop.DishArea;
using Cooking.Desktop.Shared.Operations;

namespace Cooking.Desktop.Shared;

public interface INavigator
{
    void ShowDishEditor(Dish dish, OperationOptions<Dish> options);
}

internal class Navigator : INavigator
{
    private readonly DishEditorView _dishEditorView;
    private readonly IDishEditorViewModel _dishEditorViewModel;

    public Navigator(DishEditorView dishEditorView, IDishEditorViewModel dishEditorViewModel)
	{
        _dishEditorView = dishEditorView;
        _dishEditorViewModel = dishEditorViewModel;
    }

    public void ShowDishEditor(Dish dish, OperationOptions<Dish> options)
    {
        _dishEditorViewModel.Operate(dish, options);
        _dishEditorView.DataContext = _dishEditorViewModel;
        _dishEditorView.ShowDialog();
    }
}
