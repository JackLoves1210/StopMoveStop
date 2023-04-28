using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : UICanvas
{
    [SerializeField] private Transform[] contentItem;
    [SerializeField] private ButtonState buttonState;
    [SerializeField] private GameObject[] itemBar;
    [SerializeField] private List<ShopItem> shopItems;
    public List<ShopItem.State> shopStateItem;
    public int currentItemBar;
    public ShopItem currentItem;
    public   int indexItem;
    [SerializeField] private Text textCoin;
    [SerializeField] private Text textPrice;

    private void Start()
    {
        LevelManager.Ins.stateGame = LevelManager.StateGame.Shop;
    }
    public void GetState()
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            if (shopItems[i].state != ShopItem.State.Selected)
            {
                shopStateItem[i] = shopItems[i].state;
            } 
        }
        
    }
    public int GetIndexItem(ShopItem currentItem)
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            if (shopItems[i] == currentItem)
            {
                return i;
            }
        }
        return 0 ;
    }
    public void SetState(ShopItem.State currentItem)
    {
        this.currentItem.state = currentItem;
    }
    public void SelectItem(ShopItem item)
    {
        GetState();
        for (int i = 0; i < shopItems.Count; i++)
        {
            shopItems[i].SetState(shopStateItem[i]); 
        }
        indexItem = GetIndexItem(item);
        currentItem = item;
        switch (currentItem.state)
        {
            case ShopItem.State.Buy:
                buttonState.SetState(ButtonState.State.Buy);
                break;
            case ShopItem.State.Bought:
                buttonState.SetState(ButtonState.State.Equip);
                break;
            case ShopItem.State.Equip:
                buttonState.SetState(ButtonState.State.Equiped);
                break;
            case ShopItem.State.Selected:
                break;
            default:
                break;
        }
        LevelManager.Ins.player.TryClosest(this);
        currentItem.SetState(ShopItem.State.Selected);
        SetPrice(currentItem.price);
    }
    public void ButtonBuy()
    {
        if (UserData.Ins.coin > currentItem.price)
        {
            UserData.Ins.coin -= currentItem.price;
            PlayerPrefs.SetInt("Coin", UserData.Ins.coin);
            PlayerPrefs.Save();
            SetCoin(UserData.Ins.coin);
            buttonState.SetState(ButtonState.State.Equip);
            currentItem.SetState(ShopItem.State.Bought);
            GetState();
            for (int i = 0; i < shopItems.Count; i++)
            {
                shopItems[i].SetState(shopStateItem[i]);
            }
            UserData.Ins.SetList("ShopItem", shopStateItem);
            PlayerPrefs.Save();
        }
    }
    public void ButtonAds()
    {
        buttonState.SetState(ButtonState.State.Equip);
        currentItem.SetState(ShopItem.State.Bought);
        GetState();
        for (int i = 0; i < shopItems.Count; i++)
        {
            shopItems[i].SetState(shopStateItem[i]);
        }
        UserData.Ins.SetList("ShopItem", shopStateItem);
        PlayerPrefs.Save();
    }
    public void ButtonEquip()
    {
        buttonState.SetState(ButtonState.State.Equiped);
        currentItem.SetState(ShopItem.State.Equip);
        if (currentItemBar == 0 && indexItem <= ItemManager.Ins.hatTypes.Length - 2)
        {
            LevelManager.Ins.player.ChangeHat(indexItem);
            PlayerPrefs.SetInt(UserData.Keys_Hat_Data, indexItem);
            CheckItemEquiped(0, ItemManager.Ins.hatTypes.Length - 1);
        }
        else if (currentItemBar == 1 && indexItem < ItemManager.Ins.hatTypes.Length + ItemManager.Ins.pantTypes.Length - 2)
        {
            LevelManager.Ins.player.ChangePant(indexItem - ItemManager.Ins.hatTypes.Length + 1);
            PlayerPrefs.SetInt(UserData.Keys_Pant_Data, indexItem - ItemManager.Ins.hatTypes.Length + 1);
            CheckItemEquiped(ItemManager.Ins.hatTypes.Length - 1, ItemManager.Ins.hatTypes.Length + ItemManager.Ins.pantTypes.Length - 2);
        }

        else if (currentItemBar == 2 && indexItem < ItemManager.Ins.hatTypes.Length + ItemManager.Ins.pantTypes.Length + ItemManager.Ins.accessoryTypes.Length - 3)
        {
            LevelManager.Ins.player.ChangeAccessory(indexItem - ItemManager.Ins.hatTypes.Length - ItemManager.Ins.pantTypes.Length + 2);
            PlayerPrefs.SetInt(UserData.Keys_Accessory_Data, indexItem - ItemManager.Ins.hatTypes.Length - ItemManager.Ins.pantTypes.Length + 2);
            CheckItemEquiped(ItemManager.Ins.hatTypes.Length + ItemManager.Ins.pantTypes.Length - 2, ItemManager.Ins.hatTypes.Length + ItemManager.Ins.pantTypes.Length + ItemManager.Ins.accessoryTypes.Length - 3);
        }
        else if (currentItemBar == 3 && indexItem >= ItemManager.Ins.hatTypes.Length + ItemManager.Ins.pantTypes.Length + ItemManager.Ins.accessoryTypes.Length - 3) 
        {
            LevelManager.Ins.player.ChangeSkin(indexItem - ItemManager.Ins.hatTypes.Length - ItemManager.Ins.pantTypes.Length - ItemManager.Ins.accessoryTypes.Length + 3);
            PlayerPrefs.SetInt(UserData.Keys_Skin_Data, indexItem - ItemManager.Ins.hatTypes.Length - ItemManager.Ins.pantTypes.Length - ItemManager.Ins.accessoryTypes.Length + 3);
            UserData.Ins.idPlayerSkin = PlayerPrefs.GetInt(UserData.Keys_Skin_Data, 0); ;
            CheckItemEquiped(0, shopItems.Count);
        }
        GetState();
        for (int i = 0; i < shopItems.Count; i++)
        {
            shopItems[i].SetState(shopStateItem[i]);
        }
        UserData.Ins.SetList("ShopItem", shopStateItem);
        PlayerPrefs.Save();
    }
    public void ButtonHatShop()
    {
        LoadShopItem(0);
        currentItemBar = 0;
    }
    public void ButtonPantShop()
    {
        LoadShopItem(1);
        currentItemBar = 1;
    }
    public void ButtonAccessoryShop()
    {
        LoadShopItem(2);
        currentItemBar = 2;
    }
    public void ButtonSkinShop()
    {
        LoadShopItem(3);
        currentItemBar = 3;
    }
    public override void Open()
    {
        base.Open();
        UserData.Ins.coin = PlayerPrefs.GetInt("Coin", 0);
        SetCoin(UserData.Ins.coin);
        CameraFollow.Ins.ChangeState(CameraFollow.State.Shop);
        GetState();
       // PlayerPrefs.DeleteKey("ShopItem");
        shopStateItem = UserData.Ins.GetList("ShopItem", shopStateItem);
        


        // xử lý các item của skin normal
        Debug.Log(UserData.Ins.idPlayerSkin);
        if (UserData.Ins.idPlayerSkin == 0)
        {
            for (int i = 0; i < shopStateItem.Count; i++)
            {
                if (i == UserData.Ins.idPlayerHat || i== UserData.Ins.idPlayerPant + ItemManager.Ins.hatTypes.Length-1 || i == UserData.Ins.idPlayerAccessory + ItemManager.Ins.hatTypes.Length + ItemManager.Ins.pantTypes.Length - 2)
                {
                    shopStateItem[i] = ShopItem.State.Equip;
                }
            }
        }
        for (int i = 0; i < shopItems.Count; i++)
        {
            shopItems[i].SetState(shopStateItem[i]);
        }
        LevelManager.Ins.stateGame = LevelManager.StateGame.Shop;
    }
    public override void Close(float delayTime)
    {
        base.Close(delayTime);
        CameraFollow.Ins.ChangeState(CameraFollow.State.MainMenu);
        ReturnShopItemEquiped();
    }

    public void ReturnShopItemEquiped()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        LevelManager.Ins.player.ChangeSkin(0);
        LevelManager.Ins.player.ChangeHat(ItemManager.Ins.hatTypes.Length - 1);
        LevelManager.Ins.player.ChangePant(ItemManager.Ins.pantTypes.Length - 1);
        LevelManager.Ins.player.ChangeAccessory(ItemManager.Ins.accessoryTypes.Length - 1);
        for (int i = 0; i < shopStateItem.Count; i++)
        {
            if (shopStateItem[i] == ShopItem.State.Equip)
            {
                if (i >= 0 && i < ItemManager.Ins.hatTypes.Length - 1) LevelManager.Ins.player.ChangeHat(i);
                if (i >= ItemManager.Ins.hatTypes.Length - 1 && i < ItemManager.Ins.hatTypes.Length + ItemManager.Ins.pantTypes.Length - 2)
                    LevelManager.Ins.player.ChangePant(i - ItemManager.Ins.hatTypes.Length + 1);
                if (i >= ItemManager.Ins.hatTypes.Length + ItemManager.Ins.pantTypes.Length - 2 && i < ItemManager.Ins.hatTypes.Length + ItemManager.Ins.pantTypes.Length + ItemManager.Ins.accessoryTypes.Length - 3)
                    LevelManager.Ins.player.ChangeAccessory(i - ItemManager.Ins.hatTypes.Length - ItemManager.Ins.pantTypes.Length + 2);
                if (i >= ItemManager.Ins.hatTypes.Length + ItemManager.Ins.pantTypes.Length + ItemManager.Ins.accessoryTypes.Length - 3 && i != 20)
                    LevelManager.Ins.player.ChangeSkin(i - ItemManager.Ins.hatTypes.Length - ItemManager.Ins.pantTypes.Length - ItemManager.Ins.accessoryTypes.Length + 3);
            }
        }
    }
    private void LoadShopItem(int index)
    {
        if (itemBar[index].transform.GetChild(0).gameObject.activeSelf)
        {
            itemBar[index].transform.GetChild(0).gameObject.SetActive(false);
        }
        for (int i = 0; i < itemBar.Length; i++)
        {
            if (i != index)
            {
                itemBar[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        contentItem[index].gameObject.SetActive(true);
        for (int i = 0; i < contentItem.Length; i++)
        {
            if (i != index)
            {
                contentItem[i].gameObject.SetActive(false);
            }
        }
        buttonState.UnState();
        //currentItem.SetState(ShopItem.State.Buy);
    }
    public void CheckItemEquiped(int j , int n)
    {
        for (int i = j ; i < n; i++)
        {
            if (shopStateItem[i] == ShopItem.State.Equip && i != indexItem)
            {
                shopItems[i].state = ShopItem.State.Bought;
                // GetState();
                shopItems[i].SetState(ShopItem.State.Bought);
              
            }
        }
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
