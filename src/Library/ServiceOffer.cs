using System;
using System.Collections.Generic;

namespace Library
{
    public class ServiceOffer
    {
        public List<Offers> Offers = new List<Offers>();

        public void getServices() {
            foreach (Offers item in this.Offers)
            {
                System.Console.WriteLine(item.Description);
            }
        }

        public void removeService() {
            System.Console.WriteLine("Que servicio quiere eliminar? : ");
            string a = Console.ReadLine();
            int indice = this.Offers.IndexOf();
            this.Offers.RemoveAt(indice);
        }

/*
        public static List<Offers> SortByLocation()
        {   
            //Cuando haya una lista de offertas, se creara otra, donde las mismas esten ordenadas por
            //distancia entre el trabajador y empleado.
        }

        public static List<Offers> SortByReputation()
        {
            //Cuando haya una lista de ofertas, se hara otra lista con las ofertas ordenadas por
            //reputacion del trabajador.
        }
*/
    }

}
