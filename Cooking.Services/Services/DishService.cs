using Cooking.Contracts.Models;
using Cooking.Contracts.Services;
using Cooking.DataAccess.Database;
using Cooking.DataAccess.Models;
using Cooking.DataAccess.Repository;
using LinqToDB;
using LinqToDB.Data;

namespace Cooking.Services;

internal class DishesService : IDishesService
{
    private readonly ICookingContext _cookingContext;
    private readonly CookingDatabase _cookingDatabase;

    public DishesService(ICookingContext cookingContext, CookingDatabase cookingDatabase)
    {
        _cookingContext = cookingContext;
        _cookingDatabase = cookingDatabase;
    }

    public async Task<int> AddDish(Dish dish)
    {
        // add 
        var dishId = await _cookingContext.Dishes
            .Value(x => x.Name, dish.Name)
            .InsertWithInt32IdentityAsync();

        dish.Id = dishId;
        await LinkTags(dish);

        return dishId.Value;
    }

    public async Task LinkTags(Dish dish)
    {
        var newTags = await AddNewTags(dish.Tags);
        var requestedDishTags = await GetExistingTagsAsEntities(dish.Tags);

        var linkedTags = await _cookingContext.DishTagLinks
            .Where(x => x.DishId == dish.Id)
            .ToArrayAsync();

        var tagsToUnlink = linkedTags.ExceptBy(requestedDishTags.Select(x => x.Id), x => x.TagId).Select(x => x.Id).ToArray();
        var tagsToLink = requestedDishTags.ExceptBy(linkedTags.Select(x => x.TagId), x => x.Id)
            .Select(x => new DishTagLinkEntity { DishId = dish.Id.Value, TagId = x.Id, }).ToArray();

        await _cookingContext.DishTagLinks.Where(x => tagsToUnlink.Contains(x.Id)).DeleteAsync();
        await _cookingContext.DishTagLinks.BulkCopyAsync(tagsToLink);

    }

    public Task<DishTag> AddTag(string tag)
    {
        var entity = new DishTagEntity { Tag = tag };
        _cookingDatabase.InsertWithInt32IdentityAsync(entity);

        return Task.FromResult(new DishTag());
    }

    public async Task<IList<Dish>> GetTaggedDishList()
    {
        var linksQuery =
            from link in _cookingContext.DishTagLinks
            join dish in _cookingContext.Dishes on link.DishId equals dish.Id
            join tag in _cookingContext.DishTags on link.TagId equals tag.Id
            select new { dish, tag };

        var links = await linksQuery.ToArrayAsync();
        return links.GroupBy(x => x.dish.Id)
            .Select(x => new Dish
            {
                Name = x.First().dish.Name,
                Tags = x.Select(tag => tag.tag.Tag).ToArray(),
            }).ToArray();
    }

    public async Task<IList<DishTag>> GetTags()
    {
        return await _cookingContext.DishTags.Select(x => new DishTag(x.Tag))
            .ToArrayAsync();
    }

    public Task UnlinkTag(DishTag tag, Dish dish)
    {
        throw new NotImplementedException();
    }

    private async Task<IList<DishTagEntity>> GetExistingTagsAsEntities(IList<string> tags)
    {
        var tagsUpper = tags.Select(x => x.ToUpper()).ToArray();
        var existingTags = await _cookingContext.DishTags.ToArrayAsync();
        return existingTags.Where(x => tagsUpper.Contains(x.Tag.ToUpper()))
            .ToArray();
    }

    private async Task<IList<DishTagEntity>> AddNewTags(IList<string> tags)
    {
        var existingTags = (await GetExistingTagsAsEntities(tags)).Select(x => x.Tag.ToUpper());

        var newTagEntities = tags.Where(x => !existingTags.Contains(x.ToUpper()))
            .Select(x => new DishTagEntity { Tag = x }).ToArray();

        foreach (var entity in newTagEntities)
        {
            var newId = await _cookingContext.DishTags.Value(x => x.Tag, entity.Tag)
                .InsertWithInt32IdentityAsync();

            entity.Id = newId.Value;
        }

        return newTagEntities;
    }
}
