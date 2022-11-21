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
            this.Keywords = new string[] { "offers" };
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

            var Parameter1 = parameters[1];
            var Parameter2 = parameters[2];

            if (Parameter1.ToLower() == "category")
            {
                response = caseCategories(Parameter2);
            }
            else if (Parameter1.ToLower() == "reputation")
            {
                response = caseReputation();
            }
            else
            {
                response = "Parametros incorrectos, intente de nuevo";
            }

        }
        public string caseCategories(string category)
        {
            var list = OffersManager.Instance.getOffersCategories(category);

            var concString = "";
            foreach (Offer offer in list)
            {
                concString += $"Name: {offer.employee.Name} | Description: {offer.Description} | Remuneration: {offer.Remuneration}\n";
            }
            return concString;
        }
        /*public string caseUbication(string ubication)
        {

        }*/
        public string caseReputation()
        {
            var list = OffersManager.Instance.sortOffersByReputation();
            var concString = "";
            foreach (Offer offer in list)
            {
                concString += $"Name: {offer.employee.Name} | Description: {offer.Description} | Remuneration: {offer.Remuneration}\n";
            }
            return concString;
        }
    }
}