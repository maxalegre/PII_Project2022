using System;
using Telegram.Bot.Types;
using Library;
namespace Ucu.Poo.TelegramBot
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "hola".
    /// </summary>
    public class OffersHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OffersHandler"/>. Esta clase procesa el mensaje "hola".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public OffersHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"searchoffers"};
        }

        /// <summary>
        /// Procesa el mensaje "hola" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override void InternalHandle(Message message, out string response)
        {
            var parameters = message.Text.Split(" ");
            
            Console.WriteLine(parameters);
            
            var list= OffersManager.Instance.getOffersCategories(parameters[1]);
            var concString= "";
            foreach(Offer offer in list)
            {
                concString+= $"Name: {offer.employee.Name} | Description: {offer.Description} | Remuneration: {offer.Remuneration}\n" ;
            }
            response = concString;
        }
    }
}