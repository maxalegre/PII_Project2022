using System.IO;

using NUnit.Framework;
namespace Library.Tests;
using Library;


    [TestFixture]
    public class QualificationTests
    {
        [SetUp]
        public void Setup()
        {   
        }
        
        [Test]
        public void AddingQualification()
        {
            // Employee empleado1= new Employee("Jorge", "Ventura", "Montevideo " ,"098212", "Jorge@tanto");
            
            // Employer empleador1= new Employer("Mateo", "Sosa", "Montevideo " ,"0353212", "Mate9o@tanto");

            Qualification calificacion1= new Qualification(11, "Muy buen trabajador" );
            
            Qualification calificacion2= new Qualification(5, "Muy mal jefe" );
            Qualification calificacion3= new Qualification(76, "Muy buwn jefe" );


            //int expectedValor= 5 ;
            //string expectedDescription= "Muy mal jefe" ;

            // empleado1.Qualify(calificacion2,empleador1);
            // empleado1.Qualify(calificacion3,empleador1);

            
           //int valorObtenido= empleador1.Reviews[0].Value;
           //string descripcionObtenida= empleador1.Reviews[0].Description;

           // Assert.AreEqual(expectedValor,valorObtenido);
            //Assert.AreEqual(expectedDescription,descripcionObtenida);


        }
    }

