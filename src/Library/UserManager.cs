using System;
using System.Collections;
using System.Collections.Generic;
namespace Library;

public sealed class UserManager
{
    private static UserManager instance;

    public static UserManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UserManager();
            }

            return instance;
        }
    }
    public List<IUser> Users = new List<IUser>();
    private UserManager(){}

    public void CreateUser(string name, string lastname, string id, string rol, string location, string contactnumber, string contactemail) {
        if (string.Equals(rol.ToLower(), "employer"))
        {
            Users.Add(new Employer(name, lastname, id, rol, location, contactnumber, contactemail));
        } 
        else if (string.Equals(rol.ToLower(), "employee"))
        {
            Users.Add(new Employee(name, lastname, id, rol, location, contactnumber, contactemail));      
        }
        else
        {
            System.Console.WriteLine("El rol ingresado es invalido");
        }
    }

    public IUser Login(string id)
    {
        foreach (IUser item in this.Users)
        {
            if (CheckCredentials(item, id))
            {
                return item;
            }
        }
        return null;
    }

    public bool CheckCredentials(IUser user, string id) {
        if (user is Employee)
        {
            if (((Employee)user).ID.Equals(id, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        else
        {
            if (((Employer)user).ID.Equals(id, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
}
