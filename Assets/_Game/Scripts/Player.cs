using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotateSpeed;
    
    public bool _isMove;
    public bool _isCanAttack;

    private float _timeRate = 1.1f;
    private float _time = 0f;

   
    private WeaponCtl _obj;
    public bool isCanMove;

    private void Update()
    {
        if (this.IsDead)
        {
            this.OnDeath();
        }
        _time += Time.deltaTime;
        if (!this.IsDead)
        {
            if (LevelManager._instance.alive == 1)
            {
                ChangeAnim(Constant.ANIM_VICTORY);
                return;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isMove = false;
                _time = 1.1f;
            }
            else if (!_isMove && _listTarget.Count > 0)
            {
                if (_time >= _timeRate)
                {
                    OnAttack();
                    _time = 0f;
                }

            }
            else if (!_isMove)
            {
                ChangeAnim(Constant.ANIM_IDLE);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                indexWeapon++;
                if (indexWeapon == weaponTypes.Length)
                {
                    indexWeapon = 0;
                }
                ChangeWeapon(indexWeapon);
            }
            
        }
    }

    void FixedUpdate()
    {
        Move();
    }


    public override void OnInit()
    {
        base.OnInit();
        ResetPosition();
        this.SetIndicator(this);
        botName.SetName();
        OnEnableWeapon(_weaponType);
        IsDead = false;
    }

    private void ResetPosition()
    {
        TF.position = Vector3.zero;
        TF.rotation = Quaternion.Euler(new Vector3(0,180,0));
    }
    private void Move()
    {
        if (isCanMove)
        {
            if (Input.GetMouseButton(0) && JoystickControl.direct != Vector3.zero)
            {
                _isMove = true;
                _rb.MovePosition(_rb.position + JoystickControl.direct * _moveSpeed * Time.fixedDeltaTime);
                ChangeAnim(Constant.ANIM_RUN);
                Vector3 direction = Vector3.RotateTowards(transform.forward, JoystickControl.direct, _rotateSpeed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }  
    }

    public override void ChangeWeapon(int index)
    {
        Debug.Log(_weaponType.typeWeapon);
        base.ChangeWeapon(index);
      //  int randIndex = Random.Range(0, weaponTypes.Length);
        _weaponType = weaponTypes[index];
        
        OnEnableWeapon(_weaponType);
    }

    public override void SetIndicator(Character character)
    {
        base.SetIndicator(character);
    }
    public override void OnAttack()
    {
        base.OnAttack();
        StartCoroutine(DoSpawnWeapon());
    }

    public override void AddTarget(Character character)
    {
        base.AddTarget(character);
        //  Debug.Log(GetClosestTarget());
        character.SetMask(true);
    }

    public override void RemoveTarget(Character character)
    {
        base.RemoveTarget(character);
        character.SetMask(false);
    }
    IEnumerator DoSpawnWeapon()
    {

        float timerate = 0.4f;
        float _time_2 = 0; ;

        while (_time_2 < timerate)
        {
            _time_2 += Time.deltaTime;
            yield return null;
            if (Input.GetMouseButton(0))
            {
                goto Lable;
            }
        }
        this.SpawnWeapon();
        Lable:
        yield return null;
    }

    public override void SpawnWeapon()
    {
        base.SpawnWeapon();
    }
    public override void OnDeath()
    {
         base.OnDeath();
         isCanMove = false;
         LevelManager._instance.RemoveTarget(this);
         UIManager.Ins.OpenUI<Loses>();
         UIManager.Ins.OpenUI<GamePlay>().CloseDirectly();
         this.ClearTarget(this);
    }

    public void OnRevive()
    {
        IsDead = false;
        Debug.Log("Revive");
        ChangeAnim("idle");
    }
}
