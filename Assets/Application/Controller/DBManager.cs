using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//making the class static to get accessed from anywhere
public static class DBManager  {
    public static string shortname;

    public static bool LoggedIn { get { return shortname != null; } }
    
    public static void LogOut()
    {
        shortname = null;
    }

}
