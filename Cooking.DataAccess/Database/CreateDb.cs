using Cooking.DataAccess.Models;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SQLite;
using System.IO.Abstractions;

namespace Cooking.DataAccess.Database;

public static class Db
{
    private static readonly IFileSystem _fileSystem = new FileSystem();

    public static async Task CreateDefault()
    {
        SQLiteTools.AlwaysCheckDbNull = true;

        if (_fileSystem.File.Exists(Constants.DatabaseFile))
        {
            return;
        }

        SQLiteTools.CreateDatabase(Constants.DatabaseName);
        var newDb = new CookingDatabase();

        var blinyId = await FillDishes(newDb);
        var tagIds = await FillTags(newDb);
        await FillDishTagLinks(newDb, blinyId, tagIds);

        await newDb.CreateTableAsync<MenuDishLinkEntity>(tableOptions: TableOptions.CreateIfNotExists);
        await newDb.CreateTableAsync<MenuEntity>(tableOptions: TableOptions.CreateIfNotExists);
    }

    private static async Task FillDishTagLinks(CookingDatabase db, int blinyId, int[] tagIds)
    {
        await db.CreateTableAsync<DishTagLinkEntity>(tableOptions: TableOptions.CreateIfNotExists);

        var links = tagIds.Select(x => new DishTagLinkEntity { DishId = blinyId, TagId = x, });
        await db.BulkCopyAsync(links);
    }

    private static async Task<int[]> FillTags(CookingDatabase db)
    {
        await db.CreateTableAsync<DishTagEntity>(tableOptions: TableOptions.CreateIfNotExists);

        var sniadanak   = new DishTagEntity { Tag = DishTag.Sniadanak, };
        var abied       = new DishTagEntity { Tag = DishTag.Abied, };
        var viacera     = new DishTagEntity { Tag = DishTag.Viacera, };
        var rest = new[]
        {
            new DishTagEntity { Tag = DishTag.Salodkae, },
            new DishTagEntity { Tag = DishTag.Vadkae, },
        };

        await db.BulkCopyAsync(rest);
        return new[]
        {
            await db.InsertWithInt32IdentityAsync(sniadanak),
            await db.InsertWithInt32IdentityAsync(abied),
            await db.InsertWithInt32IdentityAsync(viacera),
        };
    }

    private static async Task<int> FillDishes(CookingDatabase db)
    {
        await db.CreateTableAsync<DishEntity>(tableOptions: TableOptions.CreateIfNotExists);

        var dishes = new[]
        {
            new DishEntity { Name = "Суп греч", },
            new DishEntity { Name = "Суп ріс", },
            new DishEntity { Name = "Суп булён", },
            new DishEntity { Name = "Суп фасоль", },
            new DishEntity { Name = "Расольнік", },
            new DishEntity { Name = "Боршч", },
            new DishEntity { Name = "Халаднік", },

            new DishEntity { Name = "Сырнікі", },
            new DishEntity { Name = "Бліны", },
            // bliny,
            new DishEntity { Name = "Ладкі (барбуз)", },
            new DishEntity { Name = "Бліны з мачанкай", },

            new DishEntity { Name = "Макарон", },
            new DishEntity { Name = "Макарон з памідорам", },
            new DishEntity { Name = "Бабка", },
            new DishEntity { Name = "Картопля (зажарка)", },

            new DishEntity { Name = "Плоў", },
            new DishEntity { Name = "Рыс (агародніна)", },
            new DishEntity { Name = "Гречіха", },

            new DishEntity { Name = "Салаты", },
            new DishEntity { Name = "Галубцы", },
            new DishEntity { Name = "Перцы", },
        };

        await db.BulkCopyAsync(dishes);

        var bliny = new DishEntity { Name = "Бліны (з творагам, у духоўцы)", };
        return await db.InsertWithInt32IdentityAsync(bliny);
    }
}
