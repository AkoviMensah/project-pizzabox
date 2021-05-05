using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Storing;
using PizzaBox.Storing.Repositories;

namespace PizzaBox.Client.Singletons
{
  /// <summary>
  ///
  /// </summary>
  public class PizzaSingleton
  {
    private readonly FileRepository _fileRepository = new FileRepository();
    private static PizzaSingleton _instance;
    private const string _path = @"data/pizzas.xml";

    private readonly PizzaBoxContext _context = new PizzaBoxContext();

    public List<APizza> PizzasMenu { get; set; }
    public List<APizza> Pizzas { get; set; }
    public static PizzaSingleton Instance(PizzaBoxContext context)
    {
        if (_instance == null)
        {
          _instance = new PizzaSingleton(context);
        }

        return _instance;
    }
    private PizzaSingleton(PizzaBoxContext context)
    {
      _context = context;
      if (PizzasMenu == null)
      {
        PizzasMenu = _fileRepository.ReadFromFile<List<APizza>>(_path);
      }
    }
  }
}