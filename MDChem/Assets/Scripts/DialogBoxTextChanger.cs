using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogBoxTextChanger : MonoBehaviour
{
    public Text dialogText;

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null)
        {
            if (SceneManager.GetActiveScene().name.Equals("Level6"))
            {
                if (DraggableManager.count == 0)
                {
                    dialogText.text = "Out of the pool of elements drag and drop all the +1 ions to the avaliable slots above. Once you do the level will allow you to progress.";
                }
                else if (DraggableManager.count == 1)
                {
                    dialogText.text = "Out of the pool of elements drag and drop all the +2 ions to the avaliable slots above. Once you do the level will allow you to progress.";
                }
            }
            else if (SceneManager.GetActiveScene().name.Equals("Level7"))
            {
                if (DraggableManager.count == 0)
                {
                    dialogText.text = "Out of the pool of elements drag and drop all the -1 ions to the avaliable slots above. Once you do the level will allow you to progress.";
                }
                else if (DraggableManager.count == 1)
                {
                    dialogText.text = "Out of the pool of elements drag and drop all the -2 ions to the avaliable slots above. Once you do the level will allow you to progress.";
                }
                else if (DraggableManager.count == 2)
                {
                    dialogText.text = "Out of the pool of elements drag and drop all the -3 ions to the avaliable slots above. Once you do the level will allow you to progress.";
                }
            }

        }

    }
}
