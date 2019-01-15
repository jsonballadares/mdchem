using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonSkin : UI_Template
{
    [SerializeField]
    protected Image myImage;
    [SerializeField]
    protected TextMeshProUGUI myText;
    [SerializeField]
    protected TextMeshProUGUI myCountText;

    protected override void LoadUISkin()
    {
        base.LoadUISkin();

        ButtonOnOff(true);

        if (myText)
            UpdateText(myText, currentUISkin.buttonText);

        if (myCountText)
            UpdateText(myCountText, currentUISkin.buttonCountText);

        
    }

    public void ButtonOnOff(bool isOn)
        {
        if (myImage)
            {
            if (isOn)
                UpdateImage(myImage, currentUISkin.buttonSprite);
            else
                UpdateImage(myImage, currentUISkin.buttonSpriteOff);
            }

        }
}
