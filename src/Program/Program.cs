using System;
using Library;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine(UserManager.Instance);
            // Categories cat = new Categories();

            // Offers Offer1 = new Offers("Arquitecto", 100);
            // Offers Offer2 = new Offers("Ingeniero", 500);
            // Offers Offer3 = new Offers("Jardinero", 1000);
            // ServiceOffer Services = new ServiceOffer();
            // Services.addOffer(Offer1);
            // Services.addOffer(Offer2);
            // Services.addOffer(Offer3);

            // Admin adm = new Admin();
            // adm.addCategory(cat, "Translator");
            // adm.addCategory(cat, "Data Analyst");
            // adm.addCategory(cat, "Fitness Instructor");

            /*
            cat.getCategories();
            cat.removeCategory();
            cat.getCategories();
            */

            /*
            Services.getServices();
            Services.removeService();
            Services.getServices();
            */
            /*
            ContractManager emp1 = new ContractManager ();
            emp1.createEmployeeContracts("27/10/2022","-","Translator","asd");
            emp1.GetEmployeeContracts();
            */
            /*
            double suma = 0;
            double promedio = 0;
            int qualification.Length = 0;
            Console.WriteLine("Indique el ID del usuario del que quisiera conocer la calificación promedio");
            char persona = Console.ReadLine()[0];
            for (int i=0; i < qualification.Length; i++);
            {
                if(persona==a[i].getId())
                {
                    suma = suma+ a[i].getQualification();
                    cantidadCalificaciones++;
                }
            }
            promedio = suma / qualification.Length;
            Console.WriteLine ("La calificaciones promedio de la persona consultada es de:" + promedio);
            */
        }    
    }
}
