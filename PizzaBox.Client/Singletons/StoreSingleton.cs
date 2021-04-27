using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
    private readonly PizzaBoxContext _context;
    private static StoreSingleton _instance;

    public List<AStore> Stores { get; }


    /// <summary>
    /// 
    /// </summary>
    private StoreSingleton(PizzaBoxContext context)
    {
      _context = context;

      if (Stores == null)
      {
         _context.Stores.AddRange(_fileRepository.ReadFromFile<List<AStore>>(_path));
         _context.SaveChanges();

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

    public IEnumerable<AStore> ViewOrders(AStore store)
    {
      // lambda - lINQ (linq to objects)
      // EF Loading = Eager Loading
      var orders = _context.Stores //load all stores
                    .Include(s => s.Orders) // load all orders for all stores
                    .ThenInclude(o => o.Pizza) // load all pizzas for all orders
                    .Where(s => s.Name == store.Name); // LINQ = lang integrated query

      // EF Explicit Loading
      var st = _context.Stores.FirstOrDefault(s => s.Name == store.Name);
      _context.Entry<AStore>(store).Collection<Order>(s => s.Orders).Load(); // load all orders+ properties for 1 store

      // sql - LINQ (ling to sql)
      // EF Lazy Loading
      var orders2 = from r in _context.Stores
                      //join ro in _context.Orders on r.EntityId == ro.StoreEntityId
                    where r.Name == store.Name
                    select r;

      return orders.ToList();
    }
  }
}