using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Transform tf;
    [SerializeField] private WeaponCtl _wreaponPrefab;
 
    private void Update()
    {
        
    }

    private void DoAttack()
    {
        SimplePool.Spawn<WeaponCtl>(_wreaponPrefab, tf.position, Quaternion.Euler(new Vector3(0,-90,90))).Oninit();
    }
}
