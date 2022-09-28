using System;
using Library;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Categories cat = new Categories();

            Offers Offer1 = new Offers("Arquitecto", 100);
            Offers Offer2 = new Offers("Ingeniero", 500);
            Offers Offer3 = new Offers("Jardinero", 1000);
            ServiceOffer Services = new ServiceOffer();
            Services.Offers.Add(Offer1);
            Services.Offers.Add(Offer2);
            Services.Offers.Add(Offer3);

            Admin adm = new Admin();
            adm.addCategory(cat, "Translator");
            adm.addCategory(cat, "Data Analyst");
            adm.addCategory(cat, "Fitness Instructor");

            /*
            cat.getCategories();
            cat.removeCategory();
            cat.getCategories();
            */
            Services.getServices();
            Services.removeService();
            Services.getServices();
        }
    }
}
