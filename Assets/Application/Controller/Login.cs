using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {

    WWW loginData;

    public InputField shortName;
    public InputField password;
    public Button loginButton;
    public static int id;

    public Text shortnameAttention;
    public Text passwordAttention;

    public Developer dev;
    PopUpScript popUp;
    string message;

    string[] webresult;

    public bool loginSuccessfully = false;

    public void callLoginUser()
    {
        StartCoroutine(LoginUser());
    }

    IEnumerator LoginUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("shortname", shortName.text);
        form.AddField("password", password.text);
        loginData = new WWW("http://192.168.1.32/scrumboard/loginServices/login.php", form);
        while (loginData.isDone == false)
        {
            if (loginData.isDone == true) break;
        }
        webresult = loginData.text.Split('\t');
        string check = webresult[0];
        if (check == "0")
        {
            loginSuccessfully = true;
        }
        login();
        yield return loginData;
    }
    public void login()
    {
        if (!string.IsNullOrEmpty(loginData.error))
        {
            popUp = GetComponent<PopUpScript>();
            popUp.Show(loginData.error);
        }
        else if (loginSuccessfully)
        {
            id = int.Parse(webresult[1]);
            DBManager.shortname = shortName.text;
            dev = Developer.GetInstalce();
            dev.Id = id;
            dev.Shortname = shortName.text;
            if (shortName.text == "TB")
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
        }
        else
        {
            message = loginData.text;
            popUp = GetComponent<PopUpScript>();
            popUp.Show(message);

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
        //Debug.Log(www.text);

        //StartCoroutine(update_isloggedin());
    }
    public void verifyInputs()
    {
        bool passwordLengthIsCorrect = false;
        bool shortNameLengthIsCorrect = false;

        if (password.text.Length >= 8)
        {
            passwordLengthIsCorrect = true;
            passwordAttention.text = "";
        }
        else
        {
            passwordAttention.text = "attention: Password length must be higher the 8.";
        }

        if (shortName.text.Length >= 2 && shortName.text.Length <= 5)
        {
            shortNameLengthIsCorrect = true;
            shortnameAttention.text = "";
        }
        else
        {
            shortnameAttention.text = "attention: shortname must be more the 2 and lower the 5 letters.";
        }

       
        loginButton.interactable = (shortNameLengthIsCorrect && passwordLengthIsCorrect);
    }
}
