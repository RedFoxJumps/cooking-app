using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Cooking.DataAccess.Database;
using Cooking.DataAccess.Repository;
using Cooking.Desktop.DishArea;
using Cooking.Desktop.MenuArea;
using Cooking.Desktop.Shared;
using Cooking.Services;

namespace Cooking.Desktop;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var hostBuilder = new HostBuilder();
        hostBuilder.ConfigureServices(services =>
        {
            services.AddScoped<MainView>().AddScoped<IMainViewModel, MainViewModel>();
            services.AddScoped<DishEditorView>().AddScoped<IDishEditorViewModel, DishEditorViewModel>();
            services.AddScoped<NewMenuView>().AddScoped<INewMenuViewModel, NewMenuViewModel>();
            services.AddScoped<MenuHistoryView>().AddScoped<IMenuHistoryViewModel, MenuHistoryViewModel>();
            services.AddScoped<CatalogueView>().AddScoped<ICatalogueViewModel, CatalogueViewModel>();
            services.AddScoped<CatalogueView>().AddScoped<IDishViewModel, DishViewModel>();

            services.RegisterServices();
            services.AddScoped<IAsyncRunner, AsyncRunner>();
            services.AddScoped<INavigator, Navigator>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<CookingDatabase>();
            services.AddScoped<ICookingContext, CookingContext>();
        });

        var host = hostBuilder.Build();

        var view = host.Services.GetRequiredService<MainView>();
        view.DataContext = host.Services.GetRequiredService<IMainViewModel>();
        view.Show();
    }
}
