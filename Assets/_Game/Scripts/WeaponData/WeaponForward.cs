using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponForward : WeaponCtl
{

    [SerializeField] protected Vector3 currentPostion;
    
    void Start()
    {
        currentPostion = _character.transform.position;
    }
    private void Update()
    {
        MoveForward();
    }

    public void MoveForward()
    {
        if (!hasUpdatedPosition)
        {
            currentPostion = _character.transform.position;
            hasUpdatedPosition = true;
        }

        if (Vector3.Distance(transform.position, currentPostion) < _character.ATT_RANGE)
        {
            transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            hasUpdatedPosition = false;
            OnDespawn();
        }
    }

}
