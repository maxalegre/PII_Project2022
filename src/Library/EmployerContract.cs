using System.Collections.Generic;
using System.Collections;

namespace Library
{
    public class EmployerContract : Contract
    {
        private string initDate;
        private string finalDate;
        public string jobs;

        
        public EmployerContract (string initDate, string finalDate, string jobs) : base (initDate, finalDate, jobs)
        {
            this.initDate = initDate;
            this.finalDate = finalDate;
            this.jobs = jobs;
        }

        
    }
}