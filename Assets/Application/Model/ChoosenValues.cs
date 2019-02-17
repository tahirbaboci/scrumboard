using UnityEngine;
using System.Collections;

public class ChoosenValues
{
    private string sprint;
    private string project;

    private static ChoosenValues instance;

    

    private ChoosenValues()
    {

    }

    public static ChoosenValues GetInstalce()
    {
        if(instance == null)
        {
            instance = new ChoosenValues();
        }
        return instance;
    }

    public string Sprint
    {
        get
        {
            return sprint;
        }

        set
        {
            sprint = value;
        }
    }

    public string Project
    {
        get
        {
            return project;
        }

        set
        {
            project = value;
        }
    }
}
