namespace Cooking.Domain.Test;

public class ProviderOne
{
    public ProviderOne()
    {
        Console.WriteLine("First");
    }
}

public class ProviderTwo
{
    public ProviderTwo()
    {
        Console.WriteLine("Second");
    }
}

public class ProviderThree
{
    public ProviderThree()
    {
        Console.WriteLine("Third");
    }
}

public static class Ext
{
    public static ProviderOne Exe<T>(this IEnumerable<T> coll)
    {
        return new ProviderOne();
    }

    public static ProviderTwo Exe(this ProviderOne coll)
    {
        return new ProviderTwo();
    }

    public static ProviderThree Exe(this ProviderTwo coll)
    {
        return new ProviderThree();
    }
}
