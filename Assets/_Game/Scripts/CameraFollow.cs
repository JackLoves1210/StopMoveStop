using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _tf;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _moveSpeed;
    private void LateUpdate()
    {
        _tf.position = Vector3.Lerp(_tf.position, _offset + _target.position, _moveSpeed * Time.deltaTime);
    }

}
