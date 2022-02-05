using Microsoft.EntityFrameworkCore;
using ContosoRESTAPI.Models;

namespace ContosoRESTAPI.Data;

public class PizzaContext : DbContext
{
    public PizzaContext(DbContextOptions<PizzaContext> options) : base(options) { }

    public PizzaContext() { }

    public virtual DbSet<Pizza> Pizzas => Set<Pizza>();
    public DbSet<Topping> Toppings => Set<Topping>();
    public DbSet<Sauce> Sauces => Set<Sauce>();
}
