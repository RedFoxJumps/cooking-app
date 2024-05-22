using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Cooking.Contracts.Models;
using Cooking.Contracts.Services;
using Cooking.Desktop.Shared;
using Cooking.Desktop.Shared.Operations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RelayCommand = CommunityToolkit.Mvvm.Input.RelayCommand;

namespace Cooking.Desktop.DishArea;

public interface IDishEditorViewModel : IObjectOperator<Dish, DishEditorParams>
{
    IOkCancelViewModel OkCancelViewModel { get; }

    IAsyncRelayCommand<string> CreateNewTagCommand { get; }

    string DishName { get; set; }

    string DishDescription { get; set; }

    ObservableCollection<ITagViewModel> CurrentTags { get; }

    ObservableCollection<ITagViewModel> ExistingTags { get; }
}

internal partial class DishEditorViewModel : ViewModelBase, IDishEditorViewModel
{
    private readonly IDishesService _dishesService;
    private Lazy<string> AddTagButtonText { get; }

    public DishEditorViewModel(IDishesService dishesService)
    {
        _dishesService = dishesService;
        AddTagButtonText = new Lazy<string>(GetAddTagText);
        OkCancelViewModel = new OkCancelViewModel
        {
            OkCommand = new RelayCommand(Ok, CanInvokeOk),
        };
    }

    protected Action<Dish>? CurrentOperationCallback { get; set; }

    public IOkCancelViewModel OkCancelViewModel { get; }

    public ObservableCollection<ITagViewModel> CurrentTags { get; private set; } = new();

    public ObservableCollection<ITagViewModel> ExistingTags { get; private set; } = new();

    public bool HasChanges { get; set; } = true;

    [ObservableProperty]
    private string _dishName = string.Empty;

    [ObservableProperty]
    private string _dishDescription = string.Empty;

    private void MoveTagAction(ITagViewModel tag)
    {
        var targetList = CurrentTags.Contains(tag)
            ? ExistingTags
            : CurrentTags;

        CurrentTags.Remove(tag);
        ExistingTags.Remove(tag);

        targetList.Add(tag);
    }

    public void Operate(Dish dish, OperationOptions<Dish, DishEditorParams> options)
    {
        DishName = dish.Name;
        CurrentOperationCallback = options.OperationResultCallback;
        ExistingTags = new (options.Params.Tags.Select(x => GetMoveButton(x.Tag)));

        CurrentTags = new (dish.Tags.Select(tag => GetMoveButton(tag)));
        CurrentTags.Add(GetCreateButton());
    }

    [RelayCommand]
    private async Task CreateNewTag(string tag)
    {
        if (DoesTagExist(tag))
        {
            return;
        }

        var result = await _dishesService.AddTag(tag);
        CurrentTags.Add(GetMoveButton(tag));
    }

    protected Dish GetResult() => new ()
    {
        Name = DishName,
        Tags = CurrentTags.Where(x => x.Tag != AddTagButtonText.Value).Select(x => x.Tag).ToArray(),
    };

    protected void Ok() => CurrentOperationCallback?.Invoke(GetResult());

    protected bool CanInvokeOk() => true; // !string.IsNullOrEmpty(DishName);

    private bool DoesTagExist(string tag) => ExistingTags.Any(x => x.Tag == tag) && CurrentTags.Any(x => x.Tag == tag);

    private ITagViewModel GetMoveButton(string tag) => new TagButtonViewModel(MoveTagAction, tag);

    private string GetAddTagText()
    {
        var tag = App.Current.Resources["AddPlus"].ToString();
        return tag ?? "Add +";
    }

    private ITagViewModel GetCreateButton()
    {
        return new TagButtonViewModel(MoveTagAction, AddTagButtonText.Value);
    }
}
