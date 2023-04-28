using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UIExample;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;
    public List<Character> characters;
    public enum StateGame { MainMenu, Shop, GamePlay }
    public StateGame stateGame;
    public Transform canvasIndicator;
    public int alive ;
    public int maxAlive;
    public int currentLevel;
    //[SerializeField] public NavMeshData []navMeshDatas;
    //[SerializeField] public NavMeshData currentNavMesh;
    [SerializeField] private Map[] mapFrefab;
   // [SerializeField] private Map currentMap;
    [SerializeField] private List<Map> maps;
    private void Awake()
    {
        canvasIndicator = GameObject.FindGameObjectWithTag(Constant.NAME_CANVAS_INDICATOR).transform;
        UserData.Ins.OnInitData();
        PlayerPrefs.DeleteKey(UserData.Key_Level);
        PlayerPrefs.DeleteKey(UserData.Key_MaxAlive);
        if (PlayerPrefs.HasKey(UserData.Key_Level))
        {
            currentLevel = PlayerPrefs.GetInt(UserData.Key_Level);
        }
        else currentLevel = 1;
        for (int i = 0; i < mapFrefab.Length; i++)
        {
            Map map =SimplePool.Spawn<Map>(mapFrefab[i]);
            map.gameObject.SetActive(false);
            maps.Add(map);
        }
        maps[currentLevel - 1].gameObject.SetActive(true);
        //currentMap = SimplePool.Spawn<Map>(maps[currentLevel - 1]);
        maxAlive = PlayerPrefs.GetInt(UserData.Key_MaxAlive, 10);
        alive = maxAlive;
    }

    public void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("s");
            SimplePool.Despawn(maps[currentLevel - 1]);
            

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("s");
            currentLevel++;
            maps[currentLevel - 1].gameObject.SetActive(true);

        }
    }
    public void OnReset()
    {
        player.OnRevive();
        characters.Add(player);
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
        alive = maxAlive;
        PlayerPrefs.SetInt(UserData.Key_MaxAlive, maxAlive);
        //OnLoadLevel(levelIndex);
        UIManager.Ins.OpenUI<Loses>().CloseDirectly();
        BotManager._instance.SpawnBot(alive, BotManager._instance.realBot);
        UIManager.Ins.OpenUI<MainMenu>();
    }

    public void NextLevelGame()
    {
        characters.Clear();
        OnReset();
        maxAlive = PlayerPrefs.GetInt(UserData.Key_MaxAlive);
        maxAlive += 10;
        PlayerPrefs.SetInt(UserData.Key_MaxAlive, maxAlive);
        alive = maxAlive;
        NextLevel();
        //OnLoadLevel(levelIndex);
        UIManager.Ins.OpenUI<Win>().CloseDirectly();
        BotManager._instance.SpawnBot(alive, BotManager._instance.realBot);
        
        UIManager.Ins.OpenUI<MainMenu>();
        PlayerPrefs.Save();
    }

    public void NextLevel()
    {

        SimplePool.Despawn(maps[currentLevel-1]);
        currentLevel++;
        maps[currentLevel - 1].gameObject.SetActive(true);
        PlayerPrefs.SetInt(UserData.Key_Level, currentLevel);
        //NavMesh.RemoveAllNavMeshData();
        //NavMesh.AddNavMeshData(currentNavMesh);
        PlayerPrefs.Save(); 
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

