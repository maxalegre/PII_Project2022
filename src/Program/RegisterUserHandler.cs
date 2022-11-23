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
    public class RegisterUserHandler : BaseHandler
    {
        public const string NOMBREAPELLIDO = "Ingrese el nombre y apellido";
        public const string ROLPREGUNTA = "¿Es empleado o empleador?";
        public const string LOCATIONPREGUNTA = "Ingrese su direccion (Calle y numero)";
        public const string NUMBERPREGUNTA = "Ingrese numero de telefono";
        public const string EMAILPREGUNTA = "Ingrese su email";

        public const string INTERNAL_ERROR = "Error interno de configuración, no puedo buscar direcciones";
        public const string PARAMETROERROR = "Usuario no encontrado";

        private Dictionary<long, State> stateForUser = new Dictionary<long, State>();
        private List <String> resultadosPreguntas = new List<string>();

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
        public RegisterUserHandler(BaseHandler next):base(next)
        {
            this.Keywords = new string[] {"register"};
        
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
                this.stateForUser[message.From.Id] = State.NombreApellido;
                response = NOMBREAPELLIDO;
            }
            else if (state == State.NombreApellido)
            {
                //UserData data = new UserData();

                // En el estado AddressPrompt el mensaje recibido es la respuesta con la dirección
                var dato = this.Data[message.From.Id].NombreApellido = message.Text.ToString();
                if (dato!=null)
                {
                    this.resultadosPreguntas.Add(dato);
                    this.stateForUser[message.From.Id] = State.RolPregunta;
                    response = ROLPREGUNTA;
                }
                else
                {
                    response = PARAMETROERROR;
                }
            }
            else if (state == State.RolPregunta)
                {

                var dato = this.Data[message.From.Id].Rol = message.Text.ToString();

                    if(dato!=null)
                    {
                        this.resultadosPreguntas.Add(dato);
                        this.stateForUser[message.From.Id] = State.LocationPregunta;
                        response = LOCATIONPREGUNTA;
                    }
                    else
                    {
                        response = PARAMETROERROR;
                    }
                }
            else if (state == State.LocationPregunta)
                {
                    var dato = this.Data[message.From.Id].Location = message.Text.ToString();

                    if(dato!=null)
                    {
                        this.resultadosPreguntas.Add(dato);
                        this.stateForUser[message.From.Id] = State.NumberPregunta;
                        response = NUMBERPREGUNTA;
                    }
                    else
                    {
                        response = PARAMETROERROR;
                    }
                }
            else if (state == State.NumberPregunta)
                {
                    var dato = this.Data[message.From.Id].PhoneNumber = message.Text.ToString();

                    if(dato!=null)
                    {
                        this.resultadosPreguntas.Add(dato);
                        this.stateForUser[message.From.Id] = State.EmailPregunta;
                        response = EMAILPREGUNTA;
                    }
                    else
                    {
                        response = PARAMETROERROR;
                    }
                }
            else if (state == State.EmailPregunta)
                {
                    var dato = this.Data[message.From.Id].Email = message.Text.ToString();

                    if(dato!=null)
                    {
                        this.resultadosPreguntas.Add(dato);
                        this.stateForUser.Remove(message.From.Id); // Equivalente a volver al estado inicial
                        
                        response = "Se obtuvieron los datos correctamente";
                    }
                    else
                    {
                        response = PARAMETROERROR;
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
            NombreApellido,
            RolPregunta,
            LocationPregunta,
            NumberPregunta,
            EmailPregunta
        }

        /// <summary>
        /// Representa los datos que va obteniendo el comando AddressHandler en los diferentes estados.
        /// </summary>
        private class UserData
        {
            
            public string NombreApellido { get; set; }
            public string Rol { get; set; }
            public string Location { get; set; }
            public string PhoneNumber { get; set; }
            public string ID { get; set; }
            public string Email { get; set; }



            /// <summary>
            /// El resultado de la búsqueda de la dirección ingresada.
            /// </summary>
           // public IAddressResult Result { get; set; }
        }
    }
}