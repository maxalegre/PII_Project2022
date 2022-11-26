using System.Collections.Generic;
using Library;
public interface IUser
{
    public string ID{get;}
     public string Name{get;set;}
     public string LastName{get;set;}
     public string Location{get;set;}
     public string contactNumber{get;set;}
     public string contactEmail{get;set;}
    void changeNumber(string newNumber);
    void changeEmail(string newEmail);
    void AddQualification(Qualification qualification);
    double getQualy();
}
