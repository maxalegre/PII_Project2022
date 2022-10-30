using System;
using System.Collections;
using System.Collections.Generic;
namespace Library;

public class Employee : User, IUser
{
    public List<Qualification> Reviews = new List<Qualification>();

    public Employee (string name, string lastname, string id, string rol, string location, string contactnumber, string contactemail) 
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
    public void makeOffer(string Description, double Remuneration) 
    {
        Offers offer = new Offers(Description, Remuneration);
    }
    public void AddQualification(Qualification calificacion){
        this.Reviews.Add(calificacion);
    }
    public void Qualify(Qualification calificacion, Employer receptor)
    {
        receptor.AddQualification(calificacion);
    }
    
}
