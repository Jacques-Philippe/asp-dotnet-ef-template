using Microsoft.EntityFrameworkCore;
using ContosoRESTAPI.Models;

namespace ContosoRESTAPI.Data;

public class PizzaContext : DbContext
{
    public PizzaContext(DbContextOptions<PizzaContext> options) : base(options) { }

    //Default constructor for Moq framework
    public PizzaContext() { }

    public virtual DbSet<Pizza> Pizzas => Set<Pizza>();
    public virtual DbSet<Topping> Toppings => Set<Topping>();
    public virtual DbSet<Sauce> Sauces => Set<Sauce>();
}
