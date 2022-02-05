using Xunit;
using ContosoRESTAPI.Controllers;
using ContosoRESTAPI.Models;
using Moq;
using System.Collections.Generic;
using ContosoRESTAPI.Data;
using ContosoRESTAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;
using System.Diagnostics;
using System;

namespace ContosoRESTAPI.Tests;

public class PizzaServiceTest
{
    //Not sure how to make this work
    // [Fact]
    // public void Delete_ById_RemovesPizza()
    // {
    //     var data = ReturnMultiple().AsQueryable();

    //     //Arrange
    //     var mockSet = new Mock<DbSet<Pizza>>();
    //     mockSet.As<IQueryable<Pizza>>().Setup(m => m.Provider).Returns(data.Provider);
    //     mockSet.As<IQueryable<Pizza>>().Setup(m => m.Expression).Returns(data.Expression);
    //     mockSet.As<IQueryable<Pizza>>().Setup(m => m.ElementType).Returns(data.ElementType);
    //     mockSet.As<IQueryable<Pizza>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

    //     var mockContext = new Mock<PizzaContext>();
    //     mockContext.Setup(m => m.Pizzas).Returns(mockSet.Object);

    //     var mockService = new PizzaService(mockContext.Object);

    //     //Act
    //     int deletedId = 1;
    //     mockService.DeleteById(deletedId);
    //     var pizzas = mockService.GetAll().ToList();

    //     //Assert
    //     Assert.Equal(expected: 2, pizzas.Count);
    //     Assert.Null(pizzas.Where(p => p.Id == deletedId));
    // }

    [Fact]
    public void Create_WithValidPizzaArg_ReturnsCreatedPizzaAndAddsPizza()
    {
        var pepperoniTopping = new Topping { Name = "Pepperoni", Calories = 130 };
        var tomatoSauce = new Sauce { Name = "Tomato", IsVegan = true };
        var pizza = new Pizza
        {
            Id = 1,
            Name = "New pizza 1000",
            Toppings = new List<Topping> { pepperoniTopping },
            Sauce = tomatoSauce
        };

        //Arrange
        var mockSet = new Mock<DbSet<Pizza>>();
        var mockContext = new Mock<PizzaContext>();
        mockContext.Setup(m => m.Pizzas).Returns(mockSet.Object);

        var mockService = new PizzaService(mockContext.Object);

        //Act
        var createdPizza = mockService.Create(pizza);

        //Assert
        Assert.Equal(createdPizza, pizza);
        Assert.NotNull(createdPizza);
    }

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
        var pizzas = mockService.GetAll().ToList();

        //Assert
        Assert.Equal(pizzas.Count, 3);
        Assert.NotNull(pizzas[0].Sauce);
    }

    [Fact]
    public void Get_ExistingId_ReturnsPizzaWithId()
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
        var allPizzas = mockService.GetAll().ToList();
        var pizza = mockService.GetById(1);

        //Assert
        Assert.Equal(pizza, allPizzas[0]);
    }

    [Fact]
    public void Get_NonExistingId_ReturnsNull()
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
        var pizza = mockService.GetById(-1);

        //Assert
        Assert.Null(pizza);
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
                Id = 1,
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
                Id = 2,
                Name = "Hawaiian",
                Sauce = tomatoSauce,
                Toppings = new List<Topping> { pineappleTopping, hamTopping }
            },
            new Pizza
            {
                Id = 3,
                Name = "Alfredo Chicken",
                Sauce = alfredoSauce,
                Toppings = new List<Topping> { chickenTopping }
            }
        };
        return pizzas;
    }


}
