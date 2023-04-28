using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public enum State {Buy , Bought , Selected , Equip }
    public State state;
    [SerializeField] GameObject[] stateObjects;
    [SerializeField] GameObject[] statusItem;
    public Transform child;
    public int price;
    public void SetState(State state)
    {
        for (int i = 0; i < stateObjects.Length; i++)
        {
            stateObjects[i].SetActive(false);
        }
        stateObjects[(int)state].SetActive(true);
        this.state = state;
    }
    public void ButtonSelected(UIShop uIShop)
    {
        if (this.state != State.Selected)
        {
            uIShop.SelectItem(this);
        }
        
    }

    //public Transform FindDeepChild(Transform parent, string name)
    //{
    //    Transform result = parent.Find(name);

    //    if (result != null)
    //        return result;

    //    foreach (Transform child in parent)
    //    {
    //        result = FindDeepChild(child,name);
    //        if (result != null)
    //            return result;
    //    }

    //    return null;
    //}

}
