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
        AssociatedObject.Loaded += OnInitialize;

        base.OnAttached();
    }

    private async void OnInitialize(object? sender, EventArgs e)
    {
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
