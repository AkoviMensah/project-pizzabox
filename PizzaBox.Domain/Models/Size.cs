using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
  /// <summary>
  ///
  /// </summary>
  public class Size : AComponent
  {
    public Size() { }
    public Size(string size)
    {
      Name = size;
      if (size == "Small") Price = 6.00M;
      if (size == "Medium") Price = 8.00M;
      if (size == "Large") Price = 10.00M;
    }
  }
}