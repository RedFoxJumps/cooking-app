using System.Text;
using Cooking.Domain.Models;

using Cooking.Domain.Extensions;
using Cooking.Domain.Processors.Extensions.Exclusion;
using Cooking.Domain.Processors.Extensions.Menu;
using Cooking.Domain.Processors.Extensions.Filters;
using Cooking.Domain.Processors.Extensions.ResultSelection;

namespace Cooking.ConsoleApp;

internal class Program
{
    static Dish[] Dishes =
    {
        Dish.New("Суп греч", Tag.Viacera, Tag.Abied, Tag.Vadkae),
        Dish.New("Суп ріс", Tag.Viacera, Tag.Abied, Tag.Vadkae),
        Dish.New("Суп булён", Tag.Viacera, Tag.Abied, Tag.Vadkae),
        Dish.New("Суп фасоль", Tag.Viacera, Tag.Abied, Tag.Vadkae),
        Dish.New("Расольнік", Tag.Viacera, Tag.Abied, Tag.Vadkae),
        Dish.New("Боршч", Tag.Viacera, Tag.Abied, Tag.Vadkae),
        Dish.New("Халаднік", Tag.Viacera, Tag.Abied, Tag.Vadkae),

        Dish.New("Сырнікі", Tag.Sniadanak, Tag.Salodkae),
        Dish.New("Бліны", Tag.Sniadanak, Tag.Viacera),
        Dish.New("Бліны (з творагам, у духоўцы)", Tag.Sniadanak, Tag.Viacera, Tag.Salodkae),
        Dish.New("Ладкі (барбуз)", Tag.Sniadanak, Tag.Viacera, Tag.Abied),
        Dish.New("Бліны з мачанкай", Tag.Sniadanak, Tag.Viacera, Tag.Abied),

        Dish.New("Макарон", Tag.Viacera, Tag.Abied, Tag.Sniadanak),
        Dish.New("Макарон з памідорам", Tag.Viacera, Tag.Abied, Tag.Sniadanak),
        Dish.New("Бабка", Tag.Viacera, Tag.Abied, Tag.Sniadanak),
        Dish.New("Картопля", Tag.Viacera, Tag.Abied, Tag.Sniadanak),

        Dish.New("Плоў", Tag.Viacera, Tag.Abied),
        Dish.New("Рыс", Tag.Viacera, Tag.Abied),
        Dish.New("Гречіха", Tag.Sniadanak, Tag.Viacera, Tag.Abied),

        Dish.New("Галубцы", Tag.Sniadanak, Tag.Viacera, Tag.Abied),
        Dish.New("Перцы", Tag.Sniadanak, Tag.Viacera, Tag.Abied),
    };

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        //int[] a = { 1, 2, 3, };
        //a.Exe().Exe().Exe();

        // extensions api example
        /// var menu =
        ///     FoodList.BeginMenu();
        ///     
        ///     menu
        ///     .ExcludeRecent(TagsTalbe.Home)
        ///     .AddToMenu(Type.Inc, Tag.Sniadanak).Select(ResultSelection.Single)
        ///     .AddFilter(Type.For, Tag.Abied, Tag.Viachera).Select(ResultSelection.Multiple, Instances.Two)
        ///     .AddFilter(Type.Not, Tag.Vadkae)
        ///     
        /// menu.Process();

        var extensionsMenu = Dishes.InitializeMenu();
        extensionsMenu
            .ExcludeRecent(Enumerable.Empty<Dish>())
            .AddToMenu(FilterType.OfType, Tag.Sniadanak).ButOf(Tag.Salodkae).Select(ResultSelector.Single)
            .AddToMenu(FilterType.Not, Tag.Sniadanak, Tag.Abied).Select(ResultSelector.All)
            ;

        var extensiveGeneration = extensionsMenu.MakeMenu();
        Print(extensiveGeneration, "Extensions");

        var ensuranceOne = Dishes
            .Where(x => x.Tags.Contains(Tag.Sniadanak))
            .Where(x => !x.Tags.Contains(Tag.Salodkae))
            .First();

        var ensuranceAll = Dishes
            .Where(x => !x.Tags.Contains(Tag.Sniadanak))
            .Where(x => !x.Tags.Contains(Tag.Abied))
            .Append(ensuranceOne);

        Print(ensuranceAll, "Ensurance");

        return;

        var simpleGeneration = GenerateSimple(4);
        Print(simpleGeneration);
        Console.WriteLine();
    }

    static Dish[] GenerateSimple(int v)
    {
        return new[]
        {
            Dishes.Random(Tag.Sniadanak),
            Dishes.Random(Tag.Abied),
            Dishes.Random(Tag.Viacera),
        }
        .Shuffle()
        .Take(v)
        .ToArray();
    }

    static void Print(IEnumerable<Dish> dishes, string colelctionName = "")
    {
        var st = string.Join("\n", dishes);
        if (!string.IsNullOrEmpty(colelctionName))
        {
            Console.WriteLine(colelctionName);
        }

        Console.WriteLine(st);
        Console.WriteLine();
    }
}