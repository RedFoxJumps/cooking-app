using Cooking.DataAccess.Database;
using Cooking.DataAccess.Models;
using Cooking.DataAccess.Repository;
using Cooking.Domain.Models;
using LinqToDB;

namespace Cooking.CookingApp.Retrievers;

public interface IDishesRetriever
{
    Task<List<Dish>> GetTaggedDishList();
}

public class DishesRetriever : IDishesRetriever
{
    private readonly ICookingContext cookingContext;

    public DishesRetriever(ICookingContext cc)
    {
        cookingContext = cc;
    }

    public async Task<List<Dish>> GetTaggedDishList()
    {
        await Db.CreateDefault();
        var links =
            from dish in cookingContext.Dishes
            join link in cookingContext.DishTagLinks on dish.Id equals link.DishId
            join tag in cookingContext.DishTags on link.TagId equals tag.Id
            select new
            {
                Name = dish.Name,
                TagEntity = tag,
            };

        var taggedDishes =
            from link in await links.ToArrayAsync()
            group link by link.Name into g
            select new Dish
            {
                Name = g.Key,
                Tags = g.Select(x => x.TagEntity.Tag).ToList(),
            };

        return taggedDishes.ToList();
    }
}
