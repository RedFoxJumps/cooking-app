using Cooking.DataAccess.Database;
using Cooking.DataAccess.Models;
using Cooking.DataAccess.Repository;
using Cooking.Domain.Models;
using LinqToDB;

namespace Cooking.CookingApp.Services;

public interface ITagService
{
    Task<DishTag[]> GetAllTags();

    Task AddTagsForExistingDish(int dishId, DishTag[] tags);

    Task RemoveTagsFromExistingDish(int dishId, DishTag[] tags);

    Task AddTags(DishTag[] tags); 
}

public class TagService : ITagService
{
    private readonly CookingDatabase _cookingDatabase;
    private readonly ICookingContext _cookingContext;
    private readonly IRepository<DishTagEntity> _tagRepository;

    public TagService(
        CookingDatabase cookingDatabase,
        ICookingContext cookingContext,
        IRepository<DishTagEntity> tagRepository)
    {
        _cookingDatabase = cookingDatabase;
        _cookingContext = cookingContext;
        _tagRepository = tagRepository;
    }

    public async Task<DishTag[]> GetAllTags()
    {
        return (await _cookingContext.DishTags.ToArrayAsync())
            .Select(tag => new DishTag
            {
                Id = tag.Id,
                Description = tag.Description,
            })
            .ToArray();
    }

    public async Task AddTagsForExistingDish(int dishId, DishTag[] tags)
    {
        var links = tags.Select(x => new DishTagLinkEntity
        {
            DishId = dishId,
            TagId = x.Id.Value,
        });

        var insertions = links.Select(x => _cookingDatabase.InsertOrReplaceAsync(x));
        await Task.WhenAll(insertions);
    }

    public async Task AddTags(DishTag[] tags)
    {
        var tagEntities = tags
            .Select(x => new DishTagEntity
            {
                Id = x.Id ?? default,
                Tag = x.Tag,
                Description = x.Description,
            }).ToArray();

        await _tagRepository.Insert(tagEntities);
    }

    public async Task RemoveTagsFromExistingDish(int dishId, DishTag[] tags)
    {
        var links = tags.Select(x => new DishTagLinkEntity
        {
            DishId = dishId,
            TagId = x.Id.Value,
        });

        var deletions = links.Select(x => _cookingContext.DishTagLinks.DeleteAsync(dbLink => SameLink(x, dbLink)));
        await Task.WhenAll(deletions);
    }

    private static bool SameLink(DishTagLinkEntity link1, DishTagLinkEntity link2)
    {
        return link1.TagId == link2.TagId
            && link1.DishId == link2.DishId;
    }
}
