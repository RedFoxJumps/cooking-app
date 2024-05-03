using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Cooking.Desktop;

internal partial class ViewModelBaseImpl : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged = delegate { };

    protected void Set<T>(ref T prop, T value, [CallerMemberName] string callerName = "")
    {
        if (EqualityComparer<T>.Default.Equals(prop, value))
        {
            return;
        }

        prop = value;
        OnProp(callerName);
    }

    private void OnProp([CallerMemberName] string prop = "")
    {
        PropertyChanged!.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
