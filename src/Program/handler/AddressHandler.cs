using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telegram.Bot.Types;

namespace Ucu.Poo.TelegramBot
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "dirección".
    /// </summary>
    public class AddressHandler : BaseHandler
    {
        public const string ADDRESS_PROMPT = "Vamos a buscar una dirección. Decime qué dirección querés buscar por favor";
        public const string INTERNAL_ERROR = "Error interno de configuración, no puedo buscar direcciones";
        public const string ADDRESS_NOT_FOUND = "No encuentro la dirección. Decime qué dirección querés buscar por favor";

        private Dictionary<long, AddressState> stateForUser = new Dictionary<long, AddressState>();

        /// <summary>
        /// El estado del comando para un usuario que envía un mensaje. Cuando se comienza a procesar el comando para un
        /// nuevo usuario se agrega a este diccionario y cuando se termina de procesar el comando se remueve.
        /// </summary>
        public IReadOnlyDictionary<long, AddressState> StateForUser
        {
            get
            {
                return this.stateForUser;
            }
        }

        // Un buscador de direcciones. Permite que la forma de encontrar una dirección se determine en tiempo de
        // ejecución: en el código final se asigna un objeto que use una API para buscar direcciones; y en los casos de
        // prueba se asigne un objeto que retorne un resultado que puede ser configurado desde el caso de prueba.
        private IAddressFinder finder;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AddressHandler"/>.
        /// </summary>
        /// <param name="next">Un buscador de direcciones.</param>
        /// <param name="next">El próximo "handler".</param>
        public AddressHandler(IAddressFinder finder, BaseHandler next)
            : base(new string[] { "dirección", "direccion" }, next)
        {
            this.finder = finder;
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
                this.stateForUser.Add(message.From.Id, AddressState.Start);
            }

            AddressState state = this.StateForUser[message.From.Id];

            if (state == AddressState.Start)
            {
                // En el estado Start le pide la dirección y pasa al estado AddressPrompt
                this.stateForUser[message.From.Id] = AddressState.AddressPrompt;
                response = ADDRESS_PROMPT;
            }
            else if ((state == AddressState.AddressPrompt) && (this.finder != null))
            {
                AddressData data = new AddressData();

                // En el estado AddressPrompt el mensaje recibido es la respuesta con la dirección
                data.Address = message.Text;
                data.Result = this.finder.GetLocation(data.Address);

                if (data.Result.Found)
                {
                    // Si encuentra la dirección pasa nuevamente al estado Initial
                    response = $"La dirección es en {data.Result.Latitude},{data.Result.Longitude}";
                    this.stateForUser.Remove(message.From.Id); // Equivalente a volver al estado inicial
                }
                else
                {
                    // Si no encuentra la dirección se la pide de nuevo y queda en el estado AddressPrompt
                    response = ADDRESS_NOT_FOUND;
                }
            }
            else if ((state == AddressState.AddressPrompt) && (this.finder == null))
            {
                // En el estado AddressPrompt si no hay un buscador de direcciones hay que responder que hubo un error
                // y volver al estado inicial.
                response = INTERNAL_ERROR;
                this.stateForUser.Remove(message.From.Id); // Equivalente a volver al estado inicial
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
        public enum AddressState
        {
            Start,
            AddressPrompt
        }

        /// <summary>
        /// Representa los datos que va obteniendo el comando AddressHandler en los diferentes estados.
        /// </summary>
        private class AddressData
        {
            /// <summary>
            /// La dirección que se ingresó en el estado AddressState.AddressPrompt.
            /// </summary>
            public string Address { get; set; }

            /// <summary>
            /// El resultado de la búsqueda de la dirección ingresada.
            /// </summary>
            public IAddressResult Result { get; set; }
        }
    }
}