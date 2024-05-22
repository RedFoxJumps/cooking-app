using Cooking.Contracts.Models;
using System.Collections.Generic;

namespace Cooking.Desktop.DishArea;

public record DishEditorParams(IList<DishTag> Tags);
