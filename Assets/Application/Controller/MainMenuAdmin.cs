using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAdmin : MonoBehaviour {

    public Text userDisplay = null;
    Developer dev = Developer.GetInstalce();

    public void Start()
    {
        if (DBManager.LoggedIn)
        {
            userDisplay.text = "User: " + DBManager.shortname;
        }
    }
    public void logOutUser()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void registerDeveloper() {
    }
    public void OnDestroy()
    {
        exit();
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
        //Debug.Log(www.text);

        //StartCoroutine(update_isloggedin());
    }

}
