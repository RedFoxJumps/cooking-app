using Cooking.Contracts.Models;

namespace Cooking.Contracts.Services;

public interface IMenuService
{
    Task<Menu> MakeMenu(IList<MenuCreationOptions> menuOptions, DateOnly? excludeDishesPastDate = null, string? note = null);

    Task<IReadOnlyCollection<Menu>> GetMenuHistory(TimeSpan pastPeriod);

    Task<IReadOnlyCollection<Menu>> GetMenuHistory(DateOnly from, DateOnly to);

    Task<Menu> RepeatMenu(DateTime fromDate);
}
