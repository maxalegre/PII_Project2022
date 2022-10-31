using System;

namespace Library;

public sealed class Admin
{
    private static Admin instance;

    public static Admin Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Admin();
            }

            return instance;
        }
    }
    private Admin(){}
   
}