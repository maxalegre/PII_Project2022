using System;
using System.Collections.Generic;
using Library;
public class QualificationManager
{
    private static QualificationManager instance;

    public static QualificationManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new QualificationManager();
            }

            return instance;
        }
    }
    public QualificationManager(){}
    
}
