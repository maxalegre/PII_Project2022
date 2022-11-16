using System.Collections;
using System.Collections.Generic;
namespace Library;

public class Employer : User, IUser
{
    public List<Qualification> Reviews = new List<Qualification>();

    public Employer (string name, string lastname, string id, string rol, string location, string contactnumber, string contactemail) 
    : base(name, lastname, id, rol, location, contactemail, contactnumber)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new UserException("Nombre inválido");
        }
        else if (string.IsNullOrEmpty(lastname))
        {
            throw new UserException("Apellido inválido");
        }
        else if (string.IsNullOrEmpty(id))
        {
            throw new UserException("ID inválido");
        }
        else if (string.IsNullOrEmpty(location))
        {
            throw new UserException("Localización inválido");
        }
        else if (string.IsNullOrEmpty(contactnumber))
        {
            throw new UserException("Numero inválido");
        }
        else if (string.IsNullOrEmpty(contactemail))
        {
            throw new UserException("Email inválido");
        }
        else
        {   
            this.Name = name;
            this.LastName = lastname;
            this.ID = id;
            this.Location = location;
            this.contactNumber = contactnumber;
            this.contactEmail = contactemail;
        }
    }
    public void changeNumber(string newNumber)
    {
        this.contactNumber = newNumber;
    }
    public void changeEmail(string newEmail)
    {
        this.contactEmail = newEmail;
    }
    
    public void AddQualification(Qualification calificacion){
        this.Reviews.Add(calificacion);
    }
    public void getOffersByCategory(string category)
    {
        //OffersManager offersManager= new OffersManager();
        OffersManager.Instance.getOffersByCategory(category);
    }

    public void addOfferEmployer (string description, double Remuneration, string category)
    {
        OffersManager.Instance.addOffer(this,description,Remuneration,category);

    }
    // ADD
    public void searchEmployee (string category)
    {
       // El empleador debe recibir una lista de ofertas y seleccionar uno para contratar un employee.
       // acá recibe la lista de ofertas por categoria
       OffersManager.Instance.getOffersByCategory(category);
       // acá recibe la lista de ofertas por reputación
       OffersManager.Instance.sortOffersByReputation();
       

       //Elegir que oferta se acepta
       // mi idea acá es elegir de la lista de offers una oferta, sacarla de esa lista, y que ese offer al estar relacionado
       // con un employee, ya lo pueda contratar.
       foreach (Offer offer in OffersManager.Instance.getOffersCategories(category))
       {
            // recorrer lista de ofertas por categoria
            // de esta lista de offers seleccionar una

            System.Console.WriteLine("{0} => {1}", offer.Category, offer.employee.Name);
            System.Console.WriteLine("Seleccione un empleado a contratar:");

            // Comparo lo que se ingresa por consola y si es igual al nombre del employee lo contrata

            if (System.Console.ReadLine() == offer.employee.ToString())
            {
                System.Console.WriteLine("{0} contratado", offer.employee);
                offer.employee.hired = true;
                // Ofrecer el contrato?????????
                /*
                System.Console.WriteLine("Ingrese fecha: ");
                string initDate = System.Console.ReadLine();
                System.Console.WriteLine("Ingrese su rol: ");
                string role = System.Console.ReadLine();
                ContractManager.Instance.createContracts(initDate, "-", offer.Category, role);
                */
            }
       }

    }
    
}


