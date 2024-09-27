using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Library;

public abstract class User : IUser
{
    public string ID{get; set;}
    public string Name{get;set;}
    public string LastName{get;set;}
    public string Location{get;set;}
    public string contactNumber{get;set;}
    public string contactEmail{get;set;}
    public List<Qualification> Reviews = new List<Qualification>();

    public User(string name, string lastname, string id, string location, string contactnumber, string contactemail) {
        this.Name = name;
        this.ID = id;
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
    
    public List<Contract> getContracts()
    {
        return ContractManager.Instance.getContracts(this);
    }
    public double getQualy()
    {
        return QualificationManager.Instance.getAverage(this.Reviews);
    }
    /*public void Qualify()
    {
        List<Contract> contracts = ContractManager.Instance.getFinishedContracts(this);
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
    }*/
}