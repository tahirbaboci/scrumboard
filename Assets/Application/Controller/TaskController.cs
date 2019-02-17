using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskController : MonoBehaviour {

    WWW tasksData;
    WWW updateTaskStatusData;
    WWW taskOwnerData;
    WWW deleteTheTask;
    WWW TaskToDeveloper;
    WWW addTask;

    TaskOwnerManager taskOwnerManager;

    public GameObject dropdownTicketObject;
    Dropdown dropdownTicket;
    int m_DropdownValue;
    TicketController ticketcontroller;

    public InputField id;
    public InputField tasktitle;
    public InputField idcategory;
    public InputField idtask;
    public Button addTaskButton;

    private JsonData itemData;

    public RootTask roottask;
    public Task task;
    public List<Task> filteredTasks;

    public GameObject AddTaskPopupGameObjectManager;
    PopUpScript popUp;

    public string message;
    string idTicket;

    private void Start()
    {
        taskOwnerManager = FindObjectOfType<TaskOwnerManager>();
    }

    public void calladdtask()
    {
        StartCoroutine(addtask());
    }
    public void calldeletetask(int TaskID)
    {
        StartCoroutine(deleteTask(TaskID));
    }
    public void callreadtasks()
    {
        StartCoroutine(readtasks());
    }
    public void callupdateTaskStatus(int taskID)
    {
        StartCoroutine(updateTaskStatus(taskID));
    }
    public void callGiveTaskToADeveloper(int taskID, int idDeveloper)
    {
        StartCoroutine(giveTaskToADeveloper(taskID, idDeveloper));
    }

    public void generateDropdown()
    {
        ticketcontroller = FindObjectOfType<TicketController>();
        dropdownTicket = dropdownTicketObject.GetComponent<Dropdown>();
        dropdownTicket.ClearOptions();
        List<string> data = new List<string>();
        foreach (Ticket item in ticketcontroller.rootticket.tickets)
        {
            data.Add(item.id.ToString());
        }
        dropdownTicket.AddOptions(data);
    }
    IEnumerator addtask()
    {
        m_DropdownValue = dropdownTicket.value;
        idTicket = dropdownTicket.options[m_DropdownValue].text;

        WWWForm form = new WWWForm();
        form.AddField("tasktitle", tasktitle.text);
        form.AddField("idticket", idTicket);

        addTask = new WWW("http://192.168.1.32/scrumboard/taskservices/create.php", form);
        while (addTask.isDone == false)
        {
            if (addTask.isDone == true) break;
        }
        if (!string.IsNullOrEmpty(addTask.error))
        {
            popUp = AddTaskPopupGameObjectManager.GetComponent<PopUpScript>();
            popUp.Show(addTask.error);
        }
        addTaskToList();
        yield return addTask;
    }
    public string getAddTaskMessage()
    {
        popUp = AddTaskPopupGameObjectManager.GetComponent<PopUpScript>();
        itemData = JsonMapper.ToObject(addTask.text);
        int n = itemData.Count;
        if (n > 0)
        {
            popUp.Show(itemData["status_message"].ToString());
            return itemData["status"].ToString();
        }
        else
        {
            popUp.Show("NoDataWasFound");
            return "NoDataWasFound";
        }
    }
    public void addTaskToList()
    {
        if (getAddTaskMessage() == "1")
        {
            task = null;
            task = new Task();
            task.tasktitle = tasktitle.text;
            task.idCategory = 7;
            task.idTicket = int.Parse(idTicket);
            roottask.tasks.Add(task);
            tasktitle.text = "";
        }
    }

    IEnumerator deleteTask(int TaskID)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", TaskID);
        deleteTheTask = new WWW("http://192.168.1.32/scrumboard/taskservices/delete.php", form);
       
        while (deleteTheTask.isDone == false)
        {
            if (deleteTheTask.isDone == true) break;
        }
        yield return deleteTheTask;
    }
    public string getDeleteTaskMessage()
    {
        itemData = JsonMapper.ToObject(deleteTheTask.text);
        int n = itemData.Count;
        if (n > 0)
        {
            return itemData["status"].ToString();
        }
        else
        {
            return "NoDataWasFound";
        }
    }
    IEnumerator updateTaskStatus(int taskID)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", taskID);
        updateTaskStatusData = new WWW("http://192.168.1.32/scrumboard/taskservices/update_category.php", form);
        while (updateTaskStatusData.isDone == false)
        {
            if (updateTaskStatusData.isDone == true) break;
        }
        yield return updateTaskStatusData;
    }
    public string getUpdateTaskStatusMessage()
    {
        itemData = JsonMapper.ToObject(updateTaskStatusData.text);
        int n = itemData.Count;
        if (n > 0)
        {
            return itemData["status"].ToString();
        }
        else
        {
            return "NoDataWasFound";
        }
    }
    IEnumerator giveTaskToADeveloper(int taskID, int idDeveloper)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", taskID);
        form.AddField("idDeveloper", idDeveloper);
        TaskToDeveloper = new WWW("http://192.168.1.32/scrumboard/taskservices/update_TaskToDeveloper.php", form);
        while (TaskToDeveloper.isDone == false)
        {
            if (TaskToDeveloper.isDone == true) break;
        }
        yield return TaskToDeveloper;
    }
    public string getUpdateTaskToADeveloperMessage()
    {
        itemData = JsonMapper.ToObject(TaskToDeveloper.text);
        int n = itemData.Count;
        if (n > 0)
        {
            return itemData["status"].ToString();
        }
        else
        {
            return "NoDataWasFound";
        }
    }
    IEnumerator readtasks()
    {
        tasksData = new WWW("http://192.168.1.32/scrumboard/taskservices/read.php");
        //WaitUntil a = new WaitUntil(() => tasksData.isDone == true);
        while (tasksData.isDone == false)
        {
            if (tasksData.isDone == true) break;
        }
        gettasks();
        yield return tasksData;
    }
    public void gettasks()
    {
        roottask = null;
        task = null;
        if (roottask == null)
        {
            roottask = new RootTask();
        }
        itemData = JsonMapper.ToObject(tasksData.text);
        int n = itemData["tasks"].Count;
        for (int i = 0; i < n; i++)
        {
            task = new Task();
            task.id = int.Parse(itemData["tasks"][i]["id"].ToString());
            task.tasktitle = itemData["tasks"][i]["tasktitle"].ToString();
            task.creationdate = itemData["tasks"][i]["creationdate"].ToString();
            task.idCategory = int.Parse(itemData["tasks"][i]["idCategory"].ToString());
            task.idTicket = int.Parse(itemData["tasks"][i]["idTicket"].ToString());
            task.category = itemData["tasks"][i]["category"].ToString();
            task.tickettitle = itemData["tasks"][i]["tickettitle"].ToString();
            task.taskOwner = taskOwnerManager.callreadTaskOwner(int.Parse(itemData["tasks"][i]["id"].ToString()));
            roottask.tasks.Add(task);
        }
    }
    public List<Task> getTasksByTicketIdAndCategory(int ticketId, int idCategory)
    {
        filteredTasks = new List<Task>();
        foreach (Task item in roottask.tasks)
        {
            if (item.idTicket == ticketId && item.idCategory == idCategory)
            {
                filteredTasks.Add(item);
            }
        }
        return filteredTasks;
    }
}
