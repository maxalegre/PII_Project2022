using NUnit.Framework;

namespace Library.Test
{
    [TestFixture]
    public class ContractTests
    {
        [Test]
        public void createContracts()
        {
            // Creo employee para probar la creación del contrato
            Employee employee = new Employee ("Gonzalo", "Cañete", "gonza.cañete", "employee", "Montevideo","099701004","gonzalo@gmail.com");
            // Creo employer
            Employer employer = new Employer ("Cristiano", "Ronaldo", "CR7", "employer", "Manchester", "12345678", "CR7@gmail.com");
            // Test de creación de contrato
            Contract contract = new Contract ("18/11/2022", "-","Jardinería", employer, employee);
        }

        [Test]
        public void isValidTest()
        {
            const string expected = "-";
            Employee employee = new Employee ("Gonzalo", "Cañete", "gonza.cañete", "employee", "Montevideo","099701004","gonzalo@gmail.com");
            Employer employer = new Employer ("Cristiano", "Ronaldo", "CR7", "employer", "Manchester", "12345678", "CR7@gmail.com");
            Contract contract = new Contract("18/11/2022", "-","Jardineria", employer, employee);
            Assert.AreEqual(expected, contract.getFinalDate());
        }

        [Test]
        public void isValidErrorTest()
        {
            const string expected = "-";
            Employee employee = new Employee ("Gonzalo", "Cañete", "gonza.cañete", "employee", "Montevideo","099701004","gonzalo@gmail.com");
            Employer employer = new Employer ("Cristiano", "Ronaldo", "CR7", "employer", "Manchester", "12345678", "CR7@gmail.com");
            Contract contract = new Contract("18/11/2022", "18/11/2022","Jardineria", employer, employee);
            Assert.AreEqual(expected, contract.getFinalDate());
        }

        [Test]
        public void ExceptionTest()
        {
            const string expected = "18/11/2022";
            Employee employee = new Employee ("Gonzalo", "Cañete", "gonza.cañete", "employee", "Montevideo","099701004","gonzalo@gmail.com");
            Employer employer = new Employer ("Cristiano", "Ronaldo", "CR7", "employer", "Manchester", "12345678", "CR7@gmail.com");
            Contract contract = new Contract("","18/11/2022","Jardineria", employer, employee);
            Assert.IsEmpty(contract.getInitDate(),"Exception");
            //Assert.IsNull(contract.getInitDate(),"Exception");
        }
    }
}