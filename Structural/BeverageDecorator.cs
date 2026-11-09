using CoffeeShopPatterns.Creational;

namespace CoffeeShopPatterns.Structural;

/// <summary>
/// DECORATOR – structural pattern.
///
/// Problem: if we wrote a separate subclass for every combination (espresso with milk,
/// espresso with milk and caramel, hot chocolate with caramel and whipped cream...),
/// the number of classes would explode.
///
/// Solution: the decorator implements the SAME interface (IBeverage) as the object
/// it wraps, and adds its own behaviour through COMPOSITION instead of inheritance.
/// Decorators can be stacked at runtime, in any order.
/// </summary>
public abstract class BeverageDecorator : IBeverage
{
    protected readonly IBeverage _beverage;   // the wrapped object

    protected BeverageDecorator(IBeverage beverage) => _beverage = beverage;

    public abstract string Description { get; }
    public abstract int Price { get; }
}

// ---- "ConcreteDecorator" classes ----

public class Milk : BeverageDecorator
{
    public Milk(IBeverage beverage) : base(beverage) { }
    public override string Description => _beverage.Description + " + milk";
    public override int Price => _beverage.Price + 150;
}

public class CaramelSyrup : BeverageDecorator
{
    public CaramelSyrup(IBeverage beverage) : base(beverage) { }
    public override string Description => _beverage.Description + " + caramel syrup";
    public override int Price => _beverage.Price + 200;
}

public class WhippedCream : BeverageDecorator
{
    public WhippedCream(IBeverage beverage) : base(beverage) { }
    public override string Description => _beverage.Description + " + whipped cream";
    public override int Price => _beverage.Price + 250;
}

public class ExtraShot : BeverageDecorator
{
    public ExtraShot(IBeverage beverage) : base(beverage) { }
    public override string Description => _beverage.Description + " + extra shot";
    public override int Price => _beverage.Price + 300;
}
