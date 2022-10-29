using System.Collections.Generic;

namespace Library
{

    public class Offers
    {
        public string Description;
        public double Remuneration;
        public List<Offers> listOffers = new List<Offers>();

        public Offers (string Description , double Remuneration)
        {
            this.Description = Description;
            this.Remuneration = Remuneration;
        }
        public void addOffers(Offers offers)
        {
            listOffers.Add(offers);
        }

        public void GetOffers()
        {
            foreach (Offers a in this.listOffers)
            {
                System.Console.WriteLine(a.Description);
            }
            
        }
    }
}

