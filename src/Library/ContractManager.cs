using System.Collections;
using System.Collections.Generic;

namespace Library
{
    /// <summary>
    /// Clase que se encarga de manejar los contratos. Se separan en employers y employees
    /// </summary>
    public sealed class ContractManager 
    {
    public List<Contract> employeeContracts = new List<Contract>();
    public List<Contract> employerContracts = new List<Contract>();
    private static ContractManager instance;

    public static ContractManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ContractManager();
            }

            return instance;
        }
    }
    public ContractManager(){}
    public void createEmployeeContracts(string initDate, string finalDate, string jobs, string role)
    {
        if (string.Equals(role.ToLower(), "employee"))
        {
            employeeContracts.Add(new Contract(initDate,finalDate,jobs));
            if (string.IsNullOrEmpty(initDate))
            {
                throw new ContractException ("Fecha inicial no válida");
            }
            else if (string.IsNullOrEmpty(finalDate))
            {
                throw new ContractException ("Fecha final no válida");
            }
            else if (string.IsNullOrEmpty(jobs))
            {
                throw new ContractException ("Trabajo no válido");
            }
        }
        else if (string.Equals(role.ToLower(), "employer"))
        {
            employerContracts.Add(new Contract(initDate,finalDate,jobs));
            if (string.IsNullOrEmpty(initDate))
            {
                throw new ContractException ("Fecha inicial no válida");
            }
            else if (string.IsNullOrEmpty(finalDate))
            {
                throw new ContractException ("Fecha final no válida");
            }
            else if (string.IsNullOrEmpty(jobs))
            {
                throw new ContractException ("Trabajo no válido");
            }
        }
       
    }

    public void GetEmployeeContracts()
    {
        List<Contract> listEmployee = new List<Contract>();
        foreach (Contract a in this.employeeContracts)
        {
            System.Console.WriteLine("El trabajador comenzó a trabajar el día: {0} como {1}", a.getInitDate(), a.jobs);
        }
        
    }
    /// <summary>
    /// genera lista y devulve los contratos de employers
    /// </summary>
    public void GetEmployerContracts()
    {
        List<Contract> listEmployer = new List<Contract>();
        foreach (Contract b in this.employerContracts)
        {
           System.Console.WriteLine("El empleador comenzó a trabajar el día: {0} como {1}", b.getInitDate(), b.jobs);
        }
        
    }
    }
}