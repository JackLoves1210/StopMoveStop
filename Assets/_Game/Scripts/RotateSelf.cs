using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    [SerializeField] Transform tf;
    [SerializeField] float angle = -2f;

    private void Start()
    {
        tf = transform;
    }
    private void LateUpdate()
    {
        tf.Rotate(Vector3.up * angle, Space.Self);
    }
}
