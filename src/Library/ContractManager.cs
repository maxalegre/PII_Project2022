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
    public List<Contract> contracts = new List<Contract>();
    private ContractManager(){}
    public void createContracts(string initDate, string finalDate, string jobs, Employee employee, Employer employer)
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
              
        Contract contract = new Contract (initDate, finalDate, jobs, employer, employee);
        this.contracts.Add(contract);
    }
    public List<Contract> getEmployeeContracts(Employee employee)
    {
        List<Contract> employeeContractlist = new List<Contract>();
        foreach (Contract contract in this.contracts)
        {
            if (contract.employee == employee)
            {
                employeeContractlist.Add(contract);
            }
        }
        return employeeContractlist;
    }
    public List<Contract> getEmployerContracts(Employer employer)
    {
        List<Contract> employerContractlist = new List<Contract>();
        foreach (Contract contract in this.contracts)
        {
            if (contract.employer == employer)
            {
                employerContractlist.Add(contract);
            }
        }
        return employerContractlist;
    }
    
    public List<Contract> getValidContracts(IUser user)
    {
        List<Contract> list = new List<Contract>();
        foreach (Contract item in this.contracts)
        {
            if (item.IsValid == true)
            {    
                if (item.employer == ((Employer)user) || item.employee == ((Employee)user) )
                {
                    list.Add(item);
                }
            }
        }
        return list;
    }

    public void PrintContracts(List<Contract> list)
    {
        int count = 1;
        foreach (Contract item in list)
        {
            System.Console.WriteLine($"{count} - {item.employer} ha contratado a {item.employee} para realizar el trabajo de {item.jobs}\n");
            count++;
        }
    }

    }
}