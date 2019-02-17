using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

    public WWW boardData;

    Board board;
    public RootBoard rootboard;
    private JsonData itemData;

    void Start () {
		
	}

    public void callRead()
    {
        StartCoroutine(read());
    }
    IEnumerator read()
    {
        boardData = new WWW("http://192.168.1.32/scrumboard/boardservices/read.php");
        while (boardData.isDone == false)
        {
            if (boardData.isDone == true) break;
        }
        getReadBoard();
        yield return boardData;
    }
    public void getReadBoard()
    {
        rootboard = null;
        if (rootboard == null)
        {
            rootboard = new RootBoard();
        }
        itemData = JsonMapper.ToObject(boardData.text);
        int n = itemData["boards"].Count;
        if (n > 0)
        {
            for (int i = 0; i < n; i++)
            {
                board = new Board();
                board.Id = int.Parse(itemData["boards"][i]["id"].ToString());
                board.ProjectName = itemData["boards"][i]["projectname"].ToString();
                rootboard.boards.Add(board);
            }

        }
    }
	
}
