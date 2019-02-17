using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chooseScript : MonoBehaviour {

    TicketController ticketController;
    BoardController boardController;

    public GameObject dropdownSprintObject;
    public GameObject dropdownProjectObject;
    Dropdown dropdownSprint;
    Dropdown dropdownProject;

    PopUpScript popUp;
    string message;

    Developer dev;

    //This is the index value of the Dropdown
    int m_DropdownValue;

    ChoosenValues val;

    void Start () {

        dev = Developer.GetInstalce();

        ticketController = FindObjectOfType<TicketController>();
        ticketController.callreadSprint();
        message = ticketController.sprintData.error;
        dropdownSprint = dropdownSprintObject.GetComponent<Dropdown>();
        dropdownSprint.ClearOptions();
        dropdownSprint.AddOptions(ticketController.sprintList);


        boardController = FindObjectOfType<BoardController>();
        boardController.callRead();
        message = message + boardController.boardData.error;
        dropdownProject = dropdownProjectObject.GetComponent<Dropdown>();
        dropdownProject.ClearOptions();
        List<string> data = new List<string>();
        foreach (Board item in boardController.rootboard.boards)
        {
            data.Add(item.Id.ToString());
        }
        dropdownProject.AddOptions(data);

        if (!string.IsNullOrEmpty(ticketController.sprintData.error) && !string.IsNullOrEmpty(boardController.boardData.error))
        {
            popUp = FindObjectOfType<PopUpScript>();
            popUp.Show(message);
        }
    }
    public void getSelectedValues()
    {
        val = ChoosenValues.GetInstalce();
        m_DropdownValue = dropdownSprint.value;
        val.Sprint = dropdownSprint.options[m_DropdownValue].text;


        m_DropdownValue = dropdownProject.value;
        val.Project = dropdownProject.options[m_DropdownValue].text;
        if(dev.Shortname == "TB")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        }
        
    }
    void OnApplicationQuit()
    {
        exit();
    }

    void exit()
    {
        WWWForm form = new WWWForm();
        Debug.Log(dev.Id);
        form.AddField("id", dev.Id);
        form.AddField("isloggedin", 0);
        WWW www = new WWW("http://192.168.1.32/scrumboard/loginServices/updateisloggedin.php", form);
    }
}
