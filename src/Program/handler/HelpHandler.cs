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
    public class HelpHandler : BaseHandler
    {
        

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
        public HelpHandler(BaseHandler next):base(next)
        {
            this.Keywords = new string[] {"help"};
        
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
                response = $"Comandos utiles:\nregister: Para crear un nuevo usuario.\noffers: Para crear o filtrar ofertas.\nqualify: Para calificar a un usuario.\ncontract: Para contratar un empleado.";
                this.stateForUser.Remove(message.From.Id);
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
            
        }

        /// <summary>
        /// Representa los datos que va obteniendo el comando AddressHandler en los diferentes estados.
        /// </summary>
        private class UserData
        {
            
            public string UserId { get; set; }
            
            /// <summary>
            /// El resultado de la búsqueda de la dirección ingresada.
            /// </summary>
           // public IAddressResult Result { get; set; }
        }
    }
}