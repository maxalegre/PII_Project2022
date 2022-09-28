using System.Collections;
using System.Collections.Generic;
namespace Library;

public class Employee
{
    //datos personales
    public string displayName;
    private string Name;
    private string LastName;
    public string Location;
    //info de contacto
    public ArrayList contactNumbers = new ArrayList();
    public string contactEmail;
    public List<Qualifications> Reviews = new List<Qualifications>();

    public Employee (string Name, string LastName, string Location, string contactNumber, string contactEmail)
    {
        this.displayName = Name;
        this.Name = Name;
        this.LastName = LastName;
        this.Location = Location;
        this.contactNumbers.Add(contactNumber);
        this.contactEmail = contactEmail;
    }
    public void changeUsername(string newName)
    {
        this.displayName = newName;
    }
    public void addNumber(string newNumber)
    {
        this.contactNumbers.Add(newNumber);
    }
    public void changeEmail(string newEmail)
    {
        this.contactEmail = newEmail;
    }
    public void makeOffer()
    {
    //ni idea que poner aca
    }
    public void AddQualification(Qualifications calificacion){
        this.Reviews.Add(calificacion);
    }
    public void Qualify(Qualifications calificacion, Employer receptor)
    {
        receptor.AddQualification(calificacion);
    }
}
