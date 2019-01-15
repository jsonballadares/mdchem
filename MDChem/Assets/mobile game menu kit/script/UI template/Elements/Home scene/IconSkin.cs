using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconSkin : UI_Template
{
    /*
    public enum Icon
    {
        Start,
        Next,
        Previous,
        Back,

        Credit,
        ScoreRank,
        Achievement,
        NoWiFi,
        Profiles,
        Store,

        Options,
        SoundOn,
        SoundOff,

        Reward,

        Plus,
        Minus,

        StarOn,
        StarOff,

        Lives,
        VirtualMoney,
        RealMoney,

        PadlockSmall,
        PadlockBig,

        GarbageCan,

        Pause,
        PlayVideoAds,
        Retry,
        BackToStageScreen,

    }
    public Icon myIcon;
    */

    public UI_Skin.Icon myIcon;
    protected Image myImage;




    protected override void LoadUISkin()
    {
        if (myImage == null)
            myImage = GetComponent<Image>();

        base.LoadUISkin();

        myImage.sprite = currentUISkin.GetIcon(myIcon);

    }
}
