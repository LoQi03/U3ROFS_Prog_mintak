using System.Text;
using CoffeeShopPatterns.Creational;
using CoffeeShopPatterns.Structural;
using CoffeeShopPatterns.Behavioral;

// Coffee shop ordering system – three design patterns on one shared example:
//   1. Factory Method  – creational pattern  -> preparing the base beverage
//   2. Decorator       – structural pattern  -> adding extras
//   3. Observer        – behavioral pattern  -> notifications about the order status
//
// Run:  dotnet run              (runs straight through)
//       dotnet run -- --step    (waits for Enter after each part – for live demos)

Console.OutputEncoding = Encoding.UTF8;
bool stepByStep = args.Contains("--step");

void Header(string title)
{
    if (stepByStep)
    {
        Console.Write("\n(Press Enter to continue...)");
        Console.ReadLine();
    }
    Console.WriteLine();
    Console.WriteLine(new string('=', 60));
    Console.WriteLine("  " + title);
    Console.WriteLine(new string('=', 60));
}

Console.WriteLine("☕  COFFEE SHOP ORDERING SYSTEM – design patterns demo");

// ─────────────────────────────────────────────────────────────
// 1. FACTORY METHOD
// ─────────────────────────────────────────────────────────────
Header("1. Factory Method – creational pattern");

// The client never calls "new Espresso()": it only knows BeverageMaker.
// New beverage type = new Maker subclass; the loop below stays unchanged.
BeverageMaker[] makers =
{
    new EspressoMaker(),
    new TeaMaker(),
    new HotChocolateMaker()
};

foreach (BeverageMaker maker in makers)
{
    IBeverage beverage = maker.Serve();
    Console.WriteLine($"     -> {beverage.Description,-15} {beverage.Price,5} HUF  (type: {beverage.GetType().Name})");
}

// ─────────────────────────────────────────────────────────────
// 2. DECORATOR
// ─────────────────────────────────────────────────────────────
Header("2. Decorator – structural pattern");

// The base beverage comes from the factory method; extras are "wrapped" around it at runtime.
IBeverage latte = new EspressoMaker().Serve();
Console.WriteLine($"     base:          {latte.Description} – {latte.Price} HUF");

latte = new Milk(latte);
Console.WriteLine($"     + Milk:        {latte.Description} – {latte.Price} HUF");

latte = new CaramelSyrup(latte);
Console.WriteLine($"     + Caramel:     {latte.Description} – {latte.Price} HUF");

latte = new ExtraShot(latte);
Console.WriteLine($"     + Extra shot:  {latte.Description} – {latte.Price} HUF");

// The same decorators work on a completely different beverage:
IBeverage chocolate = new WhippedCream(new CaramelSyrup(new HotChocolateMaker().Serve()));
Console.WriteLine($"     Another drink: {chocolate.Description} – {chocolate.Price} HUF");

Console.WriteLine("\n     4 decorators + 3 beverages = 7 classes – with inheritance it would take 3 × 2⁴ = 48!");

// ─────────────────────────────────────────────────────────────
// 3. OBSERVER
// ─────────────────────────────────────────────────────────────
Header("3. Observer – behavioral pattern");

var display = new CounterDisplay();
var register = new CashRegister();
var annaApp = new CustomerApp("Anna");

var order = new Order(42, latte);
order.Subscribe(display);
order.Subscribe(register);
order.Subscribe(annaApp);
Console.WriteLine($"  Order #{order.Number}: {order.Beverage.Description} ({order.Beverage.Price} HUF)");
Console.WriteLine("  Subscribers: Display, Register, Anna's app");

// The order only changes its status – it does not know WHO reacts or HOW.
order.Status = OrderStatus.Preparing;
order.Status = OrderStatus.Ready;

order.Unsubscribe(annaApp);   // Anna picked it up, she no longer needs notifications
Console.WriteLine("\n  (Anna's app unsubscribed)");

order.Status = OrderStatus.Delivered;

// ─────────────────────────────────────────────────────────────
Header("Summary");
Console.WriteLine("  Factory Method : HOW is the beverage created?   -> the subclass decides");
Console.WriteLine("  Decorator      : WHAT is the beverage made of?  -> runtime composition");
Console.WriteLine("  Observer       : WHO knows when it changes?     -> subscribe / notify");
Console.WriteLine();
