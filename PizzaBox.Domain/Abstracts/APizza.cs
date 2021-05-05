using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Models.Pizzas;

namespace PizzaBox.Domain.Abstracts
{
  /// <summary>
  ///
  /// </summary>
  [XmlInclude(typeof(CustomPizza))]
  [XmlInclude(typeof(MeatPizza))]
  [XmlInclude(typeof(VeggiePizza))]
  public abstract class APizza : AModel
  {
    public string Name;
    public Crust Crust { get; set; }
    public Size Size { get; set; }
    public long CrustEntityId { get; set; }
    public long SizeEntityId { get; set; }
    public List<Topping> Toppings { get; set; }

    protected APizza()
    {
      Factory();
    }

    protected virtual void Factory()
    {
      AddName();
      AddCrust();
      AddSize();
      AddToppings();
    }

    protected abstract void AddName();
    protected abstract void AddCrust();
    protected abstract void AddSize();
    protected abstract void AddToppings();

    public override string ToString()
    {
      var stringBuilder = new StringBuilder();
      var separator = ", ";

      foreach (var item in Toppings)
      {
        stringBuilder.Append($"{item}{separator}");
      }

      return $"{Name} \n\t {Size} - {Crust} - {stringBuilder.ToString().TrimEnd(separator.ToCharArray())} - ${GetPrice()}\n";
    }
    public decimal GetPrice()
    {
      decimal sum = Crust.Price + Size.Price;
      foreach (Topping t in Toppings)
      {
        sum += t.Price;
      }
      return sum;
    }
  }
}