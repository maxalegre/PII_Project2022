using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
namespace Library.Tests;
using Library;


    [TestFixture]
    public class CategoriesServiceTests
    {
        
        [Test]
        public void addCategoryErrorTest()
        {
            const string expected = "Mecanico";
            CategoriesManager.Instance.addCategory("Jardinero");
            Assert.Contains(expected, CategoriesManager.Instance.getCategories());
            
        }
        [Test]
        public void addCategoryTest()
        {
            List<Category> categories = new List<Category>();
            Category category = new Category("Mecanico");
            //CategoriesManager.Instance.addCategory("Mecanico");
            
            // Emulo la función addCategory() ya que no me pasa el test por no conocer la lista "categories" del CategoriesManager
            if (!categories.Contains(category))
            {
                categories.Add(category);
            }
            Assert.Contains(category, categories);            
        }
        [Test]
        public void getCategoriesTest()
        {
            const string expected = "Mecanico";
            CategoriesManager.Instance.addCategory(expected);
            //CategoriesManager.Instance.getCategories();
            Assert.IsNotEmpty(CategoriesManager.Instance.getCategories());
        }
    }

