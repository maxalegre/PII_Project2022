using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telegram.Bot.Types;
using Library;
namespace Ucu.Poo.TelegramBot
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "dirección".
    /// </summary>
    public class CreateContractHandler : BaseHandler
    {
        public const string PRIMERAPREGUNTA = "¿A quien quieres contratar?";
        public const string INTERNAL_ERROR = "Error interno de configuración, no puedo buscar direcciones";
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

        // Un buscador de direcciones. Permite que la forma de encontrar una dirección se determine en tiempo de
        // ejecución: en el código final se asigna un objeto que use una API para buscar direcciones; y en los casos de
        // prueba se asigne un objeto que retorne un resultado que puede ser configurado desde el caso de prueba.

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AddressHandler"/>.
        /// </summary>
        /// <param name="next">Un buscador de direcciones.</param>
        /// <param name="next">El próximo "handler".</param>
        public CreateContractHandler(BaseHandler next):base(next)
        {
            this.Keywords = new string[] {"contract"};
        
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
            }

            State state = this.StateForUser[message.From.Id];

            if (state == State.Start)
            {
                // En el estado Start le pide la dirección y pasa al estado AddressPrompt
                this.stateForUser[message.From.Id] = State.PrimeraPregunta;
                response = PRIMERAPREGUNTA;
            }
            else if (state == State.PrimeraPregunta)
            {
                AddressData data = new AddressData();

                // En el estado AddressPrompt el mensaje recibido es la respuesta con la dirección
                data.UserID = message.Text.ToString(); 
                var user =UserManager.Instance.Users.Find(i => i.ID == data.UserID);
                if (user!=null)
                {
                    var me = UserManager.Instance.Users.Find(i => i.ID == message.From.Id.ToString());;
                    //ContractManager.Instance.createContracts("Hola", "sdasd","Jardineria",user,me);
                    // Si encuentra la dirección pasa nuevamente al estado Initial
                    response = $"Usuario {user.Name} contratado";
                    this.stateForUser.Remove(message.From.Id); // Equivalente a volver al estado inicial
                }
                else
                {
                    // Si no encuentra la dirección se la pide de nuevo y queda en el estado AddressPrompt
                    response = USERNOTFOUND;
                }
            }
            /*else if ((state == State.PrimeraPregunta) && (this.finder == null))
            {
                // En el estado AddressPrompt si no hay un buscador de direcciones hay que responder que hubo un error
                // y volver al estado inicial.
                response = INTERNAL_ERROR;
                this.stateForUser.Remove(message.From.Id); // Equivalente a volver al estado inicial
            }*/
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
            PrimeraPregunta
        }

        /// <summary>
        /// Representa los datos que va obteniendo el comando AddressHandler en los diferentes estados.
        /// </summary>
        private class AddressData
        {
            /// <summary>
            /// La dirección que se ingresó en el estado AddressState.AddressPrompt.
            /// </summary>
            public string UserID { get; set; }

            /// <summary>
            /// El resultado de la búsqueda de la dirección ingresada.
            /// </summary>
           // public IAddressResult Result { get; set; }
        }
    }
}