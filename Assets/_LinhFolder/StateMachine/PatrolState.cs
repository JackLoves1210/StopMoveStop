using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    float timer;
    float time ;
    public void OnEnter(Bot t)
    {
        time = 0f;
        timer = 1.1f;   
    }

    public void OnExecute(Bot t)
    {
        if (t.IsDead)
        {
            t.ChangeState(new DeathState());
        }
        else
        {
            t.Moving();
            time += Time.deltaTime;
            if (t._listTarget.Count > 0 && time > timer)
            {
                t.OnMoveStop();
                t.ChangeState(new AttackState());
                time = 0f;
            }
        }
    }

    public void OnExit(Bot t)
    {

    }

}
