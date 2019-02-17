using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    //public GameObject FirstCanvas;
    //public GameObject SecondCanvas;

    public GameObject CanvasAppear;
    public GameObject CanvasAppear2;
    public GameObject CanvasDisappear;
    public GameObject Canvas2Disappear;
    public GameObject Canvas3Disappear;

    public void Pause()
    {
        if(CanvasDisappear.gameObject == null && CanvasAppear.gameObject.activeInHierarchy == false)
        {
            Canvas2Disappear.SetActive(false);
            Canvas3Disappear.SetActive(false);
            CanvasAppear.SetActive(true);
            Time.timeScale = 0;
        }
        else if(CanvasDisappear.gameObject == null && CanvasAppear.gameObject.activeInHierarchy == true)
        {
            CanvasAppear.SetActive(false);
            Canvas2Disappear.SetActive(true);
            Canvas3Disappear.SetActive(true);
            Time.timeScale = 1;

        }
        else if(CanvasAppear.gameObject.activeInHierarchy == false)
        {
            CanvasDisappear.SetActive(false);
            Canvas2Disappear.SetActive(false);
            Canvas3Disappear.SetActive(false);
            CanvasAppear.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            CanvasAppear.SetActive(false);
            CanvasDisappear.SetActive(true);
            Canvas2Disappear.SetActive(true);
            Canvas3Disappear.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void PauseForInformationTicket()
    {
        if(CanvasAppear.gameObject.activeInHierarchy == false)
        {
            Canvas2Disappear.SetActive(false);
            Canvas3Disappear.SetActive(false);
            CanvasAppear.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            CanvasAppear.SetActive(false);
            Canvas2Disappear.SetActive(true);
            Canvas3Disappear.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void PauseForInformationTask()
    {
        if (CanvasAppear.gameObject.activeInHierarchy == false)
        {
            CanvasDisappear.SetActive(false);
            CanvasAppear.SetActive(true);
            CanvasAppear2.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            CanvasAppear.SetActive(false);
            CanvasAppear2.SetActive(false);
            CanvasDisappear.SetActive(true);
            Time.timeScale = 1;

        }
    }
    public void PauseForTicketDone()
    {
        if (CanvasAppear.gameObject.activeInHierarchy == false)
        {
            CanvasAppear.SetActive(true);
            CanvasDisappear.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            CanvasAppear.SetActive(false);
            CanvasDisappear.SetActive(true);
            Time.timeScale = 1;

        }
    }
}
