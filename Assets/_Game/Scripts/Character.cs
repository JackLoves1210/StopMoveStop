using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    //public const float TIME_DELAY_THROW = 0.4f;
    public const float MAX_SIZE = 1.8f;
    public const float MIN_SIZE = 1f;
    public float size = 1;


    public float ATT_RANGE = 5f;
    public Animator _animator;
    public GameObject mask;
    public BotName botName;
    public List<Character> _listTarget = new List<Character>();

   // public WeaponType[] weaponTypes;
    public WeaponType _weaponType;
    public Transform _weaponTransform;
    public int indexWeapon = 0;
    private GameObject modelWeapon;

   // public Material[] pantTypes;
    public GameObject modelPant;

    // skin
    public GameObject modelSkin;

  //  public GameObject[] hatTypes;
    public GameObject hatType;
    public Transform hatTranform;

    // accessory
    public GameObject accessoryType;
    public Transform accessoryTranform;
    // material skin
    public Material materialSkin;
   
    string _currentAnim;

   // public Transform canvasIndicator;

    public bool IsDead;

    public virtual void OnInit()
    {
        IsDead = false;
        ClearTarget(this);
    }

    public virtual void SetIndicator(Character character)
    {
        if (LevelManager.Ins.canvasIndicator != null)
        {
            character.botName = BotNameManager._instance.GetBotNameFormPool();
            character.botName.GetName();
            character.botName.SetColor(botName.colors[0]);
            character.botName.transform.SetParent(LevelManager.Ins.canvasIndicator);
            character.botName.gameObject.SetActive(true);
            character.botName.target = TF.transform;
        }
    }
    public void OnEnableWeapon(WeaponType weaponType)
    {
        if (modelWeapon!= null)
        {
            Destroy(modelWeapon);
        }
        if (weaponType._weapon != null)
        {
            modelWeapon = Instantiate (weaponType._weapon);
            modelWeapon.transform.SetParent(_weaponTransform,false);
        }
    }

    public void SetActiveWeapon()
    {
        modelWeapon.SetActive(false);
        Invoke(nameof(IsHaveWeapon), 1f);
    }

    public void IsHaveWeapon()
    {
        modelWeapon.SetActive(true);
    }

    public Vector3 GetDirectionTaget()
    {
        Vector3 closestTarget = _listTarget[Constant.FRIST_INDEX].transform.position;
        float closestDistance = Vector3.Distance(TF.position , closestTarget);
        for (int i = 0; i < _listTarget.Count; i++)
        {
            float distance = Vector3.Distance(TF.position, _listTarget[i].transform.position);
            if (distance < closestDistance)
            {
                closestTarget = _listTarget[i].transform.position;
                closestDistance = distance;
            }
        }
        Vector3 directionToTarget = closestTarget - TF.position;
        Vector3 normalizedDirection = directionToTarget.normalized;
        return normalizedDirection;
    }

    public Vector3 GetClosestTarget()
    {
        Vector3 closestTarget = new Vector3();
        if (_listTarget.Count > 0)
        {
            closestTarget = _listTarget[Constant.FRIST_INDEX].transform.position;
        }
        else return closestTarget;
        float closestDistance = Vector3.Distance(TF.position, closestTarget);
        for (int i = 0; i < _listTarget.Count; i++)
        {
            float distance = Vector3.Distance(TF.position, _listTarget[i].transform.position);
            if (distance < closestDistance)
            {
                closestTarget = _listTarget[i].transform.position;
                closestDistance = distance;
            }
        }
        return closestTarget;
    }
    public void OnDespawnBotName(Character character)
    {
        SimplePool.Despawn(character.botName);
    }
    public virtual void OnDespawn()
    {
        //SimplePool.Despawn(botName);
    }
    public virtual void OnAttack()
    {
        LookEnemy();
        ChangeAnim(Constant.ANIM_ATTACK);
        SetActiveWeapon();
    }

    public void SetMask(bool active)
    {
        mask.SetActive(active);
    }

    public virtual void AddTarget(Character character)
    {
        this._listTarget.Add(character);
    }
    public virtual void RemoveTarget(Character character)
    {
        this._listTarget.Remove(character);
    }
    public void ChangeAnim(string animName)
    {
        if (_currentAnim != animName)
        {
            _animator.ResetTrigger(animName);
            _currentAnim = animName;
            _animator.SetTrigger(_currentAnim);
        }
    }

    public void LookEnemy() 
    {
       
        if (_listTarget.Count > 0)
        {
            Vector3 direction = GetDirectionTaget();
            direction.y = 0f;
            TF.rotation = Quaternion.LookRotation(direction);
        }
    }

    public virtual void SpawnWeapon()
    {
        Vector3 taget = GetClosestTarget();
        if (this._listTarget.Count > 0 && _weaponType.typeWeapon == TypeWeapon.Boomerang)
        {
            WeaponBoommerang weapon = SimplePool.Spawn<WeaponBoommerang>(_weaponType._wreaponPrefab, _weaponTransform.position, Quaternion.identity);
            weapon.transform.localScale = this.size * Vector3.one;
            weapon.moveSpeed = 7 * size;
            weapon.Oninit(this, taget);
        }
        else if (this._listTarget.Count > 0 && _weaponType.typeWeapon == TypeWeapon.FowardWeapon)
        {
            WeaponForward weapon = SimplePool.Spawn<WeaponForward>(_weaponType._wreaponPrefab, _weaponTransform.position, Quaternion.identity);
            weapon.transform.localScale = this.size * Vector3.one;
            weapon.moveSpeed = 7*size;
            weapon.Oninit(this, taget);
        }
        else if (this._listTarget.Count > 0 && _weaponType.typeWeapon == TypeWeapon.RotateWeapon)
        {
            WeaponForward weapon = SimplePool.Spawn<WeaponForward>(_weaponType._wreaponPrefab, _weaponTransform.position, Quaternion.identity);
            weapon.transform.localScale = this.size * Vector3.one;
            weapon.moveSpeed = 7*size;
            weapon.Oninit(this, taget);
        }
    }

    public virtual void SetSize(float size)
    {
        size = Mathf.Clamp(size, MIN_SIZE, MAX_SIZE);
        this.size = size;
        TF.localScale = size * Vector3.one;
    }
    public void ResetAnim()
    {
        ChangeAnim("");
    }
    public virtual void ChangeWeapon(int index)
    {

    }

    public virtual void ChangeSkin(int index)
    {

    }

    public virtual void ChangeHat(int index)
    {
       
    }

    public virtual void ChangeAccessory(int index)
    {
        
    }

    public virtual void ChangePant(int index)
    {
    
    }


    public virtual void Range()
    {

    }

    public virtual void OnDeath()
    {
        ChangeAnim(Constant.ANIM_DEATH);
        SimplePool.Despawn(botName);
        
    }

    public void ClearTarget(Character character)
    {
        character._listTarget.Clear();
    }
}
