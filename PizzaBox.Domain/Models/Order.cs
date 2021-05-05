using System;
using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
  public class Order : AModel
  {
    public Customer Customer { get; set; }
    public long CustomerEntityId { get; set; }
    public AStore Store { get; set; }
    public long StoreEntityId { get; set; }
    public DateTime TimeOfPurchase { get; set; }
    public List<APizza> Pizzas { get; set; }
    public Order()
    {
      Pizzas = new List<APizza>();
    }

    public decimal TotalCost
    {
      get
      {
        var sum = 0M;
        foreach (APizza pizza in Pizzas)
        {
          sum += pizza.GetPrice();
        }
        return sum;
      }
    }

    public override string ToString()
    {
      var str = $"{Customer.Name}'s order";
      foreach(APizza pizza in Pizzas)
      {
        str += "\n" +"---" + pizza.ToString();
      }
      return str;
    }
  
  }
}