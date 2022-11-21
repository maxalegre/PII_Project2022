using System;
using System.Collections.Generic;
using Library;

public class EmployeeQualification : Qualification
{
    public Employee employee;
    public EmployeeQualification(Employee employee, int rating, string comment, Contract contract)
    {
        if (rating <= 5 && rating >= 1)
        {
            if (this.contract.employeeReviewed == false)
            {
                this.employee = employee;
                this.rating = rating;
                this.comment = comment;
                contract.employeeReviewed = true;
                employee.AddQualification(this);
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
}