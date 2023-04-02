using System.Collections;
using System.Collections.Generic;
using UIExample;
using Unity;
using UnityEngine;

public class DeathState : IState<Bot>
{
    float timer = 0f;
    float time;
    public void OnEnter(Bot t)
    {
        timer = 2f;
        t.OnDeath();
    }

    public void OnExecute(Bot t)
    {
        if (time > timer)
        {
           
            t.IsDead = false;
            SimplePool.Despawn(t);
            
            LevelManager._instance.alive--;
            if (LevelManager._instance.alive > BotManager._instance.realBot )
            {
                BotManager._instance.StartCoroutine(BotManager._instance.CoroutineSpawnBot());
            }
            if (LevelManager._instance.alive == 1 )
            {
                UIManager.Ins.OpenUI<Win>();
                UIManager.Ins.OpenUI<GamePlay>().CloseDirectly();
            }
                BotManager._instance.bots.Remove(t);
            time = 0;
        }
        time += Time.deltaTime;
        
    }

    public void OnExit(Bot t)
    {

    }

}
