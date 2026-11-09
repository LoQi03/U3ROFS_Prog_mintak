namespace CoffeeShopPatterns.Creational;

/// <summary>
/// "Product" – the common interface implemented by every beverage (and later by every decorator).
/// The client only knows this interface, never the concrete classes.
/// </summary>
public interface IBeverage
{
    string Description { get; }
    int Price { get; }   // in HUF
}

// ---- "ConcreteProduct" classes ----

public class Espresso : IBeverage
{
    public string Description => "Espresso";
    public int Price => 590;
}

public class Tea : IBeverage
{
    public string Description => "Earl Grey tea";
    public int Price => 490;
}

public class HotChocolate : IBeverage
{
    public string Description => "Hot chocolate";
    public int Price => 790;
}
