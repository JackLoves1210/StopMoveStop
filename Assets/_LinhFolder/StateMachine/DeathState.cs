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
        t.ChangeColorOnDead();
        t.isCanMove = false;
        //AudioManager.Ins.Play(Constant.AUDIO_DEAD_1, t.transform);
    }

    public void OnExecute(Bot t)
    {
        if (time > timer)
        {
           
            t.IsDead = false;
            t.ChangeState(new IdleState());
           
            SimplePool.Despawn(t);
            LevelManager.Ins.alive--;
            if (LevelManager.Ins.alive > BotManager._instance.realBot )
            {
                BotManager._instance.StartCoroutine(BotManager._instance.CoroutineSpawnBot());
            }
            if (LevelManager.Ins.alive == 1 && LevelManager.Ins.player.IsDead != true )
            {
                AudioManager.Ins.Play(Constant.AUDIO_VICTORY);
                UIManager.Ins.OpenUI<Win>();
                UIManager.Ins.OpenUI<GamePlay>().CloseDirectly();
            }
            BotManager._instance.bots.Remove(t);
            LevelManager.Ins.characters.Remove(t);
            time = 0;
        }
        time += Time.deltaTime;
        
    }

    public void OnExit(Bot t)
    {
        
    }

}
