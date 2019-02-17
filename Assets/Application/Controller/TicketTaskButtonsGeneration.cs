using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TicketTaskButtonsGeneration : MonoBehaviour
{
    int InvokeNumber = 0;

    public GameObject prefabTableButtonElements;
    public GameObject prefabTaskButtonElements;
    public RectTransform ParentPanel;
    public RectTransform TaskListParentPanel;
    public GameObject TicketDeleteButton;
    public GameObject TicketDoneButton;

    //public GameObject informationObject;
    public GameObject InformationTicketManager;
    public GameObject InformationTaskManager;
    public GameObject TaskListManager;
    public GameObject TicketDoneManager;

    TicketController ticketController;
    TaskController taskController;

    public Text TicketTitle;
    public Text TicketInfos;

    public Text TaskTitle;
    public Text TaskInfos;

    Developer dev;

    // Use this for initialization
    void Start()
    {
        //InvokeRepeating("init", 0, 5.0f);
        init();
        dev = Developer.GetInstalce();
    }
    public void init()
    {
        ticketController = FindObjectOfType<TicketController>();
        ticketController.callreadtickets();
        
        taskController = FindObjectOfType<TaskController>();
        taskController.callreadtasks();
        readtickets();
    }
    public void readtickets()
    {
        InvokeNumber++;

        if (InvokeNumber > 1)
        {
            int childs = ParentPanel.transform.childCount;
            for (int i = 0; i < childs; i++)
            {
                GameObject.Destroy(ParentPanel.transform.GetChild(i).gameObject);
            }
        }
        int tempIntTableButtons = 0;
        foreach (Ticket item in ticketController.rootticket.tickets)
        {
            GameObject ButtonElements = (GameObject)Instantiate(prefabTableButtonElements);
            ButtonElements.transform.SetParent(ParentPanel, false);
            ButtonElements.transform.localScale = new Vector3(1, 1, 1);
            int n = ButtonElements.transform.childCount;
            for (int j = 0; j < n; j++)
            {
                if (j == 0)
                {
                    Button tickbtn = ButtonElements.transform.GetChild(j).GetComponent<Button>();
                    tickbtn.onClick.AddListener(() => InformationTicketManager.transform.GetComponent<Manager>().PauseForInformationTicket());
                    tickbtn.onClick.AddListener(() => getTicketInfo(ticketController.getTicket(item.id)));
                    Text ticketStatus = tickbtn.transform.GetChild(0).GetComponent<Text>();
                    if(item.status == "1")
                    {
                        ticketStatus.text = "\u2714";
                    }
                    else
                    {
                        ticketStatus.text = item.id.ToString();
                    }
                }
                else if (j == 1)
                {
                    Button ToDoTaskBtn = ButtonElements.transform.GetChild(j).GetComponent<Button>();
                    ToDoTaskBtn.onClick.AddListener(() => TaskListManager.transform.GetComponent<Manager>().Pause());
                    ToDoTaskBtn.onClick.AddListener(() => getTaskListInfo(taskController.getTasksByTicketIdAndCategory(item.id, 7))); //7 means TODO
                }
                else if (j == 2)
                {
                    Button InProgressTaskBtn = ButtonElements.transform.GetChild(j).GetComponent<Button>();
                    InProgressTaskBtn.onClick.AddListener(() => TaskListManager.transform.GetComponent<Manager>().Pause());
                    InProgressTaskBtn.onClick.AddListener(() => getTaskListInfo(taskController.getTasksByTicketIdAndCategory(item.id, 8))); //8 means InProgress
                }
                else if (j == 3)
                {
                    Button DoneTaskBtn = ButtonElements.transform.GetChild(j).GetComponent<Button>();
                    DoneTaskBtn.onClick.AddListener(() => TaskListManager.transform.GetComponent<Manager>().Pause());
                    DoneTaskBtn.onClick.AddListener(() => getTaskListInfo(taskController.getTasksByTicketIdAndCategory(item.id, 9))); //9 means Done
                }
            }
            tempIntTableButtons++;
        }
    }
    void getTicketInfo(Ticket ticket)
    {
        //informationObject.SetActive(true);
        string text = "TicketID:" + ticket.id + "\nTicket Desc:" + ticket.ticketdesc + "\nCreation Date:" + ticket.creationdate;
        TicketTitle.text = ticket.tickettitle;
        TicketInfos.text = text;
        Button ticketDeleteBtn = TicketDeleteButton.transform.GetComponent<Button>();
        ticketDeleteBtn.onClick.AddListener(() => ticketController.calldeleteticket(ticket.id));
        ticketDeleteBtn.onClick.AddListener(() => getMessageFromDeleteTicket(ticket));

        Button ticketDoneBtn = TicketDoneButton.transform.GetComponent<Button>();
        //ticketDoneBtn.onClick.AddListener(() => TicketDoneManager.transform.GetComponent<Manager>().PauseForTicketDone());
        ticketDoneBtn.onClick.AddListener(() => ticketController.callFinishTicket(ticket.id));
        ticketDoneBtn.onClick.AddListener(() => getMessageFromFinishTicket(ticket));

        ticketDoneBtn.interactable = true;
        if (ticket.status == "1")
        {
            ticketDoneBtn.interactable = false;
        }
    }
    void getMessageFromFinishTicket(Ticket item)
    {
        string message = ticketController.getFinishTicketMessage();
        if (message == "1")
        {
            ticketController.rootticket.tickets.Where(tick => tick.id == item.id)
                                         .Select(tick => { tick.status = "1"; return tick; })
                                         .ToList();
            readtickets();
        }
    }
    void getMessageFromDeleteTicket(Ticket item)
    {
        string message = ticketController.getDeleteTicketMessage();
        if (message == "1")
        {
            taskController.roottask.tasks.RemoveAll(tsk => tsk.idTicket == item.id);
            ticketController.rootticket.tickets.RemoveAll(tick => tick.id == item.id);
            readtickets();
        }
    }
    void getTaskInfo(Task task)
    {
        string text = "TaskID:" + task.id + "\nTask Category:" + task.category + "\nCreation Date:" + task.creationdate + "\nTask's ticket:" + task.tickettitle + "\nTask Owner:" + task.taskOwner;
        TaskTitle.text = task.tasktitle;
        TaskInfos.text = text;
    }
    void getMessageFromUpateTaskCategory(string message, Task item)
    {
        string TaskCategoryUpdateMessage = "";
        TaskCategoryUpdateMessage = message;
        if (TaskCategoryUpdateMessage == "1")
        {
            taskController.roottask.tasks.Where(tsk => tsk.id == item.id)
                                               .Select(tsk => { if (item.idCategory == 7) { tsk.category = "InProgress"; } else { tsk.category = "Done"; }; tsk.idCategory = item.idCategory == 7 ? 8 : 9; return tsk; })
                                               .ToList();
        }
    }
    void getMessageFromDeleteTask(string message, Task item)
    {
        string DeleteTaskMessage = "";
        DeleteTaskMessage = message;
        if (DeleteTaskMessage == "1")
        {
            taskController.roottask.tasks.RemoveAll(tsk => tsk.id == item.id);
        }
    }
    void getMessageFromGiveTaskToADeveloper(string message, Task item)
    {
        string TaskToADeveloperMessage = "";
        TaskToADeveloperMessage = message;
        if (TaskToADeveloperMessage == "1")
        {
            taskController.roottask.tasks.Where(tsk => tsk.id == item.id)
                                               .Select(tsk => { tsk.idDeveloper = dev.Id; tsk.taskOwner = dev.Shortname; return tsk; })
                                               .ToList();
        }
    }
    void getTaskListInfo(List<Task> task)
    {
        int childs = TaskListParentPanel.transform.childCount;
        for (int i = 0; i < childs; i++)
        {
            GameObject.Destroy(TaskListParentPanel.transform.GetChild(i).gameObject);
        }
        taskController = FindObjectOfType<TaskController>();
        foreach (Task item in task)
        {
            GameObject TaskListElements = (GameObject)Instantiate(prefabTaskButtonElements);
            TaskListElements.transform.SetParent(TaskListParentPanel, false);
            TaskListElements.transform.localScale = new Vector3(1, 1, 1);
            int n = TaskListElements.transform.childCount;
            for (int j = 0; j < n; j++)
            {
                if (j == 0)
                {
                    Text text = TaskListElements.transform.GetChild(j).GetComponent<Text>();
                    text.text = item.tasktitle;
                }
                else if(j == 1)
                {
                    Button ThrowRight = TaskListElements.transform.GetChild(j).GetComponent<Button>();
                    ThrowRight.onClick.AddListener(() => taskController.callupdateTaskStatus(item.id));
                    ThrowRight.onClick.AddListener(() => getMessageFromUpateTaskCategory(taskController.getUpdateTaskStatusMessage(), item));
                    ThrowRight.onClick.AddListener(() => taskController.callGiveTaskToADeveloper(item.id, dev.Id));
                    ThrowRight.onClick.AddListener(() => getMessageFromGiveTaskToADeveloper(taskController.getUpdateTaskToADeveloperMessage(), item));
                    ThrowRight.onClick.AddListener(() => TaskListManager.transform.GetComponent<Manager>().Pause());
                }
                else if(j == 2)
                {
                    Button TaskInfo = TaskListElements.transform.GetChild(j).GetComponent<Button>();
                    TaskInfo.onClick.AddListener(() => getTaskInfo(item));
                    TaskInfo.onClick.AddListener(() => InformationTaskManager.transform.GetComponent<Manager>().PauseForInformationTask());
                    
                }
                else if(j == 3)
                {
                    Button DeleteTask = TaskListElements.transform.GetChild(j).GetComponent<Button>();
                    DeleteTask.onClick.AddListener(() => taskController.calldeletetask(item.id));
                    DeleteTask.onClick.AddListener(() => getMessageFromDeleteTask(taskController.getDeleteTaskMessage(), item));
                    DeleteTask.onClick.AddListener(() => TaskListManager.transform.GetComponent<Manager>().Pause());
                }
            }
        }
    }
}