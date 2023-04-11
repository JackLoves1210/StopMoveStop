using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    //public const float TIME_DELAY_THROW = 0.4f;
    //public const float MAX_SIZE = 4f;
    //public const float MIN_SIZE = 1f;
    //protected float size = 1;


    public const float ATT_RANGE = 5f;
    public Animator _animator;
    public GameObject mask;
    public BotName botName;
    public List<Character> _listTarget = new List<Character>();

    public WeaponType[] weaponTypes;
    public WeaponType _weaponType;
    public Transform _weaponTransform;
    public int indexWeapon = 0;
    private GameObject modelWeapon;

    public Material[] pantTypes;
    public GameObject modelPant;

    public GameObject[] hatTypes;
    public GameObject hatType;
    public Transform hatTranform;

    public float _rangeAttack = 5f;
   
    string _currentAnim;

   // public Transform canvasIndicator;

    public bool IsDead;

    public virtual void OnInit()
    {
        IsDead = false;
        ChangePant();
        ChangeAccessory();
    }

    public virtual void SetIndicator(Character character)
    {
        // LevelManager._instance.canvasIndicator = GameObject.Find(Constant.NAME_CANVAS_INDICATOR).transform;
        if (LevelManager._instance.canvasIndicator != null)
        {
            character.botName = BotNameManager._instance.GetBotNameFormPool();
            character.botName.GetName();
            character.botName.transform.SetParent(LevelManager._instance.canvasIndicator);
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
        Vector3 closestTarget = _listTarget[Constant.FRIST_INDEX].transform.position;
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
            SimplePool.Spawn<WeaponBoommerang>(_weaponType._wreaponPrefab, _weaponTransform.position, Quaternion.identity).Oninit(this, taget);
        }
        else if (this._listTarget.Count > 0 && _weaponType.typeWeapon == TypeWeapon.FowardWeapon )
        {
            SimplePool.Spawn<WeaponForward>(_weaponType._wreaponPrefab, _weaponTransform.position, Quaternion.identity).Oninit(this, taget);
        }
        else if (this._listTarget.Count > 0 && _weaponType.typeWeapon == TypeWeapon.RotateWeapon)
        {
            SimplePool.Spawn<WeaponForward>(_weaponType._wreaponPrefab, _weaponTransform.position, Quaternion.identity).Oninit(this, taget);
        }
    }

    public void ResetAnim()
    {
        ChangeAnim("");
    }
    public virtual void ChangeWeapon(int index)
    {

    }

    public virtual void ChangdeSkin()
    {

    }

    public virtual void ChangeAccessory()
    {
        int index;
        index = UnityEngine.Random.Range(0, pantTypes.Length);
        if (hatType != null)
        {
            Destroy(hatType);
        }
        hatType = Instantiate(hatTypes[hatTypes.Length -1]);
        hatType.transform.SetParent(hatTranform, false);
    }

    public virtual void ChangePant()
    {
        int index;
        index = UnityEngine.Random.Range(0, pantTypes.Length);
        modelPant.transform.GetComponent<Renderer>().material = pantTypes[index];
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
