using Cooking.Contracts.Models;
using Cooking.Contracts.Services;
using Cooking.DataAccess.Repository;

namespace Cooking.Services;

internal class DishesService : IDishesService
{
    private readonly ICookingContext _cookingContext;

    public DishesService(ICookingContext cookingContext)
    {
        _cookingContext = cookingContext;
    }

    public Task<int> AddDish(Dish dish)
    {
        throw new NotImplementedException();
    }

    public Task<DishTag> AddTag(string tag)
    {
        throw new NotImplementedException();
    }

    public Task<IList<Dish>> GetTaggedDishList()
    {
        throw new NotImplementedException();
    }

    public Task<IList<DishTag>> GetTags()
    {
        var res = new []
        {
            new DishTag { Tag = "nig", },
            new DishTag { Tag = "biatch", },
        };
        return Task.FromResult((IList<DishTag>)res);
    }

    public Task LinkTag(DishTag tag, Dish dish)
    {
        throw new NotImplementedException();
    }

    public Task UnlinkTag(DishTag tag, Dish dish)
    {
        throw new NotImplementedException();
    }
}
