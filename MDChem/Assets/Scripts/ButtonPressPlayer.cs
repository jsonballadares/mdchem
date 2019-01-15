using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressPlayer : MonoBehaviour {
	
	public void PlayButtonNoise(){
		FindObjectOfType<AudioManager>().Play("buttonnoise");
	}

}
