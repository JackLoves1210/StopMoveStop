using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtl : GameUnit
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Player _player;
    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }
    public void Oninit()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.GetClosestTarget(), speed * Time.deltaTime);
    }

    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_BOT) || other.CompareTag(Constant.TAG_BLOCK))
        {
            OnDespawn();
        }
    }
}
