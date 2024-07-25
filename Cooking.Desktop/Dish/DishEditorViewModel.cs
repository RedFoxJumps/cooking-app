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

    string DishName { get; set; }

    string DishDescription { get; set; }

    ObservableCollection<ITagViewModel> AppliedTags { get; }

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

    #region mvvm

    protected Action<Dish>? CurrentOperationCallback { get; set; }

    public IOkCancelViewModel OkCancelViewModel { get; }

    public ObservableCollection<ITagViewModel> AppliedTags { get; private set; } = new();

    public ObservableCollection<ITagViewModel> ExistingTags { get; private set; } = new();

    public bool HasChanges { get; set; } = true;

    [ObservableProperty]
    private string _dishName = string.Empty;

    [ObservableProperty]
    private string _dishDescription = string.Empty;

    protected void Ok() => CurrentOperationCallback?.Invoke(GetResultDish());

    protected bool CanInvokeOk() => true; // !string.IsNullOrEmpty(DishName);

    #endregion

    private void MoveTagAction(ITagViewModel tag)
    {
        var targetList = ExistingTags.Contains(tag)
            ? AppliedTags
            : ExistingTags;

        AppliedTags.Remove(tag);
        ExistingTags.Remove(tag);

        targetList.Add(tag);
    }

    /// <summary>
    /// Create new tag, if it does not exist yet; apply it if it does; do nothing otherwise.
    /// </summary>
    private async Task ApplyTagToDishAction(ITagViewModel tagViewModel)
    {
        var tag = tagViewModel.Tag;
        if (string.IsNullOrWhiteSpace(tag))
        {
            return;
        }

        tagViewModel.Tag = "";
        var existingTag = ExistingTags.Concat(AppliedTags).FirstOrDefault(x => x.Tag == tag);
        if (existingTag is null)
        {
            AppliedTags.Add(GetMoveTagButton(tag));
            await CreateNewTag(tagViewModel);
            return;
        }

        if (ExistingTags.Contains(existingTag))
        {
            MoveTagAction(existingTag);
        }
    }

    public void Operate(Dish dish, OperationOptions<Dish, DishEditorParams> options)
    {
        DishName = dish.Name;
        DishDescription = dish.Description;
        CurrentOperationCallback = options.OperationResultCallback;
        ExistingTags = new(options.Params.Tags.Select(x => GetMoveTagButton(x.Tag)));

        AppliedTags = new(dish.Tags.Select(tag => GetMoveTagButton(tag)));
        AppliedTags.Add(AddNewTagToListButton);
    }

    private async Task CreateNewTag(ITagViewModel tagViewModel)
    {
        var tag = tagViewModel.Tag;
        if (DoesTagExist(tag))
        {
            return;
        }

        await _dishesService.AddTag(tag);
        AppliedTags.Add(GetMoveTagButton(tag));
    }

    protected Dish GetResultDish() => new ()
    {
        Name = DishName,
        Tags = AppliedTags.Where(x => x is not TagInputViewModel).Select(x => x.Tag).ToArray(),
    };

    private bool DoesTagExist(string tag) => ExistingTags.Concat(AppliedTags).Any(x => x.Tag == tag);

    private ITagViewModel GetMoveTagButton(string tag) => new TagButtonViewModel(MoveTagAction, tag);

    private ITagViewModel AddNewTagToListButton
        => new TagInputViewModel(new AsyncRelayCommand<ITagViewModel>(ApplyTagToDishAction));

    private string GetAddTagText()
    {
        var tag = App.Current.Resources["AddPlus"].ToString();
        return tag ?? "Add +";
    }
}
