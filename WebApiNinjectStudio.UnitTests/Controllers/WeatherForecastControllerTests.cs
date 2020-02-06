﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Moq;
using WebApiNinjectStudio.Domain.Concrete;
using WebApiNinjectStudio.Domain.Abstract;
using WebApiNinjectStudio.Domain.Entities;
using WebApiNinjectStudio.Controllers;
using Microsoft.AspNetCore.Mvc;



namespace WebApiNinjectStudio.UnitTests.Controllers
{
    public class WeatherForecastControllerTests
    {
        [Fact]
        public void Get()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(pr => pr.Products).Returns(new Product[] {
                new Product {
                    ProductID = 1,
                    Name ="product1",
                    Description = "product description 1",
                    Price = 12M,
                    ProductImage = new ProductImage { ProductImageId = 1, Url = "www.dr.dk/a.jpg" },
                    ProductTag = new ProductTag []
                    {
                        new ProductTag {ProductTagID = 1, Name = "Red" },
                        new ProductTag {ProductTagID = 2, Name = "blue"}
                    }
                },
                new Product {
                    ProductID = 2,
                    Name ="product2",
                    Description = "product description 2",
                    Price = 12M,
                    ProductImage = new ProductImage { ProductImageId = 2, Url = "www.dr.dk/b.jpg" }
                }
            });

            WeatherForecastController target = new WeatherForecastController(mock.Object);
            Product[] result = ((IEnumerable<Product>)target.Get()).ToArray();

            Assert.Equal(2, result.Length);
            Assert.Equal("product1", result[0].Name);
            Assert.Equal("www.dr.dk/a.jpg", result[0].ProductImage.Url);
            Assert.Equal("Red", result[0].ProductTag.ToArray()[0].Name);
        }
    }
}