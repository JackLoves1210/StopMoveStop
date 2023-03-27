using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : GameUnit
{
    [SerializeField] Text nameTxt;
    public Transform target;
    public Vector3 offset;
    Vector3 viewPoint;
    
    private void LateUpdate()
    {
        viewPoint = Camera.main.WorldToScreenPoint(target.position + offset);
        nameTxt.gameObject.SetActive(true);
        if (transform.position != viewPoint)
        {
            transform.position =  Vector3.Lerp(transform.position, viewPoint,Time.deltaTime *60f);
        }
    }

    public void SetName(string name)
    {
        nameTxt.text = name;
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
