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

        // Ver este metodo. Capaz que en vez de poner fecha "-", podriamos ver la manera de que se genere solo.
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
            Employee employee = new Employee ("Gonzalo", "Cañete", "gonza.cañete", "employee", "Montevideo","099701004","gonzalo@gmail.com");
            Employer employer = new Employer ("Cristiano", "Ronaldo", "CR7", "employer", "Manchester", "12345678", "CR7@gmail.com");
            Contract contract = new Contract("","18/11/2022","Jardineria", employer, employee);
            Assert.IsEmpty(contract.getInitDate(),"Exception");
            //Assert.IsNull(contract.getInitDate(),"Exception");
        }

        [Test]
        public void getDateTest()
        {
            const string initDate = "18/11/2022";
            const string finalDate = "18/11/2022";
            Employee employee = new Employee ("Gonzalo", "Cañete", "gonza.cañete", "employee", "Montevideo","099701004","gonzalo@gmail.com");
            Employer employer = new Employer ("Cristiano", "Ronaldo", "CR7", "employer", "Manchester", "12345678", "CR7@gmail.com");
            Contract contract = new Contract("18/11/2022","18/11/2022","Jardineria", employer, employee);

            // Compruebo que el metodo getInitDate me devuelva lo que espero
            Assert.AreEqual(initDate, contract.getInitDate());

            // Compruebo que el metodo getFinalDate me devuelva lo mismo
            Assert.AreEqual(finalDate, contract.getFinalDate());
        }

        [Test]
        public void setDatesTest()
        {
            const string initDate = "20/11/2022";
            const string finalDate = "25/11/2022";
            Employee employee = new Employee ("Gonzalo", "Cañete", "gonza.cañete", "employee", "Montevideo","099701004","gonzalo@gmail.com");
            Employer employer = new Employer ("Cristiano", "Ronaldo", "CR7", "employer", "Manchester", "12345678", "CR7@gmail.com");
            // Acá le ingrese la fecha inicial 18/11/2022 y la fecha final 18/11/2022
            Contract contract = new Contract("18/11/2022","18/11/2022","Jardineria", employer, employee);
            // Aplico el metodo set para cambiarlas a lo que seria mi expected
            contract.setInitDate("20/11/2022");
            contract.setFinalDate("25/11/2022");
            // Compruebo que con el get me devuelva lo que se seteo
            Assert.AreEqual(initDate, contract.getInitDate());
            Assert.AreEqual(finalDate, contract.getFinalDate());
            //System.Console.WriteLine(contract.getInitDate());
            //System.Console.WriteLine(contract.getFinalDate());
        }
    }
}