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
            System.Console.WriteLine("Que servicio quiere eliminar?");
            string input = Console.ReadLine();
            int indice = this.Offers.FindIndex(item => item.Description.Equals(input));
            //System.Console.WriteLine(indice);
            if (indice != -1)
            {
                this.Offers.RemoveAt(indice);
                System.Console.WriteLine("");
            }
            else
            {
                System.Console.WriteLine("Error, servicio no encontrado");
            }
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
