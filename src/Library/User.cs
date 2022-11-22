using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Library;

public abstract class User : IUser
{
    public string Name;
    public string LastName;
    public string ID;
    public string Rol;
    public string Location;
    public string contactNumber;
    public string contactEmail;
    public List<Qualification> Reviews = new List<Qualification>();

    public User(string name, string lastname, string id, string rol, string location, string contactnumber, string contactemail) {
        this.Name = name;
        this.ID = id;
        this.Rol = rol;
        this.Location = location;
        this.contactNumber = contactnumber;
        this.contactEmail = contactemail;
    }

    public void changeNumber(string newNumber)
    {
        this.contactNumber = newNumber;
    }
    public void changeEmail(string newEmail)
    {
        this.contactEmail = newEmail;
    }

    public void AddQualification(Qualification calificacion)
    {
        this.Reviews.Add(calificacion);
    }
    
    public double getQualy()
    {
        return QualificationManager.Instance.getAverage(this.Reviews);
    }
    public abstract List<Contract> getContracts();
    public void Qualify()
    {
        List<Contract> contracts = ContractManager.Instance.getValidContracts(this);
        ContractManager.Instance.PrintContracts(contracts);
        System.Console.WriteLine("Seleccione el contrato en el que desea realizar la calificación: ");
        int input;
        while (true)
        {   
            input = int.Parse(Console.ReadLine())-1;
            if (input > 0 && input < contracts.Count)
            {
                break;
            }
            else
            {
                System.Console.WriteLine("Fuera de rango.\n");
            }
        }
        Contract Selected_Contract = contracts.ElementAt(input);
        System.Console.WriteLine("Ingrese la calificación (del 1 a 5): ");
        while (true)
        {   
            input = int.Parse(Console.ReadLine());
            if (input >= 1 && input <= 5)
            {
                break;
            }
            else
            {
                System.Console.WriteLine("Fuera de rango.\n");
            }
        }
        int rating = input;
        System.Console.WriteLine("Ingrese el comentario (Opcional): ");
        string comment = Console.ReadLine();
        QualificationManager.Instance.Review(this, rating, comment, Selected_Contract);
    }
}