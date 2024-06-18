namespace Cooking.CookingApp.Dishes;

public partial class AddDishView : ContentPage
{
	public AddDishView(AddDishViewModel vm)
	{
		InitializeComponent();
		BindingContext= vm;
	}
}