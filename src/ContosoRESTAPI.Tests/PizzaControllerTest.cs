using Xunit;
using ContosoRESTAPI.Controllers;
using ContosoRESTAPI.Models;
using Moq;
using System.Collections.Generic;
using ContosoRESTAPI.Data;
using ContosoRESTAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ContosoRESTAPI.Tests;

public class PizzaControllerTest
{
    [Fact]
    public void Get_NoArgs_ReturnsAllPizzas()
    {
        var data = ReturnMultiple().AsQueryable();

        //Arrange
        var mockSet = new Mock<DbSet<Pizza>>();
        mockSet.As<IQueryable<Pizza>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Pizza>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Pizza>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Pizza>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<PizzaContext>();
        mockContext.Setup(m => m.Pizzas).Returns(mockSet.Object);

        var mockService = new PizzaService(mockContext.Object);

        //Act
        var pizzas = mockService.GetAll();

        //Assert
        Assert.Equal(pizzas.Count(), 3);
    }

    private IEnumerable<Pizza> ReturnMultiple()
    {
        //Toppings
        var pepperoniTopping = new Topping { Name = "Pepperoni", Calories = 130 };
        var sausageTopping = new Topping { Name = "Sausage", Calories = 100 };
        var hamTopping = new Topping { Name = "Ham", Calories = 70 };
        var chickenTopping = new Topping { Name = "Chicken", Calories = 50 };
        var pineappleTopping = new Topping { Name = "Pineapple", Calories = 75 };

        //Sauces
        var tomatoSauce = new Sauce { Name = "Tomato", IsVegan = true };
        var alfredoSauce = new Sauce { Name = "Alfredo", IsVegan = false };

        //Pizzas
        var pizzas = new Pizza[]
        {
            new Pizza
            {
                Name = "Meat Lovers",
                Sauce = tomatoSauce,
                Toppings = new List<Topping>
                {
                    pepperoniTopping,
                    sausageTopping,
                    hamTopping,
                    chickenTopping
                }
            },
            new Pizza
            {
                Name = "Hawaiian",
                Sauce = tomatoSauce,
                Toppings = new List<Topping> { pineappleTopping, hamTopping }
            },
            new Pizza
            {
                Name = "Alfredo Chicken",
                Sauce = alfredoSauce,
                Toppings = new List<Topping> { chickenTopping }
            }
        };
        return pizzas;
    }


}
