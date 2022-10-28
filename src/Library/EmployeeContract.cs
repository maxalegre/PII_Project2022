using System.Collections.Generic;

namespace Library
{
    public class EmployeeContract : Contract
    {
        private string initDate;
        private string finalDate;
        public string jobs;
        public string recommendations {get; set;}

        public EmployeeContract (string initDate, string finalDate, string jobs, string recommendations) : base (initDate, finalDate, jobs)
        {
            this.initDate = initDate;
            this.finalDate = finalDate;
            this.jobs = jobs;
            this.recommendations = recommendations;
        }

        
    }
}