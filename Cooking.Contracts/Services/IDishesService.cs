using Cooking.Contracts.Models;

namespace Cooking.Contracts.Services;

public interface IDishesService
{
    Task<int> AddDish(Dish dish);

    Task Update(Dish dish);

    Task<IList<Dish>> GetTaggedDishList();

    Task<IList<DishTag>> GetTags();

    Task<DishTag> AddTag(string tag);

    Task LinkTags(Dish dish);

    Task UnlinkTag(DishTag tag, Dish dish);
}
