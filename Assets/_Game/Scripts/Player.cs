using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotateSpeed;
    
    
   // [SerializeField] private CheckCharacter _checkCharacter;
   // [SerializeField] private GameObject _wpeanponPrefab;
   //[SerializeField] private WeaponCtl _wreaponPrefab;
    public bool _isMove;
    public bool _isCanAttack;

    private float _timeRate = 1.1f;
    private float _time = 0f;


    void Start()
    {
        OnEnableWeapon();
        

    }
    private WeaponCtl _obj;

    private void Update()
    {
        
        _time += Time.deltaTime;
    
        if (Input.GetMouseButtonUp(0))
        {
            _isMove = false;
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
    }

    void FixedUpdate()
    {
        Move();
        
    }

    private void Move()
    {
        if (Input.GetMouseButton(0) && JoystickControl.direct != Vector3.zero)
        {
            _isMove = true;
            _rb.MovePosition(_rb.position + JoystickControl.direct *_moveSpeed * Time.fixedDeltaTime);
            ChangeAnim(Constant.ANIM_RUN);
            Vector3 direction = Vector3.RotateTowards(transform.forward, JoystickControl.direct, _rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
    }
    public override void OnAttack()
    {
        base.OnAttack();
        StartCoroutine(DoSpawnWeapon());
    }

    public override void AddTarget(Character character)
    {
        base.AddTarget(character);
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
       LevelManager._instance.RemoveTarget(this);
    }
}
