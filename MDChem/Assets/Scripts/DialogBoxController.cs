using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxController : MonoBehaviour {

	public static int counter = 0;

	public GameObject dropPanel,dragPanel;
	void Start () {
		
		if(counter > 0){
			gameObject.SetActive(false);
			dropPanel.SetActive(true);
			dragPanel.SetActive(true);
		}
		
		counter++;
	}
	

}
