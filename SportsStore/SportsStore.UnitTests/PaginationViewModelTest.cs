using Microsoft.VisualStudio.TestTools.UnitTesting;
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
   public class PaginationViewModelTest
   {
      Mock<IProductRepository> mock;
      ProductController controller;

      [TestMethod]
      public void CanSendPaginationViewModelTest()
      {
      }

      private void PaginationViewModelArrange()
      {
         mock = new Mock<IProductRepository>();
         mock.Setup(m => m.Products).Returns(new Product[]
         {
            new Product { ProductID = 1, Name = "P1" },
            new Product { ProductID = 2, Name = "P2" },
            new Product { ProductID = 3, Name = "P3" },
            new Product { ProductID = 4, Name = "P4" },
            new Product { ProductID = 5, Name = "P5" }
         }.AsQueryable());

         controller = new ProductController(mock.Object);
         controller.pageSize = 3;
      }

      private ProductsListViewModel PageLinkAct()
      {
         return (ProductsListViewModel)controller.List(2).Model;
      }

      private void PageLinkAssert(ProductsListViewModel result)
      {
         PagingInfo pageInfo = result.PagingInfo;
         Assert.AreEqual(pageInfo.CurrentPage, 2);
         Assert.AreEqual(pageInfo.ItemsPerPage, 3);
         Assert.AreEqual(pageInfo.TotalItems, 5);
         Assert.AreEqual(pageInfo.TotalPages, 2);
      }
   }
}
