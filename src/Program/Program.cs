using System;
using Library;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Categories cat = new Categories();
            //cat.Add("asd");

            Admin adm = new Admin();
            adm.addCategory("asd");

            foreach (string item in adm.Category)
            {
                Console.WriteLine(item);
            }
        }
    }
}
