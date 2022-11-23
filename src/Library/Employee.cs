using System;
using System.Collections;
using System.Collections.Generic;
namespace Library;

public class Employee : User, IUser
{
    public bool hired = false; // Esto lo hice para certificar una contratacion
    new public string ID{get;}
    new public string Name{get;set;}
    new public string LastName{get;set;}
    new public string Location{get;set;}
    new public string contactNumber{get;set;}
    new public string contactEmail{get;set;}

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
    public void addOffer(string description, double remuneration, string category)
    {
        OffersManager.Instance.addOffer(this,description,remuneration,category);
    }
}
