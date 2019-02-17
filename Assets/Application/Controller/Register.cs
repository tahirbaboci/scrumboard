using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour {

    public InputField nome;
    public InputField surname;
    public InputField shortName;
    public InputField password;
    public Button registerButton;
    public Text nameSurnameAttention;
    public Text shortnameAttention;
    public Text passwordAttention;

    public GameObject RegisterPopupGameObjectManager;
    PopUpScript popUp;

    public string message;

    public void callRegister()
    {
        StartCoroutine(register());
    }
    IEnumerator register()
    {
        WWWForm form = new WWWForm();
        form.AddField("firstname", nome.text);
        form.AddField("lastname", surname.text);
        form.AddField("shortname", shortName.text);
        form.AddField("password", password.text);
        WWW www = new WWW("http://192.168.1.32/scrumboard/loginServices/register.php", form);
        while (www.isDone == false)
        {
            if (www.isDone == true) break;
        }
        if (!string.IsNullOrEmpty(www.error))
        {
            popUp = RegisterPopupGameObjectManager.GetComponent<PopUpScript>();
            popUp.Show(www.error);
        }
        else if (www.text == "0") {
            popUp = RegisterPopupGameObjectManager.GetComponent<PopUpScript>();
            message = "User Created Successfully.";
            popUp.Show(message);
            nome.text = "";
            surname.text = "";
            shortName.text = "";
            password.text = "";
        }
        else
        {
            popUp = RegisterPopupGameObjectManager.GetComponent<PopUpScript>();
            message = www.text;
            popUp.Show(message);

        }
        yield return www;
    }
    public void verifyInputs()
    {
        bool passwordLengthIsCorrect = false;
        bool shortNameLengthIsCorrect = false;
        bool nameAndSurname = false;

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

        if (nome.text.Length >= 2 && surname.text.Length >= 2)
        {
            nameAndSurname = true;
            nameSurnameAttention.text = "";
        }
        else
        {
            nameSurnameAttention.text = "attention: name and surname must be two or more letters.";
        }


        registerButton.interactable = (nameAndSurname && shortNameLengthIsCorrect && passwordLengthIsCorrect);
    }
}
