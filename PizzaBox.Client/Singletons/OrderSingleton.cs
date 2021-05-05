using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Storing;

  public class OrderSingleton
  {
    public List<Order> Orders { get; set; }
    private readonly PizzaBoxContext _context;
    private static OrderSingleton _instance;
    public static OrderSingleton Instance(PizzaBoxContext context)
    {
      if (_instance == null)
      {
        _instance = new OrderSingleton(context);
      }
      return _instance;
    }
    private OrderSingleton(PizzaBoxContext context)
    {
      _context = context;
      if (Orders == null)
      {
        Orders = new List<Order>();
      }
    }

    public List<Order> GetOrdersByCustomer(Customer customer)
    {
      var orders = new List<Order>();
      var stores = _context.Stores
      .Include(s => s.Orders)
      .ThenInclude(o => o.Pizzas)
      .Include(s => s.Orders)
      .Include(s => s.Orders.Where(o => o.CustomerEntityId == customer.EntityId));

      foreach (AStore store in stores)
      {
        foreach (Order order in store.Orders)
        {
          orders.Add(order);
        }
      }
      return orders;
    }

    public List<Order> GetOrdersByStore(AStore aStore)
    {
      var orders = new List<Order>();
      var stores = _context.Stores
      .Include(s => s.Orders)
      .ThenInclude(o => o.Pizzas)
      .Include(s => s.Orders)
      .Include(s => s.Orders.Where(o => o.StoreEntityId == aStore.EntityId));

      foreach (AStore store in stores)
      {
        foreach (Order order in store.Orders)
        {
          orders.Add(order);
        }
      }
      return orders;
    }
  }
