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
    public class PageCountTest
    {
        int pageCountResult1, pageCountResult2, pageCountResult3, pageCountResultAll;
        
        [TestMethod]
        public void GenerateCategorySpecificProductCount()
        {
            ProductController countController = CategoryArrange();
            CategoryAct(countController);
            CategoryAssert();
        }

        private ProductController CategoryArrange()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
         {
            new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
            new Product { ProductID = 2, Name = "P2", Category = "Cat2" },
            new Product { ProductID = 3, Name = "P3", Category = "Cat1" },
            new Product { ProductID = 4, Name = "P4", Category = "Cat2" },
            new Product { ProductID = 5, Name = "P5", Category = "Cat3" }
         }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            return controller;
        }

        private void CategoryAct(ProductController controller)
        {
            pageCountResult1 = ((ProductsListViewModel)controller.List("Cat1").Model).PagingInfo.TotalItems;
            pageCountResult2 = ((ProductsListViewModel)controller.List("Cat2").Model).PagingInfo.TotalItems;
            pageCountResult3 = ((ProductsListViewModel)controller.List("Cat3").Model).PagingInfo.TotalItems;
            pageCountResultAll = ((ProductsListViewModel)controller.List(null).Model).PagingInfo.TotalItems;
        }

        private void CategoryAssert()
        {
            Assert.AreEqual(pageCountResult1, 2);
            Assert.AreEqual(pageCountResult2, 2);
            Assert.AreEqual(pageCountResult3, 1);
            Assert.AreEqual(pageCountResultAll, 5);
        }
    }
}
