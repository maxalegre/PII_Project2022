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
    public void Review(IUser user, int rating, string comment, Contract contract)
    {
        // User es el que esta haciendo la review.
        // El que sera calificado se encuentra dentro de contract.
        if (rating <= 5 && rating >= 1)
        {
            if (user is Employer)
            {
                if (contract.employeeReviewed == false)
                {
                    contract.employeeReviewed = true;
                    contract.employee.AddQualification(new Qualification(rating, comment));
                }
                else
                {
                    throw new QualificationException("Ya existe una review para el empleado");
                }
                if (user is Employee)
                {
                    if (contract.employerReviewed == false)
                    {
                        contract.employerReviewed = true;
                        contract.employer.AddQualification(new Qualification(rating, comment));
                    }
                    else
                    {
                        throw new QualificationException("Ya existe una review para el empleador");
                    }
                }
            }
        }
        else
        {
            throw new QualificationException("El rating de la review esta fuera de rango (1 a 5)");
        }
    }
}