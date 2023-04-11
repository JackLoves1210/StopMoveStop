using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotate : MonoBehaviour
{
    public Transform pivotPoint;
    public float rotationSpeed = 700f;

    private void Start()
    {
        pivotPoint = transform;
    }
    void Update()
    {
        transform.RotateAround(pivotPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}

