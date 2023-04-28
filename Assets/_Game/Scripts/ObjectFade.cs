using System.Collections;
using UnityEngine;

public class ObjectFade : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Material material;
    private Color originalColor;
    private Color originalColorFade;


    private void Start()
    {
        material = GetComponent<Renderer>().material;
        player = FindObjectOfType<Player>().transform;
        originalColor = material.color;
        originalColorFade = material.color;
        originalColorFade.a = 0.4f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform == player )
        {
            FadeIn();
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform == player)
        {
            FadeOut();
        }
    }

    private void FadeOut()
    {
        material.color = Color.Lerp(originalColorFade, originalColor, 1f);
    }  

    private void FadeIn()
    {
        material.color = Color.Lerp(originalColor, originalColorFade, 1f);
    }
}
