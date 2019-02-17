using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class TicketController : MonoBehaviour
{
    WWW ticketsData;
    WWW ticketsCount;
    public WWW sprintData;
    WWW addTicket;
    WWW deleteTicketData;
    WWW finishTicketData;

    public InputField id;
    public InputField tickettitle;
    public InputField ticketdesc;
    public InputField sprint;
    public InputField solution;
    public Button addTicketButton;

    private JsonData itemData;

    public List<string> sprintList;

    public RootTicket rootticket;
    public Ticket ticket;
    public string message;

    public GameObject AddTicketPopupGameObjectManager;
    public GameObject DeleteTicketPopupGameObjectManager;
    public GameObject FinishTicketPopupGameObjectManager;
    PopUpScript popUp;

    ChoosenValues choose;

    public void calladdticket()
    {
        StartCoroutine(addticket());
    }
    public void calldeleteticket(int idTicket)
    {
        StartCoroutine(deleteticket(idTicket));
    }
    public void callreadtickets()
    {
        StartCoroutine(readtickets());
    }
    public void callreadSprint()
    {
        StartCoroutine(readSprint());
    }
    public void callFinishTicket(int idTicket)
    {
        StartCoroutine(finishTicket(idTicket));
    }


    private void Start()
    {
        choose = ChoosenValues.GetInstalce();
    }

    IEnumerator addticket()
    {
        WWWForm form = new WWWForm();
        
        form.AddField("tickettitle", tickettitle.text);
        form.AddField("sprint", choose.Sprint);
        form.AddField("ticketdesc", ticketdesc.text);
        form.AddField("idboard", choose.Project);

        addTicket = new WWW("http://192.168.1.32/scrumboard/ticketservices/create.php", form);
        while (addTicket.isDone == false)
        {
            if (addTicket.isDone == true) break;
        }
        if (!string.IsNullOrEmpty(addTicket.error))
        {
            popUp = AddTicketPopupGameObjectManager.GetComponent<PopUpScript>();
            popUp.Show(addTicket.error);
        }
        //getAddTicketMessage();
        addTicketToList();
        yield return addTicket;
    }
    public string getAddTicketMessage()
    {
        popUp = AddTicketPopupGameObjectManager.GetComponent<PopUpScript>();
        itemData = JsonMapper.ToObject(addTicket.text);
        int n = itemData.Count;
        if (n > 0)
        {
            popUp.Show(itemData["status_message"].ToString());
            return itemData["status"].ToString();
        }
        else
        {
            return "NoDataWasFound";
        }
    }
    public void addTicketToList()
    {
        if(getAddTicketMessage() == "1")
        {
            ticket = null;
            ticket = new Ticket();
            ticket.tickettitle = tickettitle.text;
            ticket.ticketdesc = ticketdesc.text;
            ticket.sprint = int.Parse(choose.Sprint);
            ticket.idBoard = int.Parse(choose.Project);
            rootticket.tickets.Add(ticket);
            tickettitle.text = "";
            ticketdesc.text = "";
        }
    }
    IEnumerator finishTicket(int idTicket)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", idTicket);
        form.AddField("solution", solution.text);

        finishTicketData = new WWW("http://192.168.1.32/scrumboard/ticketservices/update_status.php", form);
        while (finishTicketData.isDone == false)
        {
            if (finishTicketData.isDone == true) break;
        }
        popUp = FinishTicketPopupGameObjectManager.GetComponent<PopUpScript>();
        if (!string.IsNullOrEmpty(finishTicketData.error))
        {
            popUp.Show(finishTicketData.error);
        }
        yield return finishTicketData;
    }
    public string getFinishTicketMessage()
    {
        popUp = FinishTicketPopupGameObjectManager.GetComponent<PopUpScript>();
        itemData = JsonMapper.ToObject(finishTicketData.text);
        int n = itemData.Count;
        if (n > 0)
        {
            popUp.Show(itemData["status_message"].ToString());
            return itemData["status"].ToString();
        }
        else
        {
            return "NoDataWasFound";
        }
    }
    IEnumerator deleteticket(int idTicket)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", idTicket);

        deleteTicketData = new WWW("http://192.168.1.32/scrumboard/ticketservices/delete.php", form);
        while (deleteTicketData.isDone == false)
        {
            if (deleteTicketData.isDone == true) break;
        }
        popUp = DeleteTicketPopupGameObjectManager.GetComponent<PopUpScript>();
        if (!string.IsNullOrEmpty(deleteTicketData.error))
        {
            popUp.Show(deleteTicketData.error);
        }
        yield return deleteTicketData;
    }
    public string getDeleteTicketMessage()
    {
        popUp = DeleteTicketPopupGameObjectManager.GetComponent<PopUpScript>();
        itemData = JsonMapper.ToObject(deleteTicketData.text);
        int n = itemData.Count;
        if (n > 0)
        {
            popUp.Show(itemData["status_message"].ToString());
            return itemData["status"].ToString();
        }
        else
        {
            return "NoDataWasFound";
        }
    }
    public void removeTicketAndTasksFromList()
    {

    }
    IEnumerator readSprint()
    {
        sprintData = new WWW("http://192.168.1.32/scrumboard/ticketservices/read_sprint.php");
        while (sprintData.isDone == false)
        {
            if (sprintData.isDone == true) break;
        }
        getReadSprints();
        yield return sprintData;
    }
    public void getReadSprints() {

        sprintList = null;
        if(sprintList == null)
        {
            sprintList = new List<string>();
        }
        itemData = JsonMapper.ToObject(sprintData.text);
        int n = itemData["sprints"].Count;
        if (n > 0)
        {
            for (int i = 0; i < n; i++)
            {
                sprintList.Add(itemData["sprints"][i]["sprint"].ToString());
            }
            
        }
    }
    IEnumerator readtickets()
    {
        WWWForm form = new WWWForm();
        form.AddField("sprint", choose.Sprint);
        form.AddField("idBoard", choose.Project);

        ticketsData = new WWW("http://192.168.1.32/scrumboard/ticketservices/read.php", form);
        WaitUntil a = new WaitUntil(() => ticketsData.isDone == true);
        while (ticketsData.isDone == false)
        {
            if (ticketsData.isDone == true) break;
        }
        getTickets();
        yield return ticketsData;
    }
    public void getTickets()
    {
        rootticket = null;
        ticket = null;
        if (rootticket == null)
        {
            rootticket = new RootTicket();
        }
        itemData = JsonMapper.ToObject(ticketsData.text);
        int n = itemData["tickets"].Count;
        for (int i = 0; i < n; i++)
        {
            ticket = new Ticket();
            ticket.id = int.Parse(itemData["tickets"][i]["id"].ToString());
            ticket.tickettitle = itemData["tickets"][i]["tickettitle"].ToString();
            ticket.ticketdesc = itemData["tickets"][i]["ticketdesc"].ToString();
            ticket.creationdate = itemData["tickets"][i]["creationdate"].ToString();
            ticket.status = itemData["tickets"][i]["status"].ToString();
            ticket.sprint = int.Parse(itemData["tickets"][i]["sprint"].ToString());
            //if(itemData["tickets"][i]["solution"].ToString() != "") {
            //    ticket.solution = itemData["tickets"][i]["solution"].ToString();
            //}
            ticket.idBoard = int.Parse(itemData["tickets"][i]["idBoard"].ToString());
            rootticket.tickets.Add(ticket);
        }
    }
    public Ticket getTicket(int itemId)
    {

        foreach (Ticket item in rootticket.tickets)
        {
            if(item.id == itemId)
            {
                return item;
            }
        }
        return null;
    }
}
