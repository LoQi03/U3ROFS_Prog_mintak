namespace CoffeeShopPatterns.Behavioral;

// ---- "ConcreteSubscriber" classes: each one handles the event in its own way ----

/// <summary>The customer's mobile app – only speaks up when the drink is ready.</summary>
public class CustomerApp : IOrderObserver
{
    private readonly string _name;
    public CustomerApp(string name) => _name = name;

    public void Update(Order o)
    {
        if (o.Status == OrderStatus.Ready)
            Console.WriteLine($"     [App] {_name}'s phone: \"Your {o.Beverage.Description} is ready, come and get it!\"");
    }
}

/// <summary>The display above the counter – shows every status change.</summary>
public class CounterDisplay : IOrderObserver
{
    public void Update(Order o) =>
        Console.WriteLine($"     [Display] #{o.Number} – {o.Status}");
}

/// <summary>The cash register – records the revenue on delivery, then unsubscribes.</summary>
public class CashRegister : IOrderObserver
{
    public int Revenue { get; private set; }

    public void Update(Order o)
    {
        if (o.Status != OrderStatus.Delivered) return;

        Revenue += o.Beverage.Price;
        Console.WriteLine($"     [Register] +{o.Beverage.Price} HUF (today's revenue: {Revenue} HUF)");
        o.Unsubscribe(this);   // removes the relationship at runtime
    }
}
