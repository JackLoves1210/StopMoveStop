using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UIExample;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public static LevelManager Ins;
    public List<Character> characters;
    public enum StateGame { MainMenu, Shop, GamePlay }
    public StateGame stateGame;
    public Transform canvasIndicator;
    public int alive ;
    [SerializeField] private int maxAlive;
    public int currentLevel;
    [SerializeField] private NavMeshData []navMeshDatas;
    [SerializeField] private NavMeshData currentNavMesh;
    private void Awake()
    {
        Ins = this;
        canvasIndicator = GameObject.FindGameObjectWithTag(Constant.NAME_CANVAS_INDICATOR).transform;
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey(UserData.Key_Level))
        {
            currentLevel= PlayerPrefs.GetInt(UserData.Key_Level);
            maxAlive = PlayerPrefs.GetInt(UserData.Key_Level,3);
        }
        else currentLevel = 1;
       // LoadNavMeshData(navMeshDatas[0]);
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

        //OnLoadLevel(levelIndex);
        UIManager.Ins.OpenUI<Loses>().CloseDirectly();
        BotManager._instance.SpawnBot(alive, BotManager._instance.realBot);
        UIManager.Ins.OpenUI<MainMenu>();
    }

    public void NextLevelGame()
    {
        characters.Clear();
        OnReset();
        maxAlive += 1;
        alive = maxAlive;
        PlayerPrefs.SetInt("MaxAlive", maxAlive);
        //OnLoadLevel(levelIndex);
        UIManager.Ins.OpenUI<Win>().CloseDirectly();
        BotManager._instance.SpawnBot(alive, BotManager._instance.realBot);
        NextLevel();
        UIManager.Ins.OpenUI<MainMenu>();
        PlayerPrefs.Save();
    }

    public void NextLevel()
    {
        currentLevel++;
        PlayerPrefs.SetInt(UserData.Key_Level, currentLevel);
        currentNavMesh = navMeshDatas[currentLevel - 1];
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(currentNavMesh);
      //  SaveNavMeshData(currentNavMesh);
        PlayerPrefs.Save(); // Lưu thay đổi vào bộ nhớ


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

    public string navMeshDataKey = "NavMeshData";

    public void SaveNavMeshData(NavMeshData navMeshData)
    {
        if (navMeshData != null)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, navMeshData);
            PlayerPrefs.SetString(navMeshDataKey, Convert.ToBase64String(ms.GetBuffer()));
        }
    }

    public void LoadNavMeshData(NavMeshData navMeshData)
    {
        if (PlayerPrefs.HasKey(navMeshDataKey))
        {
            string serializedData = PlayerPrefs.GetString(navMeshDataKey);
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(serializedData));
            navMeshData = (NavMeshData)bf.Deserialize(ms);
        }
    }
}

