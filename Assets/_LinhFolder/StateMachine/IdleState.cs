using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    float timer;
    float time = 0f;
    float durationTimeAttack = 1.1f;
    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Constant.ANIM_IDLE);
    }

    public void OnExecute(Bot t)
    {
        timer = Random.Range(3f, 6f);
        if (t.isCanMove)
        {
            if (time > timer && t._listTarget.Count <= 0)
            {
                t.ChangeState(new PatrolState());
                time = 0f;
            }
            else if (t._listTarget.Count > 0 && time > durationTimeAttack)
            {
                t.ChangeState(new AttackState());
                time = 0f;
            }
            time += Time.deltaTime;
            
        }
        if (t.IsDead)
        {
            t.ChangeState(new DeathState());
        }
    }

    public void OnExit(Bot t)
    {

    }

}
