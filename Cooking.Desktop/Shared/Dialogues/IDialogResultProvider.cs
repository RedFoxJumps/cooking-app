namespace Cooking.Desktop.Shared;

public interface IDialogResultProvider<TResult>
    where TResult : new()
{
    void ShowDialogue(DialogueOptions<TResult> data);
}
