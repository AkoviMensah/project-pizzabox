using System;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Client.Singletons;

namespace PizzaBox.Client
{
  public class Program
  {
    private static void Main()
    {
      Console.WriteLine("1 -- Customer");
      Console.WriteLine("2 -- Store");
      var valid = int.TryParse(Console.ReadLine(), out int input);

      if (input == 1)
      {
        CustomerUI customerUI = new CustomerUI();
        customerUI.Start();
      }
    }
  }
}
