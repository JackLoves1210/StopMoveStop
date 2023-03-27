using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtl : GameUnit
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Character _character;
    [SerializeField] Vector3 currentPostion;
    bool hasUpdatedPosition = false;
    bool OnMove;
    public void Oninit(Character character , Vector3 target)
    {
        this._character = character;
        TF.forward = (target - TF.position).normalized;
    }

    private void Start()
    {
        
    }
    private void Update()
    {

        if (!hasUpdatedPosition)
        {
            currentPostion = _character.transform.position;
            hasUpdatedPosition = true;
        }

        if (Vector3.Distance(transform.position , currentPostion) < _character._rangeAttack)
        {
            transform.forward =new Vector3(transform.forward.x, 0, transform.forward.z);
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            hasUpdatedPosition = false;
            OnDespawn();
        }

        
    }

    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER) && other.GetComponent<Character>() != _character)
        {
            OnDespawn();
            other.GetComponent<Character>().IsDead = true;
            _character.RemoveTarget(other.GetComponent<Character>());
            hasUpdatedPosition = false;
        }
    }
}
