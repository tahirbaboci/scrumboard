using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RootTicket
{
    public List<Ticket> tickets = new List<Ticket>();
}
public class Ticket
{
    public int id { get; set; }
    public string tickettitle { get; set; }
    public string ticketdesc { get; set; }
    public string creationdate { get; set; }
    public string status { get; set; }
    public int sprint { get; set; }
    public string solution { get; set; }
    public int idBoard { get; set; }
}