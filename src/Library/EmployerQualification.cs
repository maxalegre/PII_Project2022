/*using System;
using System.Collections.Generic;
using Library;

public class EmployerQualification : Qualification
{
    public Employer employer;
    public EmployerQualification(Employer employer, int rating, string comment, Contract contract)
    {
        if (rating <= 5 && rating >= 1)
        {
            if (this.contract.employerReviewed == false)
            {
                this.employer = employer;
                this.rating = rating;
                this.comment = comment;
                contract.employerReviewed = true;
                employer.AddQualification(this);
            }
            else
            {
                //mensaje de error si ya existe una review
            }
        }
        else
        {
            //mensaje de error si el rating esta fuera de su rango (1 a 5)
        }
    }
}*/