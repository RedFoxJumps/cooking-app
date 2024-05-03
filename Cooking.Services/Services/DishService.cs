using Cooking.Contracts.Services;
using Cooking.DataAccess.Repository;
using Cooking.Services.Models;

namespace Cooking.Services;

internal class DishesService : IDishesService
{
    private readonly ICookingContext _cookingContext;

    public DishesService(ICookingContext cookingContext)
    {
        _cookingContext = cookingContext;
    }
}
