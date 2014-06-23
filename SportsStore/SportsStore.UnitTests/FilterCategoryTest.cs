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
    public class FilterCategoryTest
    {
        [TestMethod]
        public void CanFilterProductsOnCategory()
        {
            ProductController categoryController = CategoryArrange();
            Product[] categoryResult = CategoryAct(categoryController);
            CategoryAssert(categoryResult);
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

        private Product[] CategoryAct(ProductController controller)
        {
            return ((ProductsListViewModel)controller.List("Cat2", 1).Model).Products.ToArray(); ;
        }

        private void CategoryAssert(Product[] result)
        {
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");
        }
    }
}
