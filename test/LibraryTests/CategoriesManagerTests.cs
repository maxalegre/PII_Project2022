using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
namespace Library.Tests;
using Library;


    [TestFixture]
    public class CategoriesServiceTests
    {
        [SetUp]
        public void Setup()
        {   
        }
        
        [Test]
        public void addCategoryTest()
        {
            for (int i = 1; i < 5; i++)
            {
                string category = $"{i}";
                CategoriesManager.Instance.addCategory(category);
            }
            List<Category> list = CategoriesManager.Instance.getCategories();
            int counter = 1;
            foreach (Category item in list)
            {
                string Expected = $"{counter}";
                Assert.AreEqual(Expected, item.Name);
                counter++;
            }
        }

        [Test]
        public void getCategoriesTest()
        {
            for (int i = 1; i < 5; i++)
            {
                string category = $"{i}";
                CategoriesManager.Instance.addCategory(category);
            }
            List<Category> list = CategoriesManager.Instance.getCategories();

            List<Category> Expected = new List<Category>(){
                new Category("1"), new Category("2"), new Category("3"), new Category("4"), new Category("5")
                };

            for (int i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(Expected.ElementAt(i).Name, list.ElementAt(i).Name);
            }
        }
    }
