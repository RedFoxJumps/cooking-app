using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Cooking.Desktop.Shared;

public interface IOkCancelViewModel
{
    IRelayCommand OkCommand { get; }

    IRelayCommand CancelCommand { get; }
}

internal struct OkCancelViewModel : IOkCancelViewModel
{
    public IRelayCommand OkCommand { get; set; }

    public IRelayCommand CancelCommand { get; set; }
}
