using Cooking.CookingApp.Dishes;
using Cooking.CookingApp.Services;
using Cooking.DataAccess.Database;
using Cooking.DataAccess.Repository;
//using Material.Components.Maui.Extensions;

namespace Cooking.CookingApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        //builder.UseMaterialComponents(new List<string>
        //    {
        //        //generally, we needs add 6 types of font families
        //        "OpenSans-Regular.ttf",
        //        "OpenSans-Regular.ttf",
        //        "OpenSans-Regular.ttf",
        //        "OpenSans-Regular.ttf",
        //        "OpenSans-Regular.ttf",
        //        "OpenSans-Regular.ttf",
        //    });

        builder.Services.AddSingleton<MainPage>();
        Register(() =>
        {
            builder.Services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddSingleton<CookingDatabase>();
            builder.Services.AddSingleton<ICookingContext, CookingContext>();
        });

        Register(() =>
        {
            builder.Services.AddSingleton<ITagService, TagService>();
        });

        Register(() =>
        {
            builder.Services.AddSingleton<IDishesService, DishesService>();

            builder.Services.AddSingleton<DishCatalogueView>();
            builder.Services.AddSingleton<DishCatalogueViewModel>();
            Routing.RegisterRoute(nameof(DishCatalogueView), typeof(DishCatalogueView));

            builder.Services.AddSingleton<AddDishView>();
            builder.Services.AddSingleton<AddDishViewModel>();
            Routing.RegisterRoute(nameof(AddDishView), typeof(AddDishView));
        });

        return builder.Build();
    }

    private static void Register(Action reg) => reg.Invoke();
}