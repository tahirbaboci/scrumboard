using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RootBoard
{
    public List<Board> boards = new List<Board>();
}
public class Board {
    private int id;
    private string projectName;
    private string creationDate;
    private bool status;

    public Board()
    {

    }
    public Board(int id, string projectName, string creationDate, bool status)
    {
        this.id = id;
        this.projectName = projectName;
        this.creationDate = creationDate;
        this.status = status;
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

    public string ProjectName
    {
        get
        {
            return projectName;
        }

        set
        {
            projectName = value;
        }
    }

    public string CreationDate
    {
        get
        {
            return creationDate;
        }

        set
        {
            creationDate = value;
        }
    }

    public bool Status
    {
        get
        {
            return status;
        }

        set
        {
            status = value;
        }
    }
}
