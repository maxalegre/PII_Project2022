using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using Library;

namespace Library.Tests;


[TestFixture]
public class UserTests
{
    [SetUp]
    public void Setup()
    {   
    }
    
    [Test]
    public void changeNumberTest()
    {
        UserManager.Instance.CreateUser("Franco", "San Martin", "12345", "employer", "Uruguay", "123456789", "Franco@email.com");
        IUser user = UserManager.Instance.Login("12345");
        string Expected = "987654321";
        user.changeNumber(Expected);
        Assert.AreEqual(Expected, ((Employer)user).contactNumber);
    }

    [Test]
    public void changeEmailTest()
    {
        UserManager.Instance.CreateUser("Franco", "San Martin", "12345", "employer", "Uruguay", "123456789", "Franco@email.com");
        IUser user = UserManager.Instance.Login("12345");
        string Expected = "Franco@gmail.com";
        user.changeEmail(Expected);
        Assert.AreEqual(Expected, ((Employer)user).contactEmail);
    }

    [Test]
    public void getContractsTest()
    {
        UserManager.Instance.CreateUser("Franco", "San Martin", "12345", "employer", "Uruguay", "123456789", "Franco@email.com");
        UserManager.Instance.CreateUser("Roberto", "Lopez", "777", "employee", "Uruguay", "456456456", "RobertitoLopez@gmail.com");
        IUser Franco = UserManager.Instance.Login("12345");
        IUser Roberto = UserManager.Instance.Login("777");
        
        ContractManager.Instance.createContracts("19/11/2022", "1/12/2022", "Plomero", ((Employee)Roberto), ((Employer)Franco));
        
        int ExpectedCount = 1;
        Assert.AreEqual(ExpectedCount, ((Employee)Roberto).getContracts().Count);
        Assert.AreEqual(ExpectedCount, ((Employer)Franco).getContracts().Count);

        string ExpectedInitDate = "19/11/2022";
        string ExpectedFinalDate = "1/12/2022";
        string ExpectedJob = "Plomero";

        Assert.AreEqual(ExpectedInitDate, ((Employer)Franco).getContracts()[0].getInitDate());
        Assert.AreEqual(ExpectedInitDate, ((Employee)Roberto).getContracts()[0].getInitDate());
        Assert.AreEqual(ExpectedFinalDate, ((Employer)Franco).getContracts()[0].getFinalDate());
        Assert.AreEqual(ExpectedFinalDate, ((Employee)Roberto).getContracts()[0].getFinalDate());
        

        // Error al hacer estos test, en .employer y .employee hay "null".
        Assert.AreEqual(((Employer)Franco), ((Employer)Franco).getContracts()[0].employer);
        Assert.AreEqual(((Employee)Roberto), ((Employer)Franco).getContracts()[0].employee);

        Assert.AreEqual(((Employer)Franco), ((Employee)Roberto).getContracts()[0].employer);
        Assert.AreEqual(((Employee)Roberto), ((Employee)Roberto).getContracts()[0].employee);

    }
}
