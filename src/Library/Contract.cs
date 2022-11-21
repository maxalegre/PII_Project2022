using System.Collections.Generic;
using System.Collections;

namespace Library
{
    public class Contract : IContract
    {
        private string initDate;
        private string finalDate;
        public string jobs;
        public bool IsValid;
        public bool Review = false;
        public Employee employee;
        public Employer employer;

        public Contract(string initDate, string finalDate, string jobs, Employee employee, Employer employer)
        {
            this.initDate = initDate;
            this.finalDate = finalDate;
            this.jobs = jobs;

        }
        
       public string getInitDate()
       {
        return this.initDate;
       }
       public string getFinalDate()
       {
        return this.finalDate;
       }
       
       public void setInitDate(string newInitDate)
       {
        this.initDate = newInitDate;
       } 
       public void setFinalDate(string newFinalDate)
       {
        this.finalDate = newFinalDate;
       }
       
       public void setJobs (string newJob)
       {
        this.jobs = newJob;
       }

        // No se como mejorar lo del atributo si esta vigente. Pense en que si no tiene fecha de culminacion es que el contrato es valido y el trabajador sigue con contrato. 
        // Si la fecha de finalizacion es "-" es porque el trabajor sigue trabajando, por lo que  el contrato seria true, si es distinto a "-" es porque el contrato finalizo
        // No se si quieren dejarlo asi o quuieren modificar eso, porque no se me ocurre otra cosa. 
        public void isValid(string finalDate)
        {
            if (finalDate != "-")
            {
                IsValid = false;
            }
            else 
            {
                IsValid = true;
            }
        }
        // Para el timer
        public void toTimer (Employee employee, Employer employer)
        {
            this.employee = employee;
            this.employer = employer;
        }


    }
}