using System;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Client.Singletons;
using PizzaBox.Storing;
using PizzaBox.Domain.Models.Stores;
using PizzaBox.Storing.Repositories;
using System.Linq;
using System.Collections.Generic;

namespace PizzaBox.Client
{
  /// <summary>
  /// 
  /// </summary>
  public class Program
  {
    private static readonly PizzaBoxContext _context = new PizzaBoxContext();
    private static readonly CustomerSingleton _customerSingleton = CustomerSingleton.Instance(_context);
    private static readonly PizzaSingleton _pizzaSingleton = PizzaSingleton.Instance(_context);
    private static readonly StoreSingleton _storeSingleton = StoreSingleton.Instance(_context);
    private static readonly OrderRepository _orderRepository = new OrderRepository(_context);

    /// <summary>
    /// 
    /// </summary>
    private static void Main()
    {
      Console.WriteLine("1 -- Customer");
      Console.WriteLine("2 -- Store");
      var valid = int.TryParse(Console.ReadLine(), out int input);

      if (input == 1) Run();
    }

    /// <summary>
    /// 
    /// </summary>
    private static void Run()
    {
      var order = new Order();

      Console.WriteLine("Welcome to PizzaBox");
      PrintListToScreen(_customerSingleton.Customers);

      order.Customer = SelectCustomer();
      order.Store = SelectStore();
      order.Pizza = SelectPizza();

      _orderRepository.Create(order);

      var orders = _context.Orders.Where(o => o.Customer.Name == order.Customer.Name);

      PrintListToScreen(orders);
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
      var index = 0;

      foreach (var item in _storeSingleton.Stores)
      {
        Console.WriteLine($"{++index} - {item}");
      }
    }

    private static void PrintListToScreen(IEnumerable<object> items)
    {
      var index = 0;

      foreach (var item in items)
      {
        Console.WriteLine($"{++index} - {item}");
      }
    }

    private static Customer SelectCustomer()
    {
      var valid = int.TryParse(Console.ReadLine(), out int input);

      if (!valid)
      {
        return null;
      }

      var customer = _customerSingleton.Customers[input - 1];

      PrintStoreList();

      return customer;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static APizza SelectPizza()
    {
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
  }
}