namespace Cooking.CookingApp.Dishes;

public partial class DishCatalogueView : ContentPage
{
	public DishCatalogueView(DishCatalogueViewModel vm)
	{
		InitializeComponent();
		BindingContext= vm;
	}
}