using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponType")]
public class WeaponType : ScriptableObject
{
    public string _name;
    public GameObject _weapon;
    public WeaponCtl _wreaponPrefab;
    public TypeWeapon typeWeapon;
}

public enum TypeWeapon { FowardWeapon, RotateWeapon, Boomerang }