using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkinType")]
public class Skin : ScriptableObject
{
    public Material materialSelf;
    public GameObject modelHat;
    public Material materialPant;
    public GameObject modelAcessory;
    public GameObject modelWing;

    public PoolType poolType;
}
