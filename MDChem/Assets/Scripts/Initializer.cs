using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour {
	/*
	This script will initialize various elements for levels 1 - 4
	 */
	void Awake(){
		
		Application.targetFrameRate = 60;
	}

	void Start () {
		if(SceneManager.GetActiveScene().name.Equals("Level1a") || SceneManager.GetActiveScene().name.Equals("Level2a") || SceneManager.GetActiveScene().name.Equals("Level3a") || SceneManager.GetActiveScene().name.Equals("Level4a")){
			Score.scoreValue = 0;
        	DragHandler.gravity = -20;
			Spawner.stop = false;
		}else if(SceneManager.GetActiveScene().name.Equals("Level1b") || SceneManager.GetActiveScene().name.Equals("Level2b") || SceneManager.GetActiveScene().name.Equals("Level3b") || SceneManager.GetActiveScene().name.Equals("Level4b")){
			Score.scoreValue = 0;
        	DragHandler.gravity = -25;
			Spawner.stop = false;
		}else if(SceneManager.GetActiveScene().name.Equals("Level1c") || SceneManager.GetActiveScene().name.Equals("Level2c") || SceneManager.GetActiveScene().name.Equals("Level3c") || SceneManager.GetActiveScene().name.Equals("Level4c")){
			Score.scoreValue = 0;
        	DragHandler.gravity = -30;
			Spawner.stop = false;
		}
	}
	
}
