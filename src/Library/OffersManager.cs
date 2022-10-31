using System;
using System.Collections.Generic;
using System.Linq;
namespace Library
{
    public class OffersManager
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


        public OffersManager(){
        }
        public void addOffer(Employee employee,string Description , double Remuneration, string category) {
            Offer offer = new Offer(employee,Description,Remuneration,category);
            this.Offers.Add(offer); 
            CategoriesManager categoriesManager= new CategoriesManager();
            categoriesManager.addCategory(category);
        }

        public List<Offer> getoOffersByCategory(string category) {
            
            List<Offer> offersByCategory= new List<Offer>();
            
            foreach(Offer offer in this.Offers)
            {   
                if(offer.Category== category)
                {
                     offersByCategory.Add(offer);
                }

            }
            return offersByCategory;
        }
        

        public  List<Offer> sortOffersByReputation()
        {
            List<Offer> offersByReputation= new List<Offer>();

            List<Offer> SortedList = Offers.OrderBy(o=>o.employee.Reviews).ToList();
            return SortedList;

        }
    }}
        

       




