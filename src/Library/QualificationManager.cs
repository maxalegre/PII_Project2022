using System;
using System.Collections.Generic;
using Library;
public sealed class QualificationManager
{
    private static QualificationManager instance;

    public static QualificationManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new QualificationManager();
            }

            return instance;
        }
    }
    private QualificationManager(){}
    
    public double getAverage (List<Qualification> list)
    {
        double average = 0;
        foreach (Qualification a in list)
        {
            average = average + a.rating;

        }
        return average/list.Count;
    }
}

//Para ambas clases de qualification falta logica para que checkee que el que esta haciendo la review sea el empleador o empleado del contrato, ni idea como hacerlo
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
}