using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotName : GameUnit
{
    public List<string> listNameBot = new List<string>();
    public Text text;
    int rand;
    public Transform target;
    Vector3 viewPoint;
    public Vector3 offset;
    public void GetName()
    {
        rand = Random.Range(0, listNameBot.Count);
        text.text = listNameBot[rand].ToString();
    }

    public void SetName(string name)
    {
        text.text = name;
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        viewPoint = Camera.main.WorldToScreenPoint(target.position + offset);
        text.gameObject.SetActive(true);
        if (transform.position != viewPoint)
        {
            transform.position = Vector3.Lerp(transform.position, viewPoint, Time.deltaTime * 66);
        }
    }
}
