namespace Cooking.CookingApp.Extensions
{
    public static class TaskExtensions
    {
        public static Task FireAndForget<T>(this Task<T> task, Action<Task<T>> successCallback = null, Action<Task<T>> errorCallback = null)
        {
            task.ContinueWith(successCallback.DefaultIfNull(), TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith(errorCallback.DefaultIfNull(), TaskContinuationOptions.OnlyOnFaulted);

            return task;
        }

        private static Action<Task<T>> DefaultIfNull<T>(this Action<Task<T>> action) =>  action ?? delegate { };
    }
}
