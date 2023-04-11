using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public static LevelManager _instance;
    public List<Character> characters;

    public Transform canvasIndicator;
    public int alive ;

    public int currentLevel;
    [SerializeField] private NavMeshData []navMeshDatas;
    [SerializeField] private NavMeshData currentNavMesh;
    private void Awake()
    {
        _instance = this;
        canvasIndicator = GameObject.FindGameObjectWithTag(Constant.NAME_CANVAS_INDICATOR).transform;
    }

    public void OnReset()
    {
        player.OnInit();
        characters.Add(player);
        alive = 20;
        for (int i = 0; i < BotManager._instance.bots.Count; i++)
        {
             BotManager._instance.bots[i].OnDespawn();
            SimplePool.Despawn(BotManager._instance.bots[i]);
            BotManager._instance.bots[i].OnDespawnBotName(BotManager._instance.bots[i]);
            BotManager._instance.bots[i].isCanMove = false;

        }
        BotManager._instance.bots.Clear();
       // SimplePool.CollectAll();
    }

    public void LoseGame()
    {
        characters.Clear();
        OnReset();
        //OnLoadLevel(levelIndex);
        UIManager.Ins.OpenUI<Loses>().CloseDirectly();
        BotManager._instance.SpawnBot(alive, BotManager._instance.realBot);
        UIManager.Ins.OpenUI<MainMenu>();
    }

    public void NextLevelGame()
    {
        characters.Clear();
        OnReset();
        //OnLoadLevel(levelIndex);
        UIManager.Ins.OpenUI<Win>().CloseDirectly();
        BotManager._instance.SpawnBot(alive, BotManager._instance.realBot);
        NextLevel();
        UIManager.Ins.OpenUI<MainMenu>();
    }

    public void NextLevel()
    {
        currentLevel++;
        currentNavMesh = navMeshDatas[currentLevel - 1];
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(currentNavMesh);
    }
    public void RemoveTarget(Character character)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i]._listTarget.Contains(character))
            {
                characters[i]._listTarget.Remove(character);
            }
        }
    }
}
