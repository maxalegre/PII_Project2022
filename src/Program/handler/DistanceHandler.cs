using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telegram.Bot.Types;

namespace Ucu.Poo.TelegramBot
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "distancia".
    /// </summary>
    public class DistanceHandler : BaseHandler
    {
        public const string FROM_PROMPT = "Vamos a calcular una distancia. Decime la dirección de origen por favor";
        public const string TO_PROMPT = "Ahora decime la dirección de destino por favor";
        public const string ADDRESS_NOT_FOUND = "No encuentro alguna de las direcciones. Decime la dirección de origen por favor";
        public const string INTERNAL_ERROR = "Error interno de configuración, no puedo calcular distancias";

        // Un calculador de distancias entre dos direcciones. Permite que la forma de calcular distancias se determine en
        // tiempo de ejecución: en el código final se asigna un objeto que use una API para buscar distancias; y en los
        // casos de prueba se asigne un objeto que retorne un resultado que puede ser configurado desde el caso de prueba.
        private IDistanceCalculator calculator;

        private Dictionary<long, DistanceState> stateForUser = new Dictionary<long, DistanceState>();

        /// <summary>
        /// El estado del comando para un usuario que envía un mensaje. Cuando se comienza a procesar el comando para un
        /// nuevo usuario se agrega a este diccionario y cuando se termina de procesar el comando se remueve.
        /// </summary>
        public IReadOnlyDictionary<long, DistanceState> StateForUser
        {
            get
            {
                return this.stateForUser;
            }
        }

        /// <summary>
        /// Los datos  para un usuario que va obteniendo el comando en los diferentes estados.
        /// </summary>
        private Dictionary<long, DistanceData> Data = new Dictionary<long, DistanceData>();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DistanceHandler"/>.
        /// </summary>
        /// <param name="calculator">Un calculador de distancias.</param>
        /// <param name="next">El próximo "handler".</param>
        public DistanceHandler(IDistanceCalculator calculator, DistanceHandler next)
            : base(new string[] { "distancia" }, next)
        {
            this.calculator = calculator;
        }

        /// <summary>
        /// Determina si este "handler" puede procesar el mensaje. En el primer mensaje cuando
        /// <see cref="DistanceHandler.State"/> es <see cref="DistanceState.Start"/> usa
        /// <see cref="BaseHandler.Keywords"/> para buscar el texto en el mensaje ignorando mayúsculas y minúsculas. En
        /// caso contrario eso implica que los sucesivos mensajes son parámetros del comando y se procesan siempre.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <returns>true si el mensaje puede ser pocesado; false en caso contrario.</returns>
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
                this.stateForUser.Add(message.From.Id, DistanceState.Start);
                this.Data.Add(message.From.Id, new DistanceData());
            }

            DistanceState state = this.StateForUser[message.From.Id];

            if (state ==  DistanceState.Start)
            {
                // En el estado Start le pide la dirección de origen y pasa al estado FromAddressPrompt
                this.stateForUser[message.From.Id] = DistanceState.FromAddressPrompt;
                response = FROM_PROMPT;
            }
            else if ((state == DistanceState.FromAddressPrompt) && (this.calculator != null))
            {
                // En el estado FromAddressPrompt el mensaje recibido es la respuesta con la dirección de origen
                this.Data[message.From.Id].FromAddress = message.Text;
                this.stateForUser[message.From.Id] = DistanceState.ToAddressPrompt;
                response = TO_PROMPT;
            }
            else if ((state == DistanceState.ToAddressPrompt) && (this.calculator != null))
            {
                this.Data[message.From.Id].ToAddress = message.Text;
                this.Data[message.From.Id].Result = this.calculator.CalculateDistance(
                    this.Data[message.From.Id].FromAddress,
                    this.Data[message.From.Id].ToAddress);

                if (this.Data[message.From.Id].Result.FromExists && this.Data[message.From.Id].Result.ToExists)
                {
                    // Si encuentra ambas direcciones retorna la distancia y pasa nuevamente al estado Start
                    response = $"La distancia es {this.Data[message.From.Id].Result.Distance}";
                    this.stateForUser.Remove(message.From.Id);
                    this.Data.Remove(message.From.Id);
                }
                else
                {
                    // Si no encuentra alguna de las direcciones se las pide de nuevo y vuelve al estado FromAddressPrompt.
                    // Una versión más sofisticada podría determinar cuál de las dos direcciones no existe y volver al
                    // estado en el que se pide la dirección que falta.
                    response = ADDRESS_NOT_FOUND;
                    this.stateForUser[message.From.Id] = DistanceState.FromAddressPrompt;
                }
            }
            else if (((state == DistanceState.FromAddressPrompt) || (state == DistanceState.FromAddressPrompt))
                && (this.calculator == null))
            {
                // En los estados FromAddressPrompt o ToAddressPrompt si no hay un buscador de direcciones hay que
                // responder que hubo un error y volver al estado inicial.
                response = INTERNAL_ERROR;
                this.stateForUser.Remove(message.From.Id);
                this.Data.Remove(message.From.Id);
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
                this.Data.Remove(message.From.Id);
            }
        }

        /// <summary>
        /// Indica los diferentes estados que puede tener el comando DistanceHandler.
        /// - Start: El estado inicial del comando. En este estado el comando pide la dirección de origen y pasa al
        /// siguiente estado.
        /// - FromAddressPrompt: Luego de pedir la dirección de origen. En este estado el comando pide la dirección de
        /// destino y pasa al siguiente estado.
        /// - ToAddressPrompt: Luego de pedir la dirección de destino. En este estado el comando calcula la distancia
        /// y vuelve al estado Start.
        /// </summary>
        public enum DistanceState
        {
            Start,
            FromAddressPrompt,
            ToAddressPrompt
        }

        /// <summary>
        /// Representa los datos que va obteniendo el comando DistanceHandler en los diferentes estados.
        /// </summary>
        public class DistanceData
        {
            /// <summary>
            /// La dirección que se ingresó en el estado DistanceState.FromAddressPrompt.
            /// </summary>
            public string FromAddress { get; set; }

            /// <summary>
            /// La dirección que se ingresó en el estado DistanceState.ToAddressPrompt.
            /// </summary>
            public string ToAddress { get; set; }

            /// <summary>
            /// El resultado del cálculo de la distancia entre las direcciones ingresadas.
            /// </summary>
            public IDistanceResult Result { get; set; }
        }
    }
}