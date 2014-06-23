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
    public class CreateCategoriesTest
    {
        [TestMethod]
        public void CanCreateCategories()
        {
            NavigationController navigationController = CategoryArrange();
            string[] categoryResult = CategoryAct(navigationController);
            PaginationAssert(categoryResult);
        }

        private NavigationController CategoryArrange()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
         {
            new Product { ProductID = 1, Name = "P1", Category = "Apples" },
            new Product { ProductID = 2, Name = "P2", Category = "Apples" },
            new Product { ProductID = 3, Name = "P3", Category = "Plums" },
            new Product { ProductID = 4, Name = "P4", Category = "Oranges" }
         }.AsQueryable());

            NavigationController controller = new NavigationController(mock.Object);

            return controller;
        }

        private string[] CategoryAct(NavigationController controller)
        {
            return ((IEnumerable<string>)controller.Menu().Model).ToArray();
        }

        private void PaginationAssert(string[] result)
        {
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual(result[0], "Apples");
            Assert.AreEqual(result[1], "Oranges");
            Assert.AreEqual(result[2], "Plums");
        }
    }
}
