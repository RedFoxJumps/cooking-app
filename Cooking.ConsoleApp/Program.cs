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
        Dish.New("Суп греч", Tags.Viacera, Tags.Abied, Tags.Vadkae),
        Dish.New("Суп ріс", Tags.Viacera, Tags.Abied, Tags.Vadkae),
        Dish.New("Суп булён", Tags.Viacera, Tags.Abied, Tags.Vadkae),
        Dish.New("Суп фасоль", Tags.Viacera, Tags.Abied, Tags.Vadkae),
        Dish.New("Расольнік", Tags.Viacera, Tags.Abied, Tags.Vadkae),
        Dish.New("Боршч", Tags.Viacera, Tags.Abied, Tags.Vadkae),
        Dish.New("Халаднік", Tags.Viacera, Tags.Abied, Tags.Vadkae),

        Dish.New("Сырнікі", Tags.Sniadanak, Tags.Salodkae),
        Dish.New("Бліны", Tags.Sniadanak, Tags.Viacera),
        Dish.New("Бліны (з творагам, у духоўцы)", Tags.Sniadanak, Tags.Viacera, Tags.Salodkae),
        Dish.New("Ладкі (барбуз)", Tags.Sniadanak, Tags.Viacera, Tags.Abied),
        Dish.New("Бліны з мачанкай", Tags.Sniadanak, Tags.Viacera, Tags.Abied),

        Dish.New("Макарон", Tags.Viacera, Tags.Abied, Tags.Sniadanak),
        Dish.New("Макарон з памідорам", Tags.Viacera, Tags.Abied, Tags.Sniadanak),
        Dish.New("Бабка", Tags.Viacera, Tags.Abied, Tags.Sniadanak),
        Dish.New("Картопля", Tags.Viacera, Tags.Abied, Tags.Sniadanak),

        Dish.New("Плоў", Tags.Viacera, Tags.Abied),
        Dish.New("Рыс", Tags.Viacera, Tags.Abied),
        Dish.New("Гречіха", Tags.Sniadanak, Tags.Viacera, Tags.Abied),

        Dish.New("Галубцы", Tags.Sniadanak, Tags.Viacera, Tags.Abied),
        Dish.New("Перцы", Tags.Sniadanak, Tags.Viacera, Tags.Abied),
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
            .AddToMenu(FilterType.OfType, Tags.Sniadanak).ButOf(Tags.Salodkae).Select(ResultSelector.Single)
            .AddToMenu(FilterType.Not, Tags.Sniadanak, Tags.Abied).Select(ResultSelector.All)
            ;

        var extensiveGeneration = extensionsMenu.MakeMenu();
        Print(extensiveGeneration, "Extensions");

        var ensuranceOne = Dishes
            .Where(x => x.Tags.Contains(Tags.Sniadanak))
            .Where(x => !x.Tags.Contains(Tags.Salodkae))
            .First();

        var ensuranceAll = Dishes
            .Where(x => !x.Tags.Contains(Tags.Sniadanak))
            .Where(x => !x.Tags.Contains(Tags.Abied))
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
            Dishes.Random(Tags.Sniadanak),
            Dishes.Random(Tags.Abied),
            Dishes.Random(Tags.Viacera),
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