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

    Dish Dish { get; set; }

    ObservableCollection<ITagViewModel> AppliedTags { get; }

    ObservableCollection<ITagViewModel> ExistingTags { get; }
}

internal partial class DishEditorViewModel : ViewModelBase, IDishEditorViewModel
{
    private readonly IDishesService _dishesService;

    public DishEditorViewModel(IDishesService dishesService)
    {
        _dishesService = dishesService;
        OkCancelViewModel = new OkCancelViewModel
        {
            OkCommand = new AsyncRelayCommand(Ok, CanInvokeOk),
        };
    }

    #region mvvm

    protected Action<Dish>? CurrentOperationCallback { get; set; }

    public IOkCancelViewModel OkCancelViewModel { get; }

    public ObservableCollection<ITagViewModel> AppliedTags { get; private set; } = new();

    public ObservableCollection<ITagViewModel> ExistingTags { get; private set; } = new();

    public bool HasChanges { get; set; } = true;

    [ObservableProperty]
    private Dish _dish = new();

    protected bool CanInvokeOk() => true;
    protected async Task Ok()
    {
        var dishTags = AppliedTags.Where(x => x is not TagInputViewModel).Select(x => x.Tag).ToArray();
        Dish.Tags = dishTags;

        if (Dish.Id.HasValue)
        {
            await _dishesService.Update(Dish);
        }
        else
        {
            await _dishesService.AddDish(Dish);
        }

        CurrentOperationCallback?.Invoke(Dish);
    }

    #endregion

    public void Operate(Dish dish, OperationOptions<Dish, DishEditorParams> options)
    {
        Dish = dish;
        CurrentOperationCallback = options.OperationResultCallback;
        ExistingTags = new(options.Params.Tags.Select(x => GetMoveTagButton(x.Tag)));

        AppliedTags = new(dish.Tags.Select(tag => GetMoveTagButton(tag)));
        AppliedTags.Add(AddNewTagToListButton);
    }

    private void MoveTagAction(ITagViewModel? tag)
    {
        var targetList = ExistingTags.Contains(tag)
            ? AppliedTags
            : ExistingTags;

        AppliedTags.Remove(tag);
        ExistingTags.Remove(tag);

        targetList.Add(tag);
    }

    /// <summary>
    /// Create new tag, if it does not exist yet; apply it if it does exist; do nothing otherwise.
    /// </summary>
    private async Task ApplyTagToDishAction(ITagViewModel? tagViewModel)
    {
        var tag = tagViewModel?.Tag;
        if (string.IsNullOrWhiteSpace(tag))
        {
            return;
        }

        tagViewModel!.Tag = "";
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

    private async Task CreateNewTag(ITagViewModel tagViewModel)
    {
        var tag = tagViewModel.Tag;
        await _dishesService.AddTag(tag);
        AppliedTags.Add(GetMoveTagButton(tag));
    }

    private ITagViewModel GetMoveTagButton(string tag) => new TagButtonViewModel(MoveTagAction, tag);

    private ITagViewModel AddNewTagToListButton
        => new TagInputViewModel(new AsyncRelayCommand<ITagViewModel>(ApplyTagToDishAction));
}
