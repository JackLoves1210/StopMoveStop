using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtl : GameUnit
{
    public static WeaponCtl Ins;
    [SerializeField] protected Character _character;
    public float moveSpeed = 7f;
    public bool hasUpdatedPosition = false;
    public void Awake()
    {
        Ins = this;
    }
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
        if (_character is Player && other.CompareTag(Constant.TAG_CHARACTER) && other.GetComponent<Character>() != _character)
        {
            _character.SetSize(_character.size+0.1f);
            moveSpeed += 0.25f;
        }
        if (other.CompareTag(Constant.TAG_CHARACTER) && other.GetComponent<Character>() != _character)
        {
            OnDespawn();
            other.GetComponent<Character>().IsDead = true; 
            _character.RemoveTarget(other.GetComponent<Character>());
            hasUpdatedPosition = false;
            
        }
        if (other.CompareTag(Constant.TAG_CHARACTER) && other.GetComponent<Character>() is Player && other.GetComponent<Character>() != _character)
        {
            AudioManager.Ins.Play(Constant.AUDIO_DEAD);
        }
    }
}
