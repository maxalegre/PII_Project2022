using System;
using System.Collections;
using System.Collections.Generic;

namespace Library;

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

    /// <summary>
    /// This method creates a contract.
    /// It starts now and ends after "duration" months
    /// </summary>
    /// <param name="duration">Duration of the contract in months.</param>
    /// <param name="jobs">The job to be done.</param>
    /// <param name="employee">The employee.</param>
    /// <param name="employer">The employer.</param>
    public void createContracts(int duration, string jobs, Employee employee, Employer employer)
    {
 
       if (duration < 0)
       {
            throw new ContractException ("Duración no válida");
       }
       else if (string.IsNullOrEmpty(jobs))
       {
            throw new ContractException ("Trabajo no válido");
       }
              

        Contract contract = new Contract (System.DateTime.Now, System.DateTime.Now.AddSeconds(duration), jobs, employer, employee);
        this.contracts.Add(contract);
        Offer offer= OffersManager.Instance.Offers.Find(i => i.employee == employee);
        OffersManager.Instance.removeOffer(offer);
    }
    public List<Contract> getContracts(IUser user)
    {
        List<Contract> list = new List<Contract>();
        foreach (Contract contract in this.contracts)
        {
            if (user is Employer)
            {
                if (contract.employer == ((Employer)user))
                    {
                        list.Add(contract);
                    }
            }
            else if (user is Employee)
            {
                if (contract.employee == ((Employee)user))
                    {
                        list.Add(contract);
                    }
            }
        }
        return list;
    }
    public List<Contract> getFinishedContracts(IUser user)
    {
        List<Contract> list = new List<Contract>();
        foreach (Contract contract in this.contracts)
        {
            if (contract.Finished == true)
            {    
                if (user is Employer)
                {
                    if (contract.employer == ((Employer)user))
                        {
                            list.Add(contract);
                        }
                }
                else if (user is Employee)
                {
                    if (contract.employee == ((Employee)user))
                        {
                            list.Add(contract);
                        }
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