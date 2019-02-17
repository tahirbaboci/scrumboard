using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class TaskOwnerManager : MonoBehaviour {

    WWW taskOwnerData;
    private JsonData itemData;

    public string callreadTaskOwner(int id)
    {
        StartCoroutine(readTaskOwner(id));
        return getReadTaskOwner();
    }

    IEnumerator readTaskOwner(int id)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        taskOwnerData = new WWW("http://192.168.1.32/scrumboard/taskservices/find_taskOwner.php", form);
        //WaitUntil a = new WaitUntil(() => taskOwnerData.isDone == true);
        while (taskOwnerData.isDone == false)
        {
            if (taskOwnerData.isDone == true) break;
        }
        yield return taskOwnerData;
    }
    public string getReadTaskOwner()
    {
        itemData = JsonMapper.ToObject(taskOwnerData.text);
        int n = itemData.Count;
        if (n > 0)
        {
            return itemData["shortname"].ToString();
        }
        else
        {
            return "NoDataWasFound";
        }
    }
}
