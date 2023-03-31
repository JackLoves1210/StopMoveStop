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
    [SerializeField] public Animator _animator;
    //[SerializeField] public Character _target;
    //[SerializeField] public List<Character> _listTarget = new List<Character>();
    [SerializeField] GameObject mask;

    //[SerializeField] private TargetIndicator targetIndicator;
    //[SerializeField] Transform indicatorPoint;

    //[SerializeField] public Character _target;
    [SerializeField] public BotName botName;
    [SerializeField] public List<Character> _listTarget = new List<Character>();


    [SerializeField] public WeaponType _WeaponType;
    [SerializeField] public Transform _weaponTransform;
    [SerializeField] private WeaponCtl _wreaponPrefab;
    public float _rangeAttack = 7f;
    private GameObject modelWeapon;
    string _currentAnim;

    public Transform Canvas;
    //private Vector3 targetPoint;
    public bool IsDead { get; set; }

  //  private Vector3 targetPoint;
    public virtual void OnInit()
    {
        IsDead = false;
        //targetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.TargetIndicator);
        //targetIndicator.SetTarget(indicatorPoint);
        //targetIndicator.SetName("YOU");
        Canvas = GameObject.Find("Canvas_Indicator").transform;
        botName = BotNameManager._instance.GetBotNameFormPool();
        botName.GetName();
        botName.transform.SetParent(Canvas);
        botName.gameObject.SetActive(true);
        botName.target = TF.transform;
    }
    public void OnEnableWeapon()
    {
        if (modelWeapon!= null)
        {
            Destroy(modelWeapon);
        }
        if (_WeaponType._weapon != null)
        {
            modelWeapon = Instantiate ( _WeaponType._weapon);
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
    //public void OnAttack()
    //{
    //    Debug.Log("_time");
    //    float _timeRate = 1.1f;
    //    float _time = 0f;
    //    if (_time >= _timeRate)
    //    {
    //        Attack();
    //        _time = 0f;
    //    }
    //    _time += Time.deltaTime;
    //}

    //public virtual void OnAttack()
    //{
    //    this._target = GetTargetInRange();

    //    if (_target != null && !_target.IsDead/* && currentSkin.Weapon.IsCanAttack*/)
    //    {
    //        targetPoint = _target.transform.position;
    //        transform.LookAt(targetPoint + (transform.position.y - targetPoint.y) * Vector3.up);
    //        ChangeAnim(Constant.ANIM_ATTACK);
    //    }

    //}

    //public Character GetTargetInRange()
    //{
    //    Character target = null;
    //    float distance = float.PositiveInfinity;

    //    for (int i = 0; i < _listTarget.Count; i++)
    //    {
    //        if (_listTarget[i] != null && _listTarget[i] != this && !_listTarget[i].IsDead && Vector3.Distance(transform.position, _listTarget[i].transform.position) <= ATT_RANGE)
    //        {
    //            float dis = Vector3.Distance(transform.position, _listTarget[i].transform.position);

    //            if (dis < distance)
    //            {
    //                distance = dis;
    //                target = _listTarget[i];
    //            }
    //        }
    //    }

    //    return target;
    //}
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

    public virtual void OnDespawn()
    {
        SimplePool.Despawn(botName);
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
        if (this._listTarget.Count > 0)
        {
            Vector3 taget = GetClosestTarget();
            SimplePool.Spawn<WeaponCtl>(_wreaponPrefab, _weaponTransform.position, Quaternion.identity).Oninit(this, taget);
        }
    }

    public void ResetAnim()
    {
        ChangeAnim("");
    }
    public virtual void ChangeWeapon()
    {

    }

    public virtual void ChangdeSkin()
    {

    }

    public virtual void ChangeAccessory()
    {

    }

    public virtual void ChangePant()
    {

    }


    public virtual void Range()
    {

    }

    public virtual void OnDeath()
    {
        ChangeAnim(Constant.ANIM_DEATH);
    }
    
}
