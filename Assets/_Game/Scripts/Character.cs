      using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] public Animator _animator;
    [SerializeField] public GameObject _target;
    [SerializeField] public List<GameObject> _listTarget = new List<GameObject>();
    [SerializeField] public Transform tf;
    [SerializeField] public WeaponType _WeaponType;
    [SerializeField] public Transform _weaponTransform;
    private GameObject modelWeapon;
    string _currentAnim;


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
    public Vector3 GetDirectionTaget()
    {
        Vector3 closestTarget = _listTarget[Constant.FRIST_INDEX].transform.position;
        float closestDistance = Vector3.Distance(tf.position , closestTarget);
        for (int i = 0; i < _listTarget.Count; i++)
        {
            float distance = Vector3.Distance(tf.position, _listTarget[i].transform.position);
            if (distance < closestDistance)
            {
                closestTarget = _listTarget[i].transform.position;
                closestDistance = distance;
            }
        }

        Vector3 directionToTarget = closestTarget - tf.position;
        Vector3 normalizedDirection = directionToTarget.normalized;
        return normalizedDirection;
    }

    public Vector3 GetClosestTarget()
    {
        Vector3 closestTarget = _listTarget[Constant.FRIST_INDEX].transform.position;
        float closestDistance = Vector3.Distance(tf.position, closestTarget);
        for (int i = 0; i < _listTarget.Count; i++)
        {
            float distance = Vector3.Distance(tf.position, _listTarget[i].transform.position);
            if (distance < closestDistance)
            {
                closestTarget = _listTarget[i].transform.position;
                closestDistance = distance;
            }
        }
        return closestTarget;
    }
    public void ChangAnim(string animName)
    {
        if (_currentAnim != animName)
        {
            _animator.ResetTrigger(animName);
            _currentAnim = animName;
            _animator.SetTrigger(_currentAnim);
        }
    }

    public void ResetAnim()
    {
        ChangAnim("");
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

    public virtual void Attack()
    {
        ChangAnim(Constant.ANIM_ATTACK);
    }

    public virtual void Range()
    {

    }

    public virtual void Death()
    {
        ChangAnim(Constant.ANIM_DEATH);
       // ResetAnim();
    }
}
