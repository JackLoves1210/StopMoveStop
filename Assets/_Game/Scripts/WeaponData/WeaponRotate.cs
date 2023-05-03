using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotate : MonoBehaviour
{
    public Transform pivotPoint;
    public float rotationSpeed = 700f;
   [SerializeField] bool isRotate = true;

    private void Start()
    {
        pivotPoint = transform;
        isRotate = true;
    }
    void Update()
    {
        if (isRotate)
        {
            transform.RotateAround(pivotPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_WALL))
        {
            isRotate = false;
            Invoke(nameof(ResetRotate), 1.1f);
        }
    }

    void ResetRotate()
    {
        isRotate = true;
    }
}

