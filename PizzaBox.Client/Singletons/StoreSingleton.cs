using System;
using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Storing;
using PizzaBox.Storing.Repositories;

namespace PizzaBox.Client.Singletons
{
  /// <summary>
  ///
  /// </summary>
  public class StoreSingleton
  {
    private const string _path = @"data/stores.xml";
    private readonly FileRepository _fileRepository = new FileRepository();
    public readonly PizzaBoxContext _context;
    private static StoreSingleton _instance;
    public List<AStore> Stores { get; }
    private StoreSingleton(PizzaBoxContext context)
    {
      _context = context;

      if (Stores == null)
      {
        Stores = _context.Stores.ToList();
      }
    }

    public static StoreSingleton Instance(PizzaBoxContext context)
    {
      if (_instance == null)
      {
        _instance = new StoreSingleton(context);
      }

      return _instance;
    }
    public void AddOrder(Order order)
    {
      if (order.Store.Orders == null) order.Store.Orders = new List<Order>();
      order.TimeOfPurchase = DateTime.Now;
      order.Store.Orders.Add(order);
      _context.SaveChanges();
    }

    public AStore GetStore(string name)
    {
    foreach (AStore s in _context.Stores)
    {
      if (s.Name == name)
      {
        return s;
      }
    }
    return null;
    }
  }
}