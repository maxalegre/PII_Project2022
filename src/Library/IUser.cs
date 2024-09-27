using System.Collections.Generic;
using Library;
public interface IUser
{
    string ID{get; set;}
    string Name{get;set;}
    string LastName{get;set;}
    string Location{get;set;}
    string contactNumber{get;set;}
    string contactEmail{get;set;}
    void changeNumber(string newNumber);
    void changeEmail(string newEmail);
    void AddQualification(Qualification qualification);
    double getQualy();
}
