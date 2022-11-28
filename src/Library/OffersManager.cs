using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace Library
{
    public sealed class OffersManager
    {
    private static OffersManager instance;

    public static OffersManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new OffersManager();
            }

            return instance;
        }
    }
        public List<Offer> Offers = new List<Offer>();


        private OffersManager(){
        }
        public void addOffer(Employee employee,string Description , double Remuneration, string category) 
        {
            Offer offer = new Offer(employee,Description,Remuneration,category);
            this.Offers.Add(offer); 
            //CategoriesManager categoriesManager= new CategoriesManager();
            CategoriesManager.Instance.addCategory(category);
        }

        // Metodo utilizado por admin para remover ofertas
        public void removeOffer() {
            PrintOffers();
            System.Console.WriteLine("\nIndique el numero de la oferta a eliminar: ");
            this.Offers.RemoveAt(int.Parse(System.Console.ReadLine())-1);
        }

        // Metodo utilizado al contratar un servicio.
        public void removeOffer(Offer offer) {
            Offers.Remove(offer);
        }

        public void PrintOffers()
        {
            int count = 1;
            foreach (Offer item in this.Offers)
            {
                System.Console.WriteLine($"{count} - {item.employee.Name} => {item.Description}\n");
                count++;
            }
        }
        
        public  List<Offer> sortOffersByReputation()
        {
            List<Offer> offersByReputation= new List<Offer>();

            List<Offer> SortedList = Offers.OrderBy(o=>o.employee.Reviews).ToList();
            return SortedList;

        }

        // Este metodo lo hice para devolver una lista. Funciona igual al getOffersByCategory, pero retorna una lista para 
        // usarla en Employer
        public List<Offer> getOffersCategories(string category)
        {
            List<Offer> categoriesOffer = new List<Offer>();
            foreach (Offer offer in this.Offers)
            {
                if (offer.Category.Equals(category))
                {
                    categoriesOffer.Add(offer);
                }
            }
            return categoriesOffer;
        }
        
        
    }}
        

       




