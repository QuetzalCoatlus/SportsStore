using System;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SportsStore.Domain.Entities;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class AddNewLineTest
    {
        Product product1;
        Product product2;

        Cart cart;

        CartLine[] cartLineResults;

        [TestMethod]
        public void CanAddNewLines()
        {
            NewLineArrange();
            NewLineAct();
            NewLineAssert();
        }

        public void NewLineArrange()
        {
            product1 = new Product { ProductID = 1, Name = "P1" };
            product2 = new Product { ProductID = 2, Name = "P2" };

            cart = new Cart();
        }

        public void NewLineAct()
        {
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);
            cartLineResults = cart.Lines.ToArray();
        }

        public void NewLineAssert()
        {
            Assert.AreEqual(cartLineResults.Length, 2);
            Assert.AreEqual(cartLineResults[0].Product, product1);
            Assert.AreEqual(cartLineResults[1].Product, product2);
        }
    }

    [TestClass]
    public class AddQuantityTest
    {
        Product product1;
        Product product2;

        Cart cart;

        CartLine[] cartLineResults;

        [TestMethod]
        public void CanAddQuantityForExistingLines()
        {
            AddQuantityArrange();
            AddQuantityAct();
            AddQuantityAssert();
        }

        public void AddQuantityArrange()
        {
            product1 = new Product { ProductID = 1, Name = "P1" };
            product2 = new Product { ProductID = 2, Name = "P2" };

            cart = new Cart();
        }

        public void AddQuantityAct()
        {
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);
            cart.AddItem(product1, 10);
            cartLineResults = cart.Lines.ToArray();
        }

        public void AddQuantityAssert()
        {
            Assert.AreEqual(cartLineResults.Length, 2);
            Assert.AreEqual(cartLineResults[0].Quantity, 11);
            Assert.AreEqual(cartLineResults[1].Quantity, 1);
        }
    }

    [TestClass]
    public class RemoveLineTest
    {
        Product product1;
        Product product2;
        Product product3;

        Cart cart;

        [TestMethod]
        public void CanAddQuantityForExistingLines()
        {
            RemoveLineArrange();
            RemoveLineAct();
            RemoveLineAssert();
        }

        public void RemoveLineArrange()
        {
            product1 = new Product { ProductID = 1, Name = "P1" };
            product2 = new Product { ProductID = 2, Name = "P2" };
            product3 = new Product { ProductID = 3, Name = "P3" };

            cart = new Cart();
        }

        public void RemoveLineAct()
        {
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 3);
            cart.AddItem(product3, 5);
            cart.AddItem(product2, 1);

            cart.RemoveLine(product2);
        }

        public void RemoveLineAssert()
        {
            Assert.AreEqual(cart.Lines.Where(c => c.Product == product2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }
    }

    [TestClass]
    public class CalculateCartTotalTest
    {
        Product product1;
        Product product2;

        Cart cart;

        decimal total;

        [TestMethod]
        public void CanCalculateCartTotal()
        {
            CalculateCartTotalArrange();
            CalculateCartTotalAct();
            CalculateCartTotalAssert();
        }

        public void CalculateCartTotalArrange()
        {
            product1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            product2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            cart = new Cart();
        }

        public void CalculateCartTotalAct()
        {
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);
            cart.AddItem(product1, 3);

            total = cart.ComputeTotalValue();
        }

        public void CalculateCartTotalAssert()
        {
            Assert.AreEqual(total, 450M);
        }
    }

    [TestClass]
    public class ClearContentsTest
    {
        Product product1;
        Product product2;

        Cart cart;

        decimal total;

        [TestMethod]
        public void CanClearContents()
        {
            ClearContentsArrange();
            ClearContentsAct();
            ClearContentsAssert();
        }

        public void ClearContentsArrange()
        {
            product1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            product2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            cart = new Cart();
        }

        public void ClearContentsAct()
        {
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);

            cart.Clear();
        }

        public void ClearContentsAssert()
        {
            Assert.AreEqual(cart.Lines.Count(), 0);
        }
    }
}
