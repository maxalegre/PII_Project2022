using System;

namespace Library
{
    public interface IContract
    {
        void setInitDate(DateTime newInitDate);
        void setFinalDate(DateTime newFinalDate);
        void setJobs(string newJob);
    }
}