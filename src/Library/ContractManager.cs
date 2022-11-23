using System.Collections;
using System.Collections.Generic;

namespace Library
{
    /// <summary>
    /// Clase que se encarga de manejar los contratos. Se separan en employers y employees
    /// </summary>
    public sealed class ContractManager 
    {
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
    public void createContracts(string initDate, string finalDate, string jobs, User employee, User employer)
    {
 
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
              
        Contract contract = new Contract (initDate, finalDate, jobs, employee, employer);
        employee.getContracts().Add(contract);
        employer.getContracts().Add(contract);

    }
    
    }
}