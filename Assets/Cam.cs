using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    private ObjectFader objectFader;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject zoneCheck;
    private void Update()
    {

        if (player != null)
        {
            Vector3 dir = player.transform.position - transform.position;
            Ray ray = new Ray(transform.position, dir);
            RaycastHit hit;
            Debug.DrawRay(transform.position, dir, Color.blue);
            if (Physics.Raycast(ray, out hit))
            {
                
                // Debug.Log(hit.collider.transform.position);
                if (hit.collider == null)
                {
                    return;
                }
                if (hit.collider.gameObject == player || hit.collider.gameObject == zoneCheck)
                {
                    if (objectFader != null)
                    {
                        objectFader.DoFade = false;
                    }
                }
                else
                {
                    objectFader = hit.collider.gameObject.GetComponent<ObjectFader>();
                    if (objectFader != null)
                    {
                        objectFader.DoFade = true;
                    }
                }
            }
        }
    }
}
