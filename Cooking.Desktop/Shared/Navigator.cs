using System;
using Cooking.Desktop.DishArea;

namespace Cooking.Desktop.Shared;

internal interface INavigator
{
    object CurrentViewModel { get; }

    void OpenDishEditor(Action<IDishEditorViewModel> initialize);
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

    public object CurrentViewModel { get; protected set; }

    public void OpenDishEditor(Action<IDishEditorViewModel> initialize)
    {
        initialize?.Invoke(_dishEditorViewModel);
        var view = new DishEditorView();
        view.DataContext = _dishEditorViewModel;
        view.ShowDialog();
    }
}
