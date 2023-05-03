using System;
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
        
        if (other.CompareTag(Constant.TAG_WALL))
        {
            OnMoveStop();
            Debug.Log(other.name);
        }
        if (other.CompareTag(Constant.TAG_CHARACTER) && other.GetComponent<Character>() != _character)
        {
            OnDespawn();
            other.GetComponent<Character>().IsDead = true;
            _character.RemoveTarget(other.GetComponent<Character>());
            hasUpdatedPosition = false;
            if (_character is Player)
            {
                _character.SetSize(_character.size + 0.1f);
                moveSpeed += 0.25f;
                AudioManager.Ins.Play(Constant.AUDIO_SIZEUP);
                if (AudioManager.Ins.isCanVibrate)
                {
                    Handheld.Vibrate();
                }
            }
            if (other.GetComponent<Character>() is Player)
            {
                AudioManager.Ins.Play(Constant.AUDIO_DEAD);
                
            }
        }
       

    }

    private void OnMoveStop()
    {
        moveSpeed = 0f;
        Invoke(nameof(OnDespawn), 1.1f);
    }
}
