using System;
using System.Collections.Generic;
using System.IO;

namespace Library;

public abstract class User
{
    public string Name;
    public string LastName;
    public string ID;
    public string Rol;
    public string Location;
    public string contactNumber;
    public string contactEmail;

    public User(string name, string lastname, string id, string rol, string location, string contactnumber, string contactemail) {
        this.Name = name;
        this.ID = id;
        this.Rol = rol;
        this.Location = location;
        this.contactNumber = contactnumber;
        this.contactEmail = contactemail;
    }
    
}