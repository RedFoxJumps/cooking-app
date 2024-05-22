using CommunityToolkit.Mvvm.Input;

namespace Cooking.Desktop.DishArea;

public interface IDishViewModel
{
    IRelayCommand AddTagCommand { get; }

    IRelayCommand RemoveTagCommand { get; }
}

internal partial class DishViewModel : ViewModelBase, IDishViewModel
{
    [RelayCommand]
    private void AddTag() { }

    [RelayCommand]
    private void RemoveTag() { }
}
