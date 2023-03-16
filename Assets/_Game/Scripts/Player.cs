using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private CheckCharacter _checkCharacter;
   // [SerializeField] private GameObject _wpeanponPrefab;
    [SerializeField] private WeaponCtl _wreaponPrefab;
    public bool _isMove;
    public bool _isCanAttack;

    private float _timeRate = 1.1f;
    private float _time = 1.1f;
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
                Attack();
                _time = 0f;
            }

            if (_listTarget.Count > 0)
            {
                Vector3 direction = GetDirectionTaget();
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
        else if (!_isMove)
        {
            ChangAnim(Constant.ANIM_IDLE);
        }
        StartCoroutine(WeaponMoveToTarget());
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
            ChangAnim(Constant.ANIM_RUN);
            Vector3 direction = Vector3.RotateTowards(transform.forward, JoystickControl.direct, _rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);

        }
    }

    private void DoAttack()
    {
        
    }
    public override void Attack()
    {
        base.Attack();
        // _obj = Instantiate(_wpeanponPrefab, _weaponTransform.position, Quaternion.Euler(new Vector3(0, -90, 90)));
        StartCoroutine(SpawnWeapon());
        


    }

    IEnumerator WeaponMoveToTarget()
    {
       
        if (_obj != null && _listTarget.Count > 0)
        {
            Vector3 closestTarget = GetClosestTarget();
            float distance = Vector3.Distance(_obj.transform.position, closestTarget);
            float speed = 1f;
            Vector3 targetDirection = GetDirectionTaget();
            while (distance > 0.1f)
            {
                _obj.transform.position = Vector3.MoveTowards(_obj.transform.position, closestTarget,speed*Time.deltaTime);
                float distance_1 = Vector3.Distance(transform.position, _obj.transform.position);
                if (distance_1 > 6f)
                {
                    _obj.OnDespawn();
                }
                yield return null;
            }
            
        }
        yield return null;
    }
    IEnumerator SpawnWeapon()
    {
        float timeRate = 0.4f;
        float _time_2 = 0; ;
        while (_time_2 < timeRate)
        {
            _time_2 += Time.deltaTime;
            yield return null;
        }
        _obj = SimplePool.Spawn<WeaponCtl>(_wreaponPrefab, _weaponTransform.position, Quaternion.identity);
        yield return null;
    }
}
