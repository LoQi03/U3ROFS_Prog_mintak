using CoffeeShopPatterns.Creational;

namespace CoffeeShopPatterns.Behavioral;

public enum OrderStatus
{
    Received,
    Preparing,
    Ready,
    Delivered
}

/// <summary>
/// OBSERVER – behavioral pattern.
///
/// Common "Subscriber" interface: a single method that the publisher calls
/// when an event happens. The publisher only knows this interface,
/// not the concrete observer types.
/// </summary>
public interface IOrderObserver
{
    void Update(Order order);
}

/// <summary>
/// "Publisher": keeps track of its subscribers and notifies each of them
/// (and only them) when its status changes.
/// </summary>
public class Order
{
    private readonly List<IOrderObserver> _observers = new();
    private OrderStatus _status = OrderStatus.Received;

    public int Number { get; }
    public IBeverage Beverage { get; }

    public Order(int number, IBeverage beverage)
    {
        Number = number;
        Beverage = beverage;
    }

    // --- the publisher's three key methods ---
    public void Subscribe(IOrderObserver observer) => _observers.Add(observer);
    public void Unsubscribe(IOrderObserver observer) => _observers.Remove(observer);

    private void Notify()
    {
        // iterate over a copy, so an observer can unsubscribe while being notified
        foreach (var observer in _observers.ToList())
            observer.Update(this);
    }

    public OrderStatus Status
    {
        get => _status;
        set
        {
            if (_status == value) return;
            _status = value;
            Console.WriteLine($"\n  >> Order #{Number} new status: {_status}");
            Notify();
        }
    }
}
