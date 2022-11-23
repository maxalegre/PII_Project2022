using System;
using System.Collections.Generic;
using System.Collections;

namespace Library
{
    public class Contract : IContract
    {
        private DateTime initDate;
        private DateTime finalDate;
        public string jobs;
        public bool Finished = false;
        public bool Review = false;
        public Employee employee;
        public Employer employer;
        public bool employeeReviewed = false;
        public bool employerReviewed = false;

        public Contract(DateTime initDate, DateTime finalDate, string jobs, Employer employer, Employee employee)
        {
            this.initDate = initDate;
            this.finalDate = finalDate;
            this.jobs = jobs;
            this.employee = employee;
            this.employer = employer;

            var timeout = new TimeOutContract();
            timeout.contract = this;
            var timer = new CountdownTimer();
            timer.Register(((long)(finalDate - initDate).TotalMilliseconds),timeout);
        }
        
        public DateTime getInitDate()
        {
            return this.initDate;
        }

        public DateTime getFinalDate()
        {
            return this.finalDate;
        }
       
        public void setInitDate(DateTime newInitDate)
        {
            this.initDate = newInitDate;
        } 

        public void setFinalDate(DateTime newFinalDate)
        {
            this.finalDate = newFinalDate;
        }
       
        public void setJobs (string newJob)
        {
            this.jobs = newJob;
        }
        
        public void ended()
        {
            this.Finished = true;
            var timeout = new TimeOutReview();
            timeout.contract = this;
            var timer = new CountdownTimer();
            timer.Register(60000,timeout);
        }
    }
}