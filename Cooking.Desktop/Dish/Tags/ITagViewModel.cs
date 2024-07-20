using CommunityToolkit.Mvvm.Input;

namespace Cooking.Desktop.DishArea;

public interface ITagViewModel
{
    IRelayCommand<ITagViewModel> OnClickCommand { get; }

    string Tag { get; }
}
