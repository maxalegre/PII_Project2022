using System.Collections;
using System.Collections.Generic;

namespace Library
{
    /// <summary>
    /// Clase que se encarga de manejar los contratos. Se separan en employers y employees
    /// </summary>
    public class ContractManager 
    {
        public List<Contract> employeeContracts = new List<Contract>();
        public List<Contract> employerContracts = new List<Contract>();
    

    public void createEmployeeContracts(string initDate, string finalDate, string jobs, string recommendations)
    {
        employeeContracts.Add(new EmployeeContract(initDate,finalDate,jobs,recommendations));
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
        else if (string.IsNullOrEmpty(recommendations))
        {
            throw new ContractException ("Recomendación no válida");
        }
        
    }
    /// <summary>
    /// Se crean los contratos
    /// </summary>
    /// <param name="initDate"> fecha en que comienza a trabajar </param>
    /// <param name="finalDate"> fecha que finaliza el contrato </param>
    /// <param name="jobs"> Rubro en el que va a trabajar </param>
    public void createEmployerContract(string initDate, string finalDate, string jobs)
    {
        employerContracts.Add(new EmployerContract(initDate,finalDate, jobs));
        if (string.IsNullOrEmpty(initDate))
        {
            throw new ContractException ("Fecha inicial no válida");
        }
        if (string.IsNullOrEmpty(finalDate))
        {
            throw new ContractException ("Fecha final no válida");
        }
        if (string.IsNullOrEmpty(jobs))
        {
            throw new ContractException ("Trabajos no válidos");
        }

    }
    /// <summary>
    /// Genera lista y devulve los contratos de employees
    /// </summary>

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