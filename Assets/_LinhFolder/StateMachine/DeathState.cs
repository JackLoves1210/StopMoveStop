using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IState<Bot>
{
    float timer = 0f;
    float time;
    public void OnEnter(Bot t)
    {
        timer = 2f;
        t.ChangeAnim(Constant.ANIM_DEATH);
        t.OnMoveStop();
    }

    public void OnExecute(Bot t)
    {
        if (time > timer)
        {
            SimplePool.Despawn(t);
        //    SimplePool.Spawn<Bot>(t, Vector3.zero, Quaternion.identity);
            time = 0;
        }
        time += Time.deltaTime;
        
    }

    public void OnExit(Bot t)
    {

    }

}
