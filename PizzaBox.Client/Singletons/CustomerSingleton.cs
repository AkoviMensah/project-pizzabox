using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Storing;

public class CustomerSingleton
{
  public readonly PizzaBoxContext _context;
  private static CustomerSingleton _instance;
  public List<Customer> Customers { get; }
  public static CustomerSingleton Instance(PizzaBoxContext context)
  {
      if (_instance == null)
      {
        _instance = new CustomerSingleton(context);
      }
      return _instance;
  }
  private CustomerSingleton(PizzaBoxContext context)
  {
    _context = context;
    if (Customers == null)
    {
      Customers = _context.Customers.ToList();
    }
  }
  public Customer AddCustomer(string name)
  {
    Customer customer = new Customer();
    customer.Name = name;
    _context.Customers.Add(customer);
    return customer;
  }
  public Customer GetCustomer(string name)
  {
    foreach (Customer customer in _context.Customers)
    {
      if (customer.Name == name)
      {
        return customer;
      }
    }
    return AddCustomer(name);
  }
}