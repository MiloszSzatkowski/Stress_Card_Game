using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debbug_Logger_Script : MonoBehaviour {

	public void Debug_Logger (string inputText){
		GameObject.Find("Debug_Logging_Text").GetComponent<UnityEngine.UI.Text>().text = inputText;
	}
	
}
