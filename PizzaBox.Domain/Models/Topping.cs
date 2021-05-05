using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
  public class Topping : AComponent
  {
    public static List<APizza> Pizzas { get; set; }
    public Topping(string name)
    {
      Price = 1.00M;
      Name = name;

    }
    public Topping()
    {
      Price = 1.00M;
    }

  }
}