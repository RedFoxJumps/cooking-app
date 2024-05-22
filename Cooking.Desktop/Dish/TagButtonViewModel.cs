using System;
using CommunityToolkit.Mvvm.Input;

namespace Cooking.Desktop.DishArea;

public interface ITagViewModel
{
    IRelayCommand<ITagViewModel> OnClickCommand { get; }

    string Tag { get; }
}

internal partial class TagButtonViewModel : ITagViewModel
{
    public TagButtonViewModel(Action<ITagViewModel?> onClickAction, string tag)
    {
        Tag = tag;
        OnClickCommand = new RelayCommand<ITagViewModel>(onClickAction);
    }

    public string Tag { get; }

    public IRelayCommand<ITagViewModel> OnClickCommand { get; }
}
