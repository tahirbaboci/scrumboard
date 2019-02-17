using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpScript : MonoBehaviour {
    public GameObject window;
    public Text messageField;
    //Show (string)
    //Displays the indicared message in a pop-up window
	public void Show (string message) {
        messageField.text = message;
        window.SetActive(true);
		
	}
	// Hide()
	// Closes the pop-up window
	public void Hide () {
        window.SetActive(false);
	}
}
