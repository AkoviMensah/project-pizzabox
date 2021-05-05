using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Models;
using PizzaBox.Storing;

namespace PizzaBox.Client.Singletons
{
  public class CrustSingleton
  {
    private static CrustSingleton _instance;
    private static PizzaBoxContext _context;
    public List<Crust> Crusts { get; set; }

    public static CrustSingleton Instance(PizzaBoxContext context)
    {
      if (_instance == null)
      {
        _instance = new CrustSingleton(context);
      }
      return _instance;
    }
    private CrustSingleton(PizzaBoxContext context)
    {
      _context = context;
      Crusts = _context.Crust.ToList();
    }
  }
}