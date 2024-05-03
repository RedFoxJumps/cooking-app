using Cooking.Contracts.Models;

namespace Cooking.Contracts.Services;

public interface IDishesService
{
    Task<int> AddDish(Dish dish);

    Task<IList<Dish>> GetTaggedDishList();

    Task<IList<DishTag>> GetTags();

    Task<DishTag> AddTag(string tag);

    Task LinkTag(DishTag tag, Dish dish);

    Task UnlinkTag(DishTag tag, Dish dish);
}
