using System.Collections.Generic;
using Library;
public interface IUser
{
    void changeNumber(string newNumber);
    void changeEmail(string newEmail);
    void AddQualification(Qualification qualification);
    double getQualy();
}
