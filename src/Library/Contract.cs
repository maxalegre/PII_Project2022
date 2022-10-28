using System.Collections.Generic;
using System.Collections;

namespace Library
{
    public class Contract : IContract
    {
        private string initDate;
        private string finalDate;
        public string jobs;

        public Contract(string initDate, string finalDate, string jobs)
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


    }
}