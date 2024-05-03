using System.Windows.Input;

namespace Cooking.Desktop.Shared;

public interface IOkCancelViewModel
{
    ICommand OkCommand { get; }

    ICommand CancelCommand { get; }
}

internal struct OkCancelViewModel : IOkCancelViewModel
{
    public ICommand OkCommand { get; set; }

    public ICommand CancelCommand { get; set; }
}
