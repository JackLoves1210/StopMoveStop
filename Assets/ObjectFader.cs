using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    public float fadeSpeed=5f, fadeAmount = 0.4f;
    float originalOpacity;
    Material[] mats;
    public bool DoFade = false;
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
        foreach (var mat in mats)
        {
            originalOpacity = mat.color.a;
            SetMaterialTransparent(mat, true);
        }
        
    }

    
    void Update()
    {
        if (DoFade)
        {
            FadeNow();
        }
        else
        {
            ResetFade();
        }
    }

    public void ResetFade()
    {
        foreach (var mat in mats)
        {
            Color currentColor = mat.color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
                Mathf.Lerp(currentColor.a, originalOpacity, fadeSpeed * Time.deltaTime));
            mat.color = smoothColor;
        }
        
    }

    public void FadeNow()
    {
        foreach (var mat in mats)
        {
            Color currentColor = mat.color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
                Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed * Time.deltaTime));
            mat.color = smoothColor;
        }
    }

    private const int MATERIAL_OPAQUE = 0;
    private const int MATERIAL_TRANSPARENT = 1;

    private void SetMaterialTransparent(Material material, bool enabled)
    {
        material.SetFloat("_Surface", enabled ? MATERIAL_TRANSPARENT : MATERIAL_OPAQUE);
        material.SetShaderPassEnabled("SHADOWCASTER", !enabled);
        material.renderQueue = enabled ? 3000 : 2000;
        material.SetFloat("_DstBlend", enabled ? 10 : 0);
        material.SetFloat("_SrcBlend", enabled ? 5 : 1);
        material.SetFloat("_ZWrite", enabled ? 0 : 1);
    }

    public void SetMaterialTrans(Material material)
    {
        material.DisableKeyword("_ALPHATEST_ON");
        UnityEditor.BaseShaderGUI.SurfaceType surfaceType = (UnityEditor.BaseShaderGUI.SurfaceType)material.GetFloat("_Surface");
        if (surfaceType == 0)
        {
            material.SetFloat("_Surface", 1);
        }
    }
}
