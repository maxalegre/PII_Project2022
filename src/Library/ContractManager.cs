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
    private ContractManager(){}
    public void createContracts(string initDate, string finalDate, string jobs, Employee employee, Employer employer)
    {
        
        Contract contract = new Contract (initDate, finalDate, jobs, employer, employee);
        employee.Contract.Add(contract);
        employer.Contract.Add(contract);
       

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
    

    public List<Contract>  GetEmployeeContracts()
    {
        foreach (Contract a in this.employeeContracts)
        {
            System.Console.WriteLine("El trabajador comenzó a trabajar el día: {0} como {1}", a.getInitDate(), a.jobs);
        }
        return employeeContracts;
        
    }

    public List<Contract> getEmployerContracts()
    {
        foreach (Contract b in this.employerContracts)
        {
            System.Console.WriteLine("El empleador firmó el contrato el día: {0} como {1}", b.getInitDate(), b.jobs);
        }
        return employerContracts;
    }
    /// <summary>
    /// genera lista y devulve los contratos de employers
    /// </summary>
    
    }
}