using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{

    
    public void OnEnter(Bot t)
    {
        t.StartCoroutine(t.DoAttack());

    }

    public void OnExecute(Bot t)
    {
        if (t._isDead)
        {
            t.ChangeState(new DeathState());
        }
    }

    public void OnExit(Bot t)
    {

    }

}
