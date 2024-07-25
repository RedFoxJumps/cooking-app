using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cooking.Desktop.DishArea;

internal partial class TagInputViewModel : ViewModelBase, ITagViewModel
{
    public TagInputViewModel(IRelayCommand<ITagViewModel> clickCommand)
    {
        Tag = "";
        OnClickCommand = clickCommand;
    }

    [ObservableProperty]
    private string _tag;

    public IRelayCommand<ITagViewModel> OnClickCommand { get; }
}
