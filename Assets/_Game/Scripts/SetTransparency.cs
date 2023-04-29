

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class SetTransparency : MonoBehaviour
{

    public float defaultTransparency = 1f;
    public float fadeDuration = 3f;

    float currentTransparency;
    float toFadeTo;
    float tempDist;
    bool isFadingUp;
    bool isFadingDown;



    void Start()
    {
        currentTransparency = defaultTransparency;
        ApplyTransparency();
    }

    void FixedUpdate()
    {
        if (isFadingUp)
        {
            if (currentTransparency < toFadeTo)
            {
                currentTransparency += (tempDist / fadeDuration) * Time.deltaTime;
                ApplyTransparency();
            }
            else
            {
                isFadingUp = false;
            }
        }
        else if (isFadingDown)
        {
            if (currentTransparency > toFadeTo)
            {
                currentTransparency -= (tempDist / fadeDuration) * Time.deltaTime;
                ApplyTransparency();
            }
            else
            {
                isFadingDown = false;
            }
        }
    }

    void ApplyTransparency()
    {
        GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, currentTransparency);
    }



    public void SetT(float newT)
    {
        currentTransparency = newT;
        ApplyTransparency();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))// && other.GetComponent<Character>() is Player)
        {
           
            FadeT(0.2f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))// && other.GetComponent<Character>() is Player)
        {
           
            FadeT(0.9f);
        }
    }
    public void FadeT(float newT)
    {
        toFadeTo = newT;
        if (currentTransparency < toFadeTo)
        {
            tempDist = toFadeTo - currentTransparency;
            isFadingUp = true;
        }
        else
        {
            tempDist = currentTransparency - toFadeTo;
            isFadingDown = true;
        }
    }


}