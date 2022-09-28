using System;
using Library;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Categories cat = new Categories();

            Offers Offer1 = new Offers("Test", 100);
            Offers Offer2 = new Offers("Test2", 500);
            ServiceOffer Services = new ServiceOffer();
            Services.Offers.Add(Offer1);
            Services.Offers.Add(Offer2);

            Admin adm = new Admin();
            adm.addCategory(cat, "Translator");
            adm.addCategory(cat, "Data Analyst");
            adm.addCategory(cat, "Fitness Instructor");

            cat.getCategories();
            cat.removeCategory();
            cat.getCategories();
        }
    }
}
