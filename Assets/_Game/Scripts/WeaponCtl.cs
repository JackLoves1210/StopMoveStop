using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtl : GameUnit
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Character _character;
    public void Oninit(Character character , Vector3 target)
    {
        this._character = character;
        transform.forward = (target - transform.position).normalized;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position , _character.transform.position) < _character._rangeAttack)
        {
            transform.forward =new Vector3(transform.forward.x, 0, transform.forward.z);
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            OnDespawn();
        }
        
    }

    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER) || other.CompareTag(Constant.TAG_BLOCK))
        {
            OnDespawn();
            other.GetComponent<Character>().OnDeath();
            _character._listTarget.Remove(other.gameObject);   
        }
    }
}
