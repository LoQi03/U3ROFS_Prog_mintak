namespace CoffeeShopPatterns.Creational;

/// <summary>
/// FACTORY METHOD – creational pattern.
///
/// "Creator": defines the interface for creating a beverage (Brew),
/// but lets subclasses decide WHICH class gets instantiated.
/// The client calls the factory method instead of using the new operator.
/// </summary>
public abstract class BeverageMaker
{
    /// <summary>The factory method.</summary>
    protected abstract IBeverage Brew();

    /// <summary>
    /// Shared logic that is the same for every beverage. It does not know (and does not
    /// need to know) which concrete beverage the factory method returns.
    /// </summary>
    public IBeverage Serve()
    {
        IBeverage beverage = Brew();
        Console.WriteLine($"  [{GetType().Name}] prepared: {beverage.Description}");
        return beverage;
    }
}

// ---- "ConcreteCreator" classes: each one creates its own product ----

public class EspressoMaker : BeverageMaker
{
    protected override IBeverage Brew() => new Espresso();
}

public class TeaMaker : BeverageMaker
{
    protected override IBeverage Brew() => new Tea();
}

public class HotChocolateMaker : BeverageMaker
{
    protected override IBeverage Brew() => new HotChocolate();
}
