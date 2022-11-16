using System;
using System.Collections.Generic;
using Library;
public sealed class QualificationManager
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
    private QualificationManager(){}
    
    public double getAverage (List<Qualification> list)
    {
        double average = 0;
        foreach (Qualification a in list)
        {
            average = average + a.Value;

        }
        return average/list.Count;
    }
}
