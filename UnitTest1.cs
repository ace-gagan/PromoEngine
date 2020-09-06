using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromoEngine;
using System.Linq;

namespace UnitTest_PromoEngine
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<Product> _products = new List<Product>() { new Product("A"), new Product("A"), new Product("B"), new Product("B"), new Product("C"), new Product("D") };
            CalculatePrice _cp = new CalculatePrice(_products);
            Dictionary<string, float> _allDisc = _cp.GetDiscount();
            float _totalCost = _products.Sum(item => item._price);

            float _result = _totalCost - _allDisc.Values.Max();

            Assert.AreEqual(180, _result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            List<Product> _products = new List<Product>() { new Product("A"), new Product("A"), new Product("A"), new Product("A"),
                                                            new Product("A"), new Product("A"), new Product("B") };

            CalculatePrice _cp = new CalculatePrice(_products);
            Dictionary<string, float> _allDisc = _cp.GetDiscount();
            float _totalCost = _products.Sum(item => item._price);

            float _result = _allDisc.Values.Max();

            Assert.AreEqual(40, _result);

        }

        [TestMethod]
        public void TestMethod3()
        {
            List<Product> _products = new List<Product>() { new Product("A"), new Product("A"), new Product("D"), new Product("B"), new Product("B") };
            CalculatePrice _cp = new CalculatePrice(_products);
            Dictionary<string, float> _allDisc = _cp.GetDiscount();

            float _result = _allDisc.Values.Max();

            Assert.AreEqual(15, _result);
        }

        [TestMethod]
        public void TestMethod4()
        {
            List<Product> _products = new List<Product>() { new Product("A"), new Product("A"), new Product("D"), new Product("B"), new Product("B"),
                                                            new Product("A"), new Product("A"), new Product("C"), new Product("C"), new Product("C"),
                                                            new Product("C"), new Product("C"), new Product("C") };

            CalculatePrice _cp = new CalculatePrice(_products);
            Dictionary<string, float> _allDisc = _cp.GetDiscount();
            float _totalCost = _products.Sum(item => item._price);

            float _result = _totalCost - _allDisc.Values.Max();

            Assert.AreEqual(375, _result);

        }
    }
}
