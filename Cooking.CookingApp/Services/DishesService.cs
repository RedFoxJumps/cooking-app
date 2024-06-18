using Cooking.DataAccess.Database;
using Cooking.DataAccess.Models;
using Cooking.DataAccess.Repository;
using Cooking.Domain.Models;
using LinqToDB;

namespace Cooking.CookingApp.Services;

public interface IDishesService
{
    Task<int> Add(Dish dish);

    Task<List<Dish>> GetTaggedDishList();
}

public class DishesService : IDishesService
{
    private readonly ICookingContext cookingContext;
    private readonly IRepository<DishEntity> dishes;
    private readonly IRepository<DishTagEntity> dishTags;
    private readonly IRepository<DishTagLinkEntity> dishTagLinks;

    public DishesService(ICookingContext cc,
        IRepository<DishEntity> d,
        IRepository<DishTagEntity> dt,
        IRepository<DishTagLinkEntity> dtl)
    {
        cookingContext = cc;
        dishes = d;
        dishTags = dt;
        dishTagLinks = dtl;
    }

    // TODO: AddOrUpdate logic
    public async Task<int> Add(Dish dish)
    {
        var dishEntity = new DishEntity
        {
            Id = dish.Id ?? default,
            Name = dish.Name,
        };

        var dishId = await dishes.Insert(dishEntity);
        var tagInsertionTasks = dish.Tags
            .Select(x => dishTags.Insert(new DishTagEntity { Tag = x.Tag, Description = x.Description, }));

        var links = (await Task.WhenAll(tagInsertionTasks))
            .Select(x => new DishTagLinkEntity
            {
                DishId = dishId,
                TagId = x,
            })
            .ToArray();

        await dishTagLinks.Insert(links);
        return dishId;
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
                Tags = g.Select(x => new DishTag
                {
                    Tag = x.TagEntity.Tag,
                    Description = x.TagEntity.Description,
                }).ToList(),
            };

        return taggedDishes.ToList();
    }
}
