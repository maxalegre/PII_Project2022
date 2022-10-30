using System;
using System.Collections.Generic;
using System.Linq;
namespace Library
{
    public class OffersManager
    {
        public List<Offer> Offers = new List<Offer>();


        public OffersManager(){
        }
        public void addOffer(Employee employee,string Description , double Remuneration, string category) {
            Offer offer = new Offer(employee,Description,Remuneration,category);
            this.Offers.Add(offer); 
            CategoriesService categoriesService= new CategoriesService();
            categoriesService.addCategory(category);
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
        
        //Este metodo deberia estar en la clase Admin, pero la logica del mismo esta funcionando.
        //En un futuro, hay que moverlo.
        

        public  List<Offer> sortOffersByReputation()
        {
            List<Offer> offersByReputation= new List<Offer>();

            List<Offer> SortedList = Offers.OrderBy(o=>o.employee.Reviews).ToList();
            return SortedList;

        }
    }}
        /*public static List<Offers> SortByLocation()
        {   
            //Cuando haya una lista de offertas, se creara otra, donde las mismas esten ordenadas por
            //distancia entre el trabajador y empleado.
        }*/

       




