using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cooking.Desktop.DishArea;

public interface IDishViewModel
{
    ICommand AddTagCommand { get; }

    ICommand RemoveTagCommand { get; }
}

internal class DishViewModel : ViewModelBase, IDishViewModel
{
}
