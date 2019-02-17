using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RootTask
{
    public List<Task> tasks = new List<Task>();
}
public class Task
{
    public int id;
    public string tasktitle;
    public string creationdate;
    public int idCategory;
    public int idTicket;
    public int idDeveloper;
    public string category;
    public string tickettitle;
    public string taskOwner;
}