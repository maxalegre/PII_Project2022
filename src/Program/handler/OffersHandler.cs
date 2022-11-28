
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ucu.Poo.Locations.Client;
using Telegram.Bot.Types;
using Library;
namespace Ucu.Poo.TelegramBot
{
    /// <summary>

    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "dirección".
    /// </summary>
    public class OffersHandler : BaseHandler
    {
        public const string PRIMERAPREGUNTA = "Agregar oferta: Ingrese 0\nFiltrar ofertas: Ingrese 1";
        public const string FILTRO = "¿Por cual caracteristica desea filtrar?";
        public const string CATEGORIAPREGUNTA = "¿Por cual caracteristica desea filtrar?";

        public const string DESCRIPCIONOFERTA = "Agregue una descripcion a la oferta";
        public const string REMUNERACIONOFERTA = "Agregue una remuneracion a la oferta";
        public const string CATEGORIAOFERTA = "Agregue una categoria a la cual ubicar la oferta";
        public const string USERNOTFOUND = "Usuario no encontrado";

        private Dictionary<long, State> stateForUser = new Dictionary<long, State>();

        /// <summary>
        /// El estado del comando para un usuario que envía un mensaje. Cuando se comienza a procesar el comando para un
        /// nuevo usuario se agrega a este diccionario y cuando se termina de procesar el comando se remueve.
        /// </summary>
        public IReadOnlyDictionary<long, State> StateForUser
        {
            get
            {
                return this.stateForUser;
            }
        }
        private Dictionary<long, UserData> Data = new Dictionary<long, UserData>();

        // Un buscador de direcciones. Permite que la forma de encontrar una dirección se determine en tiempo de
        // ejecución: en el código final se asigna un objeto que use una API para buscar direcciones; y en los casos de
        // prueba se asigne un objeto que retorne un resultado que puede ser configurado desde el caso de prueba.

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AddressHandler"/>.
        /// </summary>
        /// <param name="next">Un buscador de direcciones.</param>
        /// <param name="next">El próximo "handler".</param>
        public OffersHandler(BaseHandler next):base(next)
        {
            this.Keywords = new string[] {"offers"};
        
        }

        /// <summary>
        /// <>
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected override bool CanHandle(Message message)
        {
            if (message == null || message.From == null)
            {
                throw new ArgumentException("No hay mensaje o no hay quién mande el mensaje");
            }

            if (this.StateForUser.ContainsKey(message.From.Id))
            {
                return true;
            }
            else
            {
                return base.CanHandle(message);
            }
        }

        /// <summary>
        /// Procesa todos los mensajes y retorna true siempre.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado indicando que el mensaje no pudo se procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override void InternalHandle(Message message, out string response)
        {
            if (message == null || message.From == null)
            {
                throw new ArgumentException("No hay mensaje o no hay quién mande el mensaje");
            }

            // Si no se recibió un mensaje antes de este usuario, entonces este es el primer mensaje y el estado del
            // comando es el estado inicial.
            if (!this.stateForUser.ContainsKey(message.From.Id))
            {
                this.stateForUser.Add(message.From.Id, State.Start);
                this.Data.Add(message.From.Id, new UserData());

            }

            State state = this.StateForUser[message.From.Id];

            if (state == State.Start)
            {
                // En el estado Start le pide la dirección y pasa al estado AddressPrompt
                this.stateForUser[message.From.Id] = State.PrimeraPregunta;
                response = PRIMERAPREGUNTA;
            }
            else if(state== State.PrimeraPregunta)
            {
                this.Data[message.From.Id].PrimeraPregunta= message.Text.ToString();
                if(this.Data[message.From.Id].PrimeraPregunta== "0" & UserManager.Instance.Users.Find(i => i.ID == message.From.Id.ToString()) is Employer )
                {
                    this.Data[message.From.Id].UserEmployer= UserManager.Instance.Users.Find(i => i.ID == message.From.Id.ToString());
                    response= FILTRO;
                    this.stateForUser[message.From.Id] = State.Filtro;
                }
                else if(this.Data[message.From.Id].PrimeraPregunta== "1" & UserManager.Instance.Users.Find(i => i.ID == message.From.Id.ToString()) is Employee )
                {
                    response= DESCRIPCIONOFERTA;
                    this.stateForUser[message.From.Id] = State.DescripcionOferta;
                }
                else
                {
                    response= "La accion no se pudo realizar. Intente nuevamente";
                }
            }
            else if (state == State.Filtro)
            {

                // En el estado AddressPrompt el mensaje recibido es la respuesta con la dirección
                var dato = this.Data[message.From.Id].Filter = message.Text.ToString();
                if (dato!=null & dato.ToLower()=="category")
                {                
                    response= CATEGORIAPREGUNTA; 
                    this.stateForUser[message.From.Id]= State.Categoria;
                    
                }
                else if (dato!=null & dato.ToLower()=="reputation")
                {                
                    response= caseReputation(); 
                    this.stateForUser.Remove(message.From.Id);
                    this.Data.Remove(message.From.Id);

                }
                else if(dato!=null & dato.ToLower()=="ubication")
                {
                    response= caseUbication(this.Data[message.From.Id].UserEmployer.Location,message.From.Id.ToString()); 
                    this.stateForUser.Remove(message.From.Id);
                    this.Data.Remove(message.From.Id);
                }
                else
                {
                    // Si no encuentra la dirección se la pide de nuevo y queda en el estado AddressPrompt
                    response = "Modo de filtrado incorrecto. Intente de nuevo";
                }
            }
            else if(state == State.Categoria)
            {
                response = caseCategory(message.Text.ToString());
                this.stateForUser.Remove(message.From.Id);
                this.Data.Remove(message.From.Id);
            }
            else if(state== State.DescripcionOferta)
            {
                this.Data[message.From.Id].OfferDescription= message.Text.ToString();
                this.stateForUser[message.From.Id] = State.RemuneracionOferta;
                response= REMUNERACIONOFERTA;
            }
            else if(state== State.RemuneracionOferta)
            {
                this.Data[message.From.Id].OfferRemuneration= double.Parse(message.Text.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                this.stateForUser[message.From.Id] = State.CategoriaOferta;
                response= CATEGORIAOFERTA;
            }
            else if(state== State.CategoriaOferta)
            {
                this.Data[message.From.Id].OfferCategory= message.Text.ToString();
                var me = UserManager.Instance.Users.Find(i => i.ID == message.From.Id.ToString());
                OffersManager.Instance.addOffer( ((Employee)me),this.Data[message.From.Id].OfferDescription,this.Data[message.From.Id].OfferRemuneration ,this.Data[message.From.Id].OfferCategory );
                this.stateForUser.Remove(message.From.Id);
                this.Data.Remove(message.From.Id);
                response= "Oferta creada correctamente";
            }
            else
            {
              response = string.Empty;
            }
            
        }
        /// <summary>
        /// Retorna este "handler" al estado inicial.
        /// </summary>
        protected override void InternalCancel(Message message)
        {
            if (message != null && message.From != null && this.StateForUser.ContainsKey(message.From.Id))
            {
                this.stateForUser.Remove(message.From.Id);
            }
        }

        /// <summary>
        /// Indica los diferentes estados que puede tener el comando AddressHandler.
        /// - Start: El estado inicial del comando. En este estado el comando pide una dirección de origen y pasa al
        /// siguiente estado.
        /// - AddressPrompt: Luego de pedir la dirección. En este estado el comando obtiene las coordenadas de la
        /// dirección y vuelve al estado Start.
        /// </summary>
        public enum State
        {
            Start,
            PrimeraPregunta,
            Filtro,
            Categoria,
            DescripcionOferta,
            RemuneracionOferta,
            CategoriaOferta,

        }

        /// <summary>
        /// Representa los datos que va obteniendo el comando AddressHandler en los diferentes estados.
        /// </summary>
        private class UserData
        {
            /// <summary>
            /// La dirección que se ingresó en el estado AddressState.AddressPrompt.
            /// </summary>
            public string PrimeraPregunta { get; set; }
            public string UserID { get; set; }
            public IUser UserEmployer { get; set; }
            public string Filter { get; set; }
            public string OfferDescription { get; set; }
            public double OfferRemuneration { get; set; }
            public string OfferCategory { get; set; }


            /// <summary>
            /// El resultado de la búsqueda de la dirección ingresada.
            /// </summary>
           // public IAddressResult Result { get; set; }


        }
        public string caseCategory(string category)
        {
            var list = OffersManager.Instance.getOffersCategories(category);

            var concString = "";
            foreach (Offer offer in list)
            {
                concString += $"Name: {offer.employee.Name} | ID: {offer.employee.ID} | Remuneration: {offer.Remuneration}\n";
            }
            return concString;
        }
        public string caseUbication(string ubication, string id)
        {
            LocationApiClient client = new LocationApiClient();
            DistanceCalculator calculador = new DistanceCalculator(client);
            IDistanceResult distance;
            var sortedOffers = "";
            foreach(Offer offer in OffersManager.Instance.Offers)
            {
                distance= calculador.CalculateDistance(this.Data[int.Parse(id)].UserEmployer.Location, offer.employee.Location);
                if (distance.Time<= 90)
                {
                    sortedOffers+= $"Name: {offer.employee.Name} | ID: {offer.employee.ID} | Category: {offer.Category}\n";
                }
            }
            return sortedOffers;                      
        }
        public string caseReputation()
        {
            var list = OffersManager.Instance.sortOffersByReputation();
            var concString = "";
            foreach (Offer offer in list)
            {
                concString += $"Name: {offer.employee.Name} | ID: {offer.employee.ID} | Category: {offer.Category}\n";
            }
            return concString;

        }
    }
}