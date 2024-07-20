using System;
using CommunityToolkit.Mvvm.Input;

namespace Cooking.Desktop.DishArea;

public class TagInputViewModel : ITagViewModel
{
    public TagInputViewModel(Action<ITagViewModel?> onClickAction, string tag)
    {
        Tag = tag;
        OnClickCommand = new RelayCommand<ITagViewModel>(onClickAction);
    }

    public string Tag { get; }

    public IRelayCommand<ITagViewModel> OnClickCommand { get; }
}
