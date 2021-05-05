using System;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Client.Singletons;
using PizzaBox.Domain.Models.Pizzas;
using System.Collections.Generic;
using PizzaBox.Storing;

namespace PizzaBox.Client
{
    /// <summary>
    ///
    /// </summary>
    public class Program
    {
        private static readonly PizzaBoxContext _context = new PizzaBoxContext();
        private static readonly CustomerSingleton _customerSingleton = CustomerSingleton.Instance(_context);
        private static readonly SizeSingleton _sizeSingleton = SizeSingleton.Instance(_context);
        private static readonly ToppingSingleton _toppingSingleton = ToppingSingleton.Instance(_context);
        private static readonly CrustSingleton _crustSingleton = CrustSingleton.Instance(_context);
        private static readonly PizzaSingleton _pizzaSingleton = PizzaSingleton.Instance(_context);
        private static readonly StoreSingleton _storeSingleton = StoreSingleton.Instance(_context);
        private static readonly OrderSingleton _orderSingleton = OrderSingleton.Instance(_context);
        private static Order order = new Order();
        private static void Main()
        {
            Run();
        }
        private static void Run()
        {
            Console.WriteLine("------- Welcome to PizzaBox -------");
            var users = new List<string> { "Customer", "Store" };
            var choice = getItem(users, "Please chose an aption");
            if (choice == "Store") StoreUI();
            else if (choice == "Customer") CustomerUI();
            else Run();
        }
        private static void StartNewOrder()
        {
            order.Customer = SelectCustomer();
            order.Pizzas = new List<APizza>();
            order.Store = getItem(_storeSingleton.Stores, "Please select a store");
            addPizza();
            PlaceOrder();
        }
        private static Customer SelectCustomer()
        {
            print("Please enter your name");
            string name = System.Console.ReadLine().ToUpper();
            return _customerSingleton.GetCustomer(name);
        }
        private static void CustomerUI()
        {
            List<string> items = new List<string>(){ "Start a new order", "View order history",
      "Remove pizza from order", "Exit"};
            print("Menu");
            for (int i = 0; i < items.Count; i++) Console.WriteLine($"{i + 1} -- {items[i].ToString()}");
            var valid = int.TryParse(Console.ReadLine(), out int input);
            if (input == 1) StartNewOrder();
            else if (input == 3) removePizza();
            else if (input == 2)
            {
                Console.WriteLine("Please enter your name");
                string name = Console.ReadLine().ToUpper();
                var orders = _orderSingleton.GetOrdersByCustomer(_customerSingleton.GetCustomer(name));
                foreach (Order o in orders) Console.WriteLine(o.ToString());
            }
            else Console.WriteLine("Invalid input");
        }
        private static void addPizza()
        {
            var p = getItem(_pizzaSingleton.PizzasMenu, "Please select a pizza");
            order.Pizzas.Add(MakePizza(ref p));
            var temp = getItem(new List<string>() { "Yes", "No" }, "Add another pizza?");
            if (temp == "Yes") addPizza();
            else if (temp == "No") return;
            else Console.WriteLine("Invalid input");
        }
        private static void addTopping(ref APizza pizza, string str)
        {
            if (pizza.Toppings.Count >= 5 || str.Equals("No")) return;
            pizza.Toppings.Add(getItem(_toppingSingleton.Toppings, "Please select a topping"));
            if (pizza.Toppings.Count < 2) addTopping(ref pizza, "Yes");
            else
            {
                var temp = getItem(new List<string> { "Yes", "No" }, "Add another topping?");
                Console.WriteLine(temp);
                if (temp == "Yes") addTopping(ref pizza, "Yes");
                else if (temp == "No") addTopping(ref pizza, "No");
                else Console.WriteLine("Invalid input");
            }

        }
        private static void removePizza()
        {
            if (order.Pizzas.Count == 0)
            {
                Console.WriteLine("There is no pizza to remove");
                return;
            }
            APizza pizza = getItem(order.Pizzas, "Select a pizza to remove");
            order.Pizzas.Remove(pizza);
            CustomerUI();
        }
        public static void PlaceOrder()
        {
            var temp = getItem(new List<string> { "Place order", "Edit" }, "Continue");
            if (temp == "Edit") CustomerUI();
            else if (temp == "Place order")
            {
                if (order.TotalCost <= 250 && order.Pizzas.Count <= 50)
                {
                    _storeSingleton.AddOrder(order);
                    System.Console.WriteLine("Your order has been placed.");
                    order = new Order();
                }
                else
                {
                    System.Console.WriteLine("Make sure TotalCost is <= 250");
                    System.Console.WriteLine("AND pizzas count is <= 50");
                    CustomerUI();
                }
            }
            else Console.WriteLine("Invalid input");
        }
        private static T getItem<T>(List<T> items, string prompt) where T : class
        {
            print(prompt);
            for (int i = 0; i < items.Count; i++) Console.WriteLine($"{i + 1} -- {items[i].ToString()}");
            var valid = int.TryParse(Console.ReadLine(), out int input);
            if (!valid) getItem(items, prompt);
            if ((!valid) || input < 1 || input > items.Count)
            {
                Console.WriteLine("Invalid input");
            }
            return items[input - 1];
        }

        private static APizza MakePizza(ref APizza p)
        {
            p.Size = getItem(_sizeSingleton.Sizes, "Please select a size");
            if (p.GetType().Equals(typeof(CustomPizza)))
            {
                p.Toppings = new List<Topping>();
                p.Crust = getItem(_crustSingleton.Crusts, "Please select a crust");
                addTopping(ref p, "Yes");
            }
            return p;
        }

        private static void print(string prompt)
        {
            Console.WriteLine("___________________________________");
            Console.WriteLine(prompt);
        }

        private static void StoreUI()
        {
            var store = getItem(_storeSingleton.Stores, "Select your store");
            Console.WriteLine("1 -- Views orders of the store");
            Console.WriteLine("2 -- Views customer's orders");
            var valid = int.TryParse(Console.ReadLine(), out int input);
            if ((!valid) || input < 1 || input > 2) Console.WriteLine("Invalid input");
            else if (input == 1)
            {
                var sum = 0M;
                var orders = _orderSingleton.GetOrdersByStore(store);
                foreach (Order o in orders)
                {
                    sum += o.TotalCost;
                    Console.WriteLine(o.ToString());
                }
                Console.WriteLine($"--- PizzaCount: {orders.Count} ----- TotalCost: {sum}");
            }
            else if (input == 2)
            {
                Customer c = getItem(_customerSingleton.Customers, "Please select a customer");
                var orders = _orderSingleton.GetOrdersByCustomer(c);
                foreach (Order o in orders)
                {
                    if (o.Store.Name == store.Name) Console.WriteLine(o.ToString());
                }
            }
        }
    }
}