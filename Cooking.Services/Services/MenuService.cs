using Cooking.Contracts.Services;
using Cooking.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.Services;

internal class MenuService : IMenuService
{
    private readonly ICookingContext _cookingContext;

    public MenuService(ICookingContext cookingContext)
	{
        _cookingContext = cookingContext;
    }
}
