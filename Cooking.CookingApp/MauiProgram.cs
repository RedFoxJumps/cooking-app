using Cooking.CookingApp.Dishes;
using Cooking.CookingApp.Retrievers;
using Cooking.DataAccess.Database;
using Cooking.DataAccess.Repository;

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

        builder.Services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddSingleton<CookingDatabase>();
        builder.Services.AddSingleton<ICookingContext, CookingContext>();
        builder.Services.AddSingleton<IDishesRetriever, DishesRetriever>();

        builder.Services.AddSingleton<MainPage>();
        Routing.RegisterRoute(nameof(DishCatalogueView), typeof(DishCatalogueView));
        builder.Services.AddSingleton<DishCatalogueView>();
        builder.Services.AddSingleton<DishCatalogueViewModel>();

        return builder.Build();
    }
}