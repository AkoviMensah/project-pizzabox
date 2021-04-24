using System;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Client.Singletons;

namespace PizzaBox.Client
{
  /// <summary>
  /// 
  /// </summary>
  public class CustomerUI
  {
    private static readonly PizzaSingleton _pizzaSingleton = PizzaSingleton.Instance;
    private static readonly StoreSingleton _storeSingleton = StoreSingleton.Instance;

    /// <summary>
    /// 
    /// </summary>
    public Order order;
    
    public void Run()
    {
      order = new Order();

      PrintStoreList();

      order.Customer = new Customer();
      order.Store = SelectStore();
      order.Pizza = SelectPizza();
      Checkout();
      //////////////////////Save to database
      Start();
    }

    /// <summary>
    /// 
    /// </summary>
    private static void PrintOrder(APizza pizza)
    {
      Console.WriteLine($"Your order is: {pizza}");
    }

    /// <summary>
    /// 
    /// </summary>
    private static void PrintPizzaList()
    {
      Console.WriteLine("Here are the available pizzas");
      var index = 0;

      foreach (var item in _pizzaSingleton.Pizzas)
      {
        Console.WriteLine($"{++index} - {item}");
      }
    }

    /// <summary>
    /// 
    /// </summary>
    private static void PrintStoreList()
    {
      Console.WriteLine("Please select a store:");
      var index = 0;

      foreach (var item in _storeSingleton.Stores)
      {
        Console.WriteLine($"{++index} - {item}");
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static APizza SelectPizza()
    {
      Console.WriteLine("Select a pizza:");
      var valid = int.TryParse(Console.ReadLine(), out int input);

      if (!valid)
      {
        return null;
      }

      var pizza = _pizzaSingleton.Pizzas[input - 1];

      PrintOrder(pizza);

      return pizza;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static AStore SelectStore()
    {
      var valid = int.TryParse(Console.ReadLine(), out int input);

      if (!valid)
      {
        return null;
      }

      PrintPizzaList();

      return _storeSingleton.Stores[input - 1];
    }

    private void PrintMenu()
    {
      Console.WriteLine("1 -- Start a new order");
      Console.WriteLine("2 -- Remove a pizza from order");
      Console.WriteLine("3 -- View order history");
    }

    public void Start()
    {
      PrintMenu();
      var valid = int.TryParse(Console.ReadLine(), out int input);

      if (input == 1) Run();
      else if (input == 2) RemovePizza();
      else 
      {
        Console.WriteLine("Invalid input");
        Start();
      }
    }

    private void RemovePizza()
    {
      try
      {
        order.Pizza = null;
        Run();
      }catch(Exception)
      {
        Console.WriteLine("You have no pizza to remove from order");
        Start();
      }
    }

    private void Checkout()
    {
      Console.WriteLine("1 -- Checkout and place order");
      Console.WriteLine("2 -- Edit the Pizza");
      Console.WriteLine("3 -- Edit order / View order history");
      
      var valid = int.TryParse(Console.ReadLine(), out int input);
      if(input == 3) Start();
      else if (input == 1) 
      {
        Console.WriteLine("Your order is placed");
        order = null;
      }
    }
  }
}
