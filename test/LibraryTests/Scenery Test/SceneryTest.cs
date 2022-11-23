using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Library
{
    [TestFixture]
    public class SceneryTest
    {
        [Test]
        public void addingOffersTest()
        {
            Employee employee = new Employee ("Gonzalo", "Cañete", "gonzalo.123", "Employee", "Montevideo", "099701004", "gonzlo@gmail.com");
            Employer employer = new Employer ("Cristiano", "Ronaldo", "CR7", "Employer", "Manchester", "12345678", "CR7@gmail.com");
            List<Offer> offers = new List<Offer>();
            //Offer offer = new Offer(employee,"Jardinero",452.45,"Jardinero");
            OffersManager.Instance.addOffer(employee,"Jardinero",452.45,"Jardinero");
            
            // Para contratar, estoy teniendo problemas con el readLine()
            //employer.searchOffers("Jardinero");
            //employer.hireEmployee(offers);
            //var offer = offers.ElementAt(int.Parse(System.Console.ReadLine())-1);
            
            Assert.IsNotEmpty(OffersManager.Instance.Offers);
        }

        [Test]
        public void addingCategoriesTest()
        {
            Employee employee = new Employee ("Gonzalo", "Cañete", "gonzalo.123", "Employee", "Montevideo", "099701004", "gonzlo@gmail.com");
            Employer employer = new Employer ("Cristiano", "Ronaldo", "CR7", "Employer", "Manchester", "12345678", "CR7@gmail.com");
            OffersManager.Instance.addOffer(employee,"Jardineria", 425.45, "Jardineria");
            Assert.IsNotEmpty(OffersManager.Instance.getOffersCategories("Jardineria"));
        }

        [Test]
        public void addingCategoriesTestError()
        {
            Employee employee = new Employee ("Gonzalo", "Cañete", "gonzalo.123", "Employee", "Montevideo", "099701004", "gonzlo@gmail.com");
            Employer employer = new Employer ("Cristiano", "Ronaldo", "CR7", "Employer", "Manchester", "12345678", "CR7@gmail.com");
            OffersManager.Instance.addOffer(employee, "Cocinero", 457.45, "Cocinero");
            Assert.IsEmpty(OffersManager.Instance.getOffersCategories("Cocinero"));
        }

        [Test]
        public void hiringEmployeeTest()
        {
            Employee employee = new Employee ("Gonzalo", "Cañete", "gonzalo.123", "Employee", "Montevideo", "099701004", "gonzlo@gmail.com");
            Employer employer = new Employer ("Cristiano", "Ronaldo", "CR7", "Employer", "Manchester", "12345678", "CR7@gmail.com");
            OffersManager.Instance.addOffer(employee, "Cocinero", 457.45, "Cocinero");
            //Offer expected = new Offer (employee,"Cocinero", 475.25, "Cocinero");
            employer.hireEmployee(OffersManager.Instance.Offers);
            var offer = 0;
            
            //OffersManager.Instance.Offers.IndexOf(expected);
            //OffersManager.Instance.getOffersCategories("Cocinero").RemoveAt(OffersManager.Instance.Offers.IndexOf(expected));
            
            // No puedo comprobar el metodo por el readLine()
            OffersManager.Instance.Offers.RemoveAt(offer);
            //employee.addOffer("Cocinero", 425.45, "Cocinero");
            
            // Me debería devolver passed porque se retiraria la oferta, pero por el readLine() no pasa el test 
            Assert.IsEmpty(OffersManager.Instance.Offers);

        }



    }
}