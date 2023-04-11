using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtl : GameUnit
{
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected Character _character;
    public bool hasUpdatedPosition = false;
    public virtual void Oninit(Character character, Vector3 target)
    {
        this._character = character;
        TF.forward = (target - TF.position).normalized;
        
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
