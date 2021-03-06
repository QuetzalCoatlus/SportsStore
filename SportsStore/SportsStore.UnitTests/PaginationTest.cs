﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

using Moq;

using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
   [TestClass]
   public class PaginationTest
   {
      [TestMethod]
      public void CanPaginate()
      {
         ProductController paginationController = PaginationArrange();
         ProductsListViewModel paginationResult = PaginationAct(paginationController);
         PaginationAssert(paginationResult);
      }

      private ProductController PaginationArrange()
      {
         Mock<IProductRepository> mock = new Mock<IProductRepository>();
         mock.Setup(m => m.Products).Returns(new Product[]
         {
            new Product { ProductID = 1, Name = "P1" },
            new Product { ProductID = 2, Name = "P2" },
            new Product { ProductID = 3, Name = "P3" },
            new Product { ProductID = 4, Name = "P4" },
            new Product { ProductID = 5, Name = "P5" }
         }.AsQueryable());

         ProductController controller = new ProductController(mock.Object);
         controller.pageSize = 3;

         return controller;
      }

      private ProductsListViewModel PaginationAct(ProductController controller)
      {
         return (ProductsListViewModel)controller.List(null, 2).Model;
      }

      private void PaginationAssert(ProductsListViewModel result)
      {
         Product[] products = result.Products.ToArray();
         Assert.IsTrue(products.Length == 2);
         Assert.AreEqual(products[0].Name, "P4");
         Assert.AreEqual(products[1].Name, "P5");
      }
   }
}
