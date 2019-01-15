using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementItem : MonoBehaviour {

    public Image background;
    public Image icon;
    public Text myName;
    public Text description;
    //reward
    public GameObject rewardObj;
    public Transform starReward;
    public Transform moneyReward;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitiateMe(Sprite _background, Sprite _icon, string _name, string _description, int _starReward, int _moneyReward)
    {
        myName.text = _name;

        UpdateMe(_background, _icon, _description);
        
        if (_starReward == 0 && _moneyReward == 0)
        {
            rewardObj.SetActive(false);
            return;
        }

        if (_starReward > 0)
            starReward.GetChild(0).GetComponent<Text>().text = _starReward.ToString();
        else
            starReward.gameObject.SetActive(false);

        if (_moneyReward > 0)
            moneyReward.GetChild(0).GetComponent<Text>().text = _moneyReward.ToString();
        else
            moneyReward.gameObject.SetActive(false);
    }

    public void UpdateMe(Sprite _background, Sprite _icon, string _description)
    {
        background.sprite = _background;
        icon.sprite = _icon;
        description.text = _description;
    }
}
