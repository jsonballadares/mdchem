using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreButtonSkin : UI_Template
{
    [SerializeField]
    protected store_button my_store_button;

    [SerializeField]
    protected Image myImage;
    [SerializeField]
    protected TextMeshProUGUI myNameText;
    [SerializeField]
    protected TextMeshProUGUI myPriceText;
    [SerializeField]
    protected TextMeshProUGUI myCountText;
    [SerializeField]
    protected TextMeshProUGUI myBuyText;

    protected override void LoadUISkin()
    {
        base.LoadUISkin();

        if (myImage)
            myImage.sprite = currentUISkin.storeItemBK;

        my_store_button.can_buy_ico = currentUISkin.storeItemCanBuyIco;
        my_store_button.cant_buy_ico = currentUISkin.storeItemCantBuyIco;
        my_store_button.virtual_money_ico = currentUISkin.GetIcon(UI_Skin.Icon.VirtualMoney);
        my_store_button.real_money_ico = currentUISkin.GetIcon(UI_Skin.Icon.RealMoney);

        if (my_store_button.give_this_selected == store_button.give_this.new_live)
            my_store_button.my_ico = currentUISkin.GetIcon(UI_Skin.Icon.Lives);
        else if (my_store_button.give_this_selected == store_button.give_this.virtual_money)
            my_store_button.my_ico = currentUISkin.GetIcon(UI_Skin.Icon.VirtualMoney);
        else if (my_store_button.give_this_selected == store_button.give_this.continue_token)
            my_store_button.my_ico = currentUISkin.GetIcon(UI_Skin.Icon.ContinueToken);
        else if (my_store_button.give_this_selected == store_button.give_this.unlock_world)
            my_store_button.my_ico = currentUISkin.GetIcon(UI_Skin.Icon.UnlockWorld);

        my_store_button.UpdateSkin();


        UpdateText(myNameText, currentUISkin.storeItemName);
        UpdateText(myPriceText, currentUISkin.storeItemPrice);
        UpdateText(myCountText, currentUISkin.storeItemQuantity);
        UpdateText(myBuyText, currentUISkin.storeItemBuyText);



    }
}
