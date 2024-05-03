using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cooking.Desktop.Shared;

public interface IAsyncRunner
{
    void RunAsync(Action action, CancellationToken cancellationToken = default);

    void RunAsync(Task task, CancellationToken cancellationToken = default);

    TResult RunAsync<TResult>(Task<TResult> task, CancellationToken cancellationToken = default);
}

internal class AsyncRunner : IAsyncRunner
{
    public void RunAsync(Action action, CancellationToken cancellationToken = default)
    {
        var task = new Task(action);
        RunAsync(task);
    }

    public async void RunAsync(Task task, CancellationToken cancellationToken = default)
    {
        await task.ContinueWith(x => { }, TaskContinuationOptions.OnlyOnFaulted);
        task.Start();
    }

    public TResult RunAsync<TResult>(Task<TResult> task, CancellationToken cancellationToken = default)
    {
        task.ContinueWith(x => { }, TaskContinuationOptions.OnlyOnFaulted);
        task.Start();
        return task.Result;
    }
}
