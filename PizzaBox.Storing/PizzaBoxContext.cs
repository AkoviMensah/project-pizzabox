using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Models.Pizzas;
using PizzaBox.Domain.Models.Stores;

namespace PizzaBox.Storing
{
  public class PizzaBoxContext : DbContext
  {
    private readonly IConfiguration _configuration;
    public DbSet<AStore> Stores { get; set; }
    public DbSet<APizza> Pizzas { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Topping> Toppings { get; set; }
    public DbSet<Crust> Crust { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public PizzaBoxContext()
    {
      _configuration = new ConfigurationBuilder().AddUserSecrets<PizzaBoxContext>().Build();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
      builder.UseSqlServer(_configuration["mssql"]);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<AStore>().HasKey(e => e.EntityId);
      builder.Entity<ChicagoStore>().HasBaseType<AStore>();
      builder.Entity<NewYorkStore>().HasBaseType<AStore>();

      builder.Entity<APizza>().HasKey(e => e.EntityId);
      builder.Entity<CustomPizza>().HasBaseType<APizza>();
      builder.Entity<MeatPizza>().HasBaseType<APizza>();
      builder.Entity<VeggiePizza>().HasBaseType<APizza>();

      builder.Entity<Crust>().HasKey(e => e.EntityId);

      builder.Entity<Order>().HasKey(e => e.EntityId);
      builder.Entity<Size>().HasKey(e => e.EntityId);
      builder.Entity<Topping>().HasKey(e => e.EntityId);

      builder.Entity<Customer>().HasKey(e => e.EntityId);

      OnDataSeeding(builder);
    }

    private void OnDataSeeding(ModelBuilder builder)
    {
      builder.Entity<ChicagoStore>().HasData(new ChicagoStore[]
      {
        new ChicagoStore() { EntityId = 1, Name = "ChicagoStore"}
      });

      builder.Entity<NewYorkStore>().HasData(new NewYorkStore[]
      {
        new NewYorkStore() { EntityId = 2, Name = "NewYorkStore"}
      });

      builder.Entity<Crust>().HasData(new Crust[]
{
        new Crust() { EntityId = 1, Name = "Original", Price = 4.00M}
});
      builder.Entity<Crust>().HasData(new Crust[]
      {
        new Crust() { EntityId = 2, Name = "Stuffed", Price = 4.00M}
      });
      builder.Entity<Crust>().HasData(new Crust[]
      {
        new Crust() { EntityId = 3, Name = "Thin", Price = 4.00M}
      });
      builder.Entity<Crust>().HasData(new Crust[]
      {
        new Crust() { EntityId = 4, Name = "Neapolitan", Price = 4.00M}
      });
      builder.Entity<Size>().HasData(new Size[]
      {
        new Size("Small") { EntityId = 1, Price = 6.00M}
      });
      builder.Entity<Size>().HasData(new Size[]
      {
        new Size("Medium") { EntityId = 2, Price = 8.00M}
      });
      builder.Entity<Size>().HasData(new Size[]
      {
        new Size("Large") { EntityId = 3, Price = 10.00M}
      });
      builder.Entity<Topping>().HasData(new Topping[]
      {
        new Topping("peppers") {EntityId = 1, Price = 1.00M},
      });
      builder.Entity<Topping>().HasData(new Topping[]
      {
        new Topping("onions")  {EntityId = 2, Price = 1.00M},
      });
      builder.Entity<Topping>().HasData(new Topping[]
      {
        new Topping("olives")  {EntityId = 3, Price = 1.00M},
      });
      builder.Entity<Topping>().HasData(new Topping[]
      {
        new Topping("Pepperoni")  {EntityId = 4, Price = 1.00M},
      });
      builder.Entity<Topping>().HasData(new Topping[]
      {
        new Topping("Marinara")  {EntityId = 5, Price = 1.00M},
      });
    }
  }
}