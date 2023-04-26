using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public WeaponType[] weaponTypes;
    public Material[] pantTypes;
    public GameObject[] hatTypes;
    public GameObject[] accessoryTypes;
    public Material[] materialsSkin;
    public Skin[] skinTypes;
}
