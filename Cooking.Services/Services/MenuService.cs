using Cooking.Contracts.Models;
using Cooking.Contracts.Services;
using Cooking.DataAccess.Repository;

namespace Cooking.Services;

internal class MenuService : IMenuService
{
    private readonly ICookingContext _cookingContext;

    public MenuService(ICookingContext cookingContext)
	{
        _cookingContext = cookingContext;
    }

    public Task<IReadOnlyCollection<Menu>> GetMenuHistory(TimeSpan pastPeriod)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Menu>> GetMenuHistory(DateOnly from, DateOnly to)
    {
        throw new NotImplementedException();
    }

    public Task<Menu> MakeMenu(IList<MenuCreationOptions> menuOptions, DateOnly? excludeDishesPastDate = null, string? note = null)
    {
        throw new NotImplementedException();
    }

    public Task<Menu> RepeatMenu(DateTime fromDate)
    {
        throw new NotImplementedException();
    }
}
