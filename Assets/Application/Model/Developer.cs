using UnityEngine;
using System.Collections;

public class Developer
{

    private int id;
    private string shortname;
    private string firstname;
    private string lastname;
    private bool isloggedin;

    private static Developer instance;

    private Developer()
    {

    }

    public static Developer GetInstalce()
    {
        if(instance == null)
        {
            instance = new Developer();
        }
        return instance;
    }

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string Firstname
    {
        get
        {
            return firstname;
        }

        set
        {
            firstname = value;
        }
    }

    public string Lastname
    {
        get
        {
            return lastname;
        }

        set
        {
            lastname = value;
        }
    }
    public bool Isloggedin
    {
        get
        {
            return isloggedin;
        }

        set
        {
            isloggedin = value;
        }
    }

    public string Shortname
    {
        get
        {
            return shortname;
        }

        set
        {
            shortname = value;
        }
    }
}
