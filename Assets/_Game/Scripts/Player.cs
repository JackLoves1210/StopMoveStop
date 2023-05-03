using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Transform tailTranform;
    public GameObject modelWing;
    public Transform wingTranform;

    public bool _isMove;
    public bool _isCanAttack;

    private float _timeRate = 1.1f;
    private float _time = 0f;

    public bool isCanMove;

    private void Update()
    {
        switch (LevelManager.Ins.stateGame)
        {
            case LevelManager.StateGame.MainMenu:
                this.ChangeAnim(Constant.ANIM_IDLE);
                break;
            case LevelManager.StateGame.Shop:
                this.ChangeAnim(Constant.ANIM_VICTORY);
                break;
            case LevelManager.StateGame.GamePlay:
                if (this.IsDead)
                {
                    this.OnDeath();
                }
                _time += Time.deltaTime;
                if (!this.IsDead)
                {
                    if (LevelManager.Ins.alive == 1)
                    {
                        isCanMove = false;
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
                        SetSize(this.size + 0.1f);
                    }

                }
                break;
            default:
                break;
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
        SetName();
        OnEnableWeapon(_weaponType);
        IsDead = false;
    }

    public void SetName()
    {
        botName.SetColor(botName.colors[1]);
        if (PlayerPrefs.HasKey(UserData.Key_NamePlayer))
        {
            botName.SetName(PlayerPrefs.GetString(UserData.Key_NamePlayer));
        }
        else
        {
            botName.SetName("You");
        }
    }
    public void TryClosest(UIShop uIShop)
    {
        
        switch (uIShop.currentItemBar)
        {
            case 0:
                ChangeHat(uIShop.GetIndexItem(uIShop.currentItem));
               // Debug.Log("ChangHat : " + uIShop.indexItem);
                break;
            case 1:
                ChangePant(uIShop.indexItem - ItemManager.Ins.hatTypes.Length + 1);
               // Debug.Log("ChangPant : "+ (uIShop.indexItem - ItemManager.Ins.hatTypes.Length + 1));
                break;
            case 2:
                ChangeAccessory(uIShop.indexItem - ItemManager.Ins.hatTypes.Length - ItemManager.Ins.pantTypes.Length + 2);
               // Debug.Log("ChangAccessory : "+ (uIShop.indexItem - ItemManager.Ins.hatTypes.Length - ItemManager.Ins.pantTypes.Length + 2));
                break;
            case 3:
                ChangeSkin(uIShop.indexItem - ItemManager.Ins.hatTypes.Length - ItemManager.Ins.pantTypes.Length - ItemManager.Ins.accessoryTypes.Length + 3);
               // Debug.Log("ChangAccessory : " + (uIShop.indexItem - ItemManager.Ins.hatTypes.Length - ItemManager.Ins.pantTypes.Length - ItemManager.Ins.accessoryTypes.Length + 3));
                break;
            default:
                break;
               
        }  
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

    public override void ChangeSkin(int index)
    {
        base.ChangeSkin(index);

        // change hat
        if (hatType != null)
        {
            Destroy(hatType);
        }
        hatType = Instantiate(ItemManager.Ins.skinTypes[index].modelHat);
        hatType.transform.SetParent(hatTranform, false);
        // change pant
        modelPant.transform.GetComponent<Renderer>().material = ItemManager.Ins.skinTypes[index].materialPant;
        // change skin
        modelSkin.GetComponent<Renderer>().material = ItemManager.Ins.skinTypes[index].materialSelf;
        if (index != 1)
        {
            if (accessoryType != null)
            {
                Destroy(accessoryType);
            }
            accessoryType = Instantiate(ItemManager.Ins.skinTypes[index].modelAcessory);
            accessoryType.transform.SetParent(accessoryTranform, false);
        }
        else
        {
            accessoryType = Instantiate(ItemManager.Ins.skinTypes[index].modelAcessory);
            accessoryType.transform.SetParent(tailTranform, false);
        }
        // change accessory
        
        // change wing
        if (modelWing != null)
        {
            Destroy(modelWing);
        }
        modelWing = Instantiate(ItemManager.Ins.skinTypes[index].modelWing);
        modelWing.transform.SetParent(wingTranform, false);

    }
    public override void ChangeWeapon(int index)
    {
        base.ChangeWeapon(index);
      //  int randIndex = Random.Range(0, weaponTypes.Length);
        _weaponType = ItemManager.Ins.weaponTypes[index];
        
        OnEnableWeapon(_weaponType);
    }

    public override void ChangeHat(int index)
    {
        base.ChangeHat(index);
        if (hatType != null)
        {
            Destroy(hatType);
        }
        hatType = Instantiate(ItemManager.Ins.hatTypes[index]);
        hatType.transform.SetParent(hatTranform, false);
    }
    public override void ChangeAccessory(int index)
    {
        base.ChangeAccessory(index);
        if (hatType != null)
        {
            Destroy(accessoryType);
        }
        accessoryType = Instantiate(ItemManager.Ins.accessoryTypes[index]);
        accessoryType.transform.SetParent(accessoryTranform, false);
    }
    public override void ChangePant(int index)
    {
        base.ChangePant(index);
        modelPant.transform.GetComponent<Renderer>().material = ItemManager.Ins.pantTypes[index];
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
         LevelManager.Ins.RemoveTarget(this);
         UIManager.Ins.OpenUI<Loses>();
         UIManager.Ins.OpenUI<GamePlay>().CloseDirectly();
         this.ClearTarget(this);
    }

    public override void SetSize(float size)
    {
        base.SetSize(size);
        this.ATT_RANGE += 0.35f;
        CameraFollow.Ins.SetRateOffset((this.size - MIN_SIZE)/ (2*MAX_SIZE - MIN_SIZE));
    }
    public void OnRevive()
    {
        base.OnInit();
        ResetPosition();
        this.SetIndicator(this);
        SetName();
        this.SetSize(1);
        IsDead = false;
        ATT_RANGE = 5f;
        _moveSpeed = 5f;
       // _weaponType._wreaponPrefab.transform.localScale =Vector3.one;
    }

}
