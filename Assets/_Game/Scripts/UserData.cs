using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : Singleton<UserData>
{
    public const string Key_Level = "Level";
    public const string Key_Coin = "Coin";
    public const string Key_SoundIsOn = "SoundIsOn";
    public const string Key_Vibrate = "VibrateIsOn";
    public const string Key_RemoveAds = "RemoveAds";
    public const string Key_Tutorial = "Tutorial";
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

    public bool soundIsOn = true;
    public bool vibrate = true;
    public bool removeAds = false;
    public bool tutorialed = false;

    public GameObject playerWeapon;
    public GameObject playerHat;
    public GameObject playerPant;
    public GameObject playerAccessory;
    public GameObject playerSkin;

    //Example
    // UserData.Ins.SetInt(UserData.Key_Level, ref UserData.Ins.level, 1);

    //data for list
    /// <summary>
    ///  0 = lock , 1 = unlock , 2 = selected
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ID"></param>
    /// <param name="state"></param>
    public void SetDataState(string key, int ID, int state)
    {
        PlayerPrefs.SetInt(key + ID, state);
    }
    /// <summary>
    ///  0 = lock , 1 = unlock , 2 = selected
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ID"></param>
    /// <param name="state"></param>
    public int GetDataState(string key, int ID, int state = 0)
    {
        return PlayerPrefs.GetInt(key + ID, state);
    }

    public void SetDataState(string key, int state)
    {
        PlayerPrefs.SetInt(key, state);
    }

    public int GetDataState(string key, int state = 0)
    {
        return PlayerPrefs.GetInt(key, state);
    }

    /// <summary>
    /// Key_Name
    /// if(bool) true == 1
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetIntData(string key, ref int variable, int value)
    {
        variable = value;
        PlayerPrefs.SetInt(key, value);
    }

    public void SetBoolData(string key, ref bool variable, bool value)
    {
        variable = value;
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public void SetFloatData(string key, ref float variable, float value)
    {
        variable = value;
        PlayerPrefs.GetFloat(key, value);
    }

    public void SetStringData(string key, ref string variable, string value)
    {
        variable = value;
        PlayerPrefs.SetString(key, value);
    }

    public void SetEnumData<T>(string key, ref T variable, T value)
    {
        variable = value;
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
    }

    public void SetEnumData<T>(string key, T value)
    {
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
    }


    public T GetEnumData<T>(string key, T defaultValue) where T : Enum
    {
        return (T)Enum.ToObject(typeof(T), PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue)));
    }

    public T GetEnumDatas<T>(string key, T defaultValue)
    {
        return (T)Enum.ToObject(typeof(T), PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue)));
    }

    //public GameObject GetData(string key, GameObject defaultValue)
    //{
    //    return (GameObject)Enum.ToObject(typeof(GameObject), PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue)));
    //}

    public  T GetObject<T>(string key)
    {
        return Get<T>(key, default(T));
    }

    public  T GetObject<T>(string key, T defaultValue)
    {
        return Get<T>(key, defaultValue);
    }

    public  void SetObject<T>(string key, T value)
    {
        Set(key, value);
    }

    static T Get<T>(string key, T defaultValue)
    {
        return JsonUtility.FromJson<T>(PlayerPrefs.GetString(key, JsonUtility.ToJson(defaultValue)));
    }

    static void Set<T>(string key, T value)
    {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(value));
    }

    public void OnInitData()
    {


        level = PlayerPrefs.GetInt(Key_Level, 0);
        coin = PlayerPrefs.GetInt(Key_Coin, 0);

        removeAds = PlayerPrefs.GetInt(Key_RemoveAds, 0) == 1;
        tutorialed = PlayerPrefs.GetInt(Key_Tutorial, 0) == 1;
        soundIsOn = PlayerPrefs.GetInt(Key_SoundIsOn, 0) == 1;
        vibrate = PlayerPrefs.GetInt(Key_Vibrate, 0) == 1;
    }

}
