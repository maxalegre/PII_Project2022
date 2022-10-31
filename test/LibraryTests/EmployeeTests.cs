using NUnit.Framework;
namespace Library.Tests;
using Library;


    [TestFixture]
    public class EmployeeTests
    {
        [SetUp]
        public void Setup()
        {   
        }
        
        [Test]
        public void registerEmployee()
        {
         Employee empleado= new Employee("Jorge", "Ventura","5012923","Empleado", "Montevideo " ,"098212", "Jorge@tanto");  
         empleado.addOffer("Cualquier cosa", 2312.21,"Jardineria");

         Employer employer= new Employer("Jose", "Ubiedo","347223","Empleador","Montevideo","0992323","josejose@gmail");
         employer.getoOffersByCategory("Jardineria");
        }

    }