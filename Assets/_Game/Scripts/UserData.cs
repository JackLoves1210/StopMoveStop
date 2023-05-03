using System;
using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;

public class UserData : Singleton<UserData>
{
    public const string Key_Level = "Level";
    public const string Key_MaxAlive = "MaxAlive";
    public const string Key_Coin = "Coin";
    public const string Key_SoundIsOn = "SoundIsOn";
    public const string Key_Vibrate = "VibrateIsOn";
    //public const string Key_RemoveAds = "RemoveAds";
   // public const string Key_Tutorial = "Tutorial";
    public const string Key_NamePlayer = "Name";

    public const string Key_Player_Weapon = "PlayerWeapon";
    public const string Key_Player_Hat = "PlayerHat";
    public const string Key_Player_Pant = "PlayerPant";
    public const string Key_Player_Accessory = "PlayerAccessory";
    public const string Key_Player_Skin = "PlayerSkin";

    public const string Keys_Weapon_Data = "WeaponDatas";
    public const string Keys_Hat_Data = "HatDatas";
    public const string Keys_Pant_Data = "PantDatas";
    public const string Keys_Accessory_Data = "AccessoryDatas";
    public const string Keys_Skin_Data = "SkinDatas";


    public int level = 0;
    public int coin = 0;

    public bool soundIsOn =true;
    public bool vibrate = true;
   // public bool removeAds = false;
    public bool tutorialed = false;

    public int idPlayerWeapon;
    public int idPlayerHat;
    public int idPlayerPant;
    public int idPlayerAccessory;
    public int idPlayerSkin;

   
    public void SetBool(string key, bool value)
    {
        int intValue = value ? 1 : 0;
        PlayerPrefs.SetInt(key, intValue);
    }
    public bool GetBool(string key)
    {
        int intValue = PlayerPrefs.GetInt(key);
        return intValue == 1;
    }

    static T Get<T>(string key, T defaultValue)
    {
        return JsonUtility.FromJson<T>(PlayerPrefs.GetString(key, JsonUtility.ToJson(defaultValue)));
    }

    static void Set<T>(string key, T value)
    {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(value));
    }
    public class ListWrapper<T>
    {
        public List<T> list = new List<T>();
    }

    public List<T> GetList<T>(string key)
    {
        return Get<ListWrapper<T>>(key, new ListWrapper<T>()).list;
    }

    public List<T> GetList<T>(string key, List<T> defaultValue)
    {
        return Get<ListWrapper<T>>(key, new ListWrapper<T> { list = defaultValue }).list;
    }

    public void SetList<T>(string key, List<T> value)
    {
        Set(key, new ListWrapper<T> { list = value });
    }
    public void OnInitData()
    {
        if (!PlayerPrefs.HasKey(Key_SoundIsOn)) SetBool(Key_SoundIsOn, true);
        soundIsOn = GetBool(Key_SoundIsOn);

        if (!PlayerPrefs.HasKey(Key_Vibrate)) SetBool(Key_Vibrate, true);
        vibrate = GetBool(Key_Vibrate);

        idPlayerSkin = PlayerPrefs.GetInt(Keys_Skin_Data, 0);
        if (idPlayerSkin == 0) LevelManager.Ins.player.ChangeSkin(idPlayerSkin);

        idPlayerWeapon = PlayerPrefs.GetInt(Keys_Weapon_Data, 0);
        LevelManager.Ins.player.ChangeWeapon(idPlayerWeapon);
        idPlayerHat = PlayerPrefs.GetInt(Keys_Hat_Data, ItemManager.Ins.hatTypes.Length-1);
        LevelManager.Ins.player.ChangeHat(idPlayerHat);
        idPlayerPant = PlayerPrefs.GetInt(Keys_Pant_Data, ItemManager.Ins.pantTypes.Length - 1);
        LevelManager.Ins.player.ChangePant(idPlayerPant);
        idPlayerAccessory = PlayerPrefs.GetInt(Keys_Accessory_Data, ItemManager.Ins.accessoryTypes.Length - 1);
        LevelManager.Ins.player.ChangeAccessory(idPlayerAccessory);

        if (idPlayerSkin != 0) LevelManager.Ins.player.ChangeSkin(idPlayerSkin);


    }
    
   
}
