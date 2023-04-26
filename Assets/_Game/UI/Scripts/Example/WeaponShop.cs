using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIExample;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : UICanvas
{

    [SerializeField] TextMeshProUGUI nameWeapon;
    [SerializeField] ButtonState buttonState;
    public Transform weaponTranform;
    public int index = 0;
    [SerializeField] private Text textCoin;
    [SerializeField] private Text textPrice;
    public int[] prices;

    public void ButtonBuy()
    {
        if (UserData.Ins.coin > prices[index])
        {
            UserData.Ins.coin -= prices[index];
            PlayerPrefs.SetInt("Coin", UserData.Ins.coin);
            PlayerPrefs.Save();
            SetCoin(UserData.Ins.coin);
            buttonState.SetState(ButtonState.State.Equip);
            buttonState.shopWeaponStatus[index] = ButtonState.State.Equip;
        }
       
    }
    public void ButtonAds()
    {
        buttonState.SetState(ButtonState.State.Equip);
        buttonState.shopWeaponStatus[index] = ButtonState.State.Equip;
    }
    public void ButtonEquip()
    {
        buttonState.SetState(ButtonState.State.Equiped);
        LevelManager.Ins.player.ChangeWeapon(index);
        buttonState.shopWeaponStatus[index] = ButtonState.State.Equiped;
        for (int i = 0; i < buttonState.shopWeaponStatus.Length; i++)
        {
            if (buttonState.shopWeaponStatus[i] == ButtonState.State.Equiped && i != index)
            {
                buttonState.shopWeaponStatus[i] = ButtonState.State.Equip;
            }
        }
    }
    public void ButtonNext()
    {   
        index = index + 1 > ItemManager.Ins.weaponTypes.Length - 1 ? 0 : index + 1;
        buttonState.SetState(buttonState.shopWeaponStatus[index]);
        nameWeapon.SetText(ItemManager.Ins.weaponTypes[index]._name.ToString());
        Destroy(weaponTranform.GetChild(0).gameObject);
        GameObject obj = Instantiate(ItemManager.Ins.weaponTypes[index]._weapon, weaponTranform.position, Quaternion.identity);
        obj.transform.SetParent(weaponTranform);
        SetPrice(prices[index]);
    }
    public void ButtomPre()
    {
        index = index - 1 < 0 ? ItemManager.Ins.weaponTypes.Length - 1 : index -1 ;
        buttonState.SetState(buttonState.shopWeaponStatus[index ]);
        nameWeapon.SetText(ItemManager.Ins.weaponTypes[index]._name.ToString());
        Destroy(weaponTranform.GetChild(0).gameObject);
        GameObject obj = Instantiate(ItemManager.Ins.weaponTypes[index]._weapon, weaponTranform.position, Quaternion.identity);
        obj.transform.SetParent(weaponTranform);
        SetPrice(prices[index]);
    }
    public override void Open()
    {
        base.Open();
        buttonState.SetState(buttonState.shopWeaponStatus[index]);
        GameObject obj = Instantiate(ItemManager.Ins.weaponTypes[index]._weapon, weaponTranform.position, Quaternion.identity);
        obj.transform.SetParent(weaponTranform);
        CameraFollow.Ins.ChangeState(CameraFollow.State.Shop);
        nameWeapon.SetText(ItemManager.Ins.weaponTypes[index]._name.ToString());
        UserData.Ins.coin = PlayerPrefs.GetInt("Coin", 0);
        SetCoin(UserData.Ins.coin);
        LevelManager.Ins.stateGame = LevelManager.StateGame.Shop;
    }
    public override void Close(float delayTime)
    {
        base.Close(delayTime);
        foreach (Transform child in weaponTranform)
        {
            Destroy(child.gameObject);
        }
        CameraFollow.Ins.ChangeState(CameraFollow.State.MainMenu);
        UIManager.Ins.OpenUI<MainMenu>();
    }
    public void SetCoin(int coin)
    {
        textCoin.text = coin.ToString();
    }
    public void SetPrice(int price)
    {
        textPrice.text = price.ToString();
    }
}
