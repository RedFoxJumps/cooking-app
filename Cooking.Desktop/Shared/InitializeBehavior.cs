using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace Cooking.Desktop.Shared;

internal interface IInitializable
{
    public Task Initialize();
}

public class InitializeBehavior : Behavior<ContentControl>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.Loaded += OnInitialize;
    }

    private async void OnInitialize(object? sender, EventArgs e)
    {
        if (AssociatedObject.DataContext is (null or not IInitializable))
        {
            return;
        }

        try
        {
            await (AssociatedObject.DataContext as IInitializable).Initialize();
        }
        catch(NullReferenceException ex)
        {
        }
        catch(AggregateException ex)
        {
        }
    }
}
