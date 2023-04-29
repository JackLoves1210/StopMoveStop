using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotManager : MonoBehaviour
{
    public static BotManager _instance;
    public Player player;
    public Bot enemyPrefab;
    public List<Bot> bots = new List<Bot>();

    public int totalBot;
   // public int currentBot;
    public int realBot;

    public float spawnRangeX;
    public float spawnRangeZ;
    public float minDistance;

    public float[] RangeX;
    public float[] RangeZ;
    public float[] minDistances;


    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        totalBot = LevelManager.Ins.maxAlive;
        LevelManager.Ins.characters.Add(player);
        player.OnInit();
        for (int i = 0; i < totalBot; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(enemyPrefab, Vector3.zero, Quaternion.identity);
            bot.gameObject.SetActive(false);
            bots.Add(bot);
        }

        SpawnRealBot(realBot);
        LevelManager.Ins.canvasIndicator.gameObject.SetActive(false);
    }


    public void SpawnBot(int quantityEnemy , int realbotSpawn)
    {
        for (int i = 0; i < quantityEnemy; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(enemyPrefab, Vector3.zero, Quaternion.identity);
            bot.gameObject.SetActive(false);
            bots.Add(bot);
            
        }
        SpawnRealBot(realbotSpawn);
    }
    public void SpawnRealBot(int realBotSpawn)
    {
        for (int i = 0; i < realBotSpawn; i++)
        {
            SpawnBot();
        }
    }

    public Bot GetBotFormPool()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            if (!bots[i].gameObject.activeInHierarchy)
            {
                return bots[i];
            }
        }
        Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, Vector3.zero, Quaternion.identity);
        bot.gameObject.SetActive(false);
        bots.Add(bot);
        return bot;
    }


    public void SpawnBot()
    {
        Bot bot = GetBotFormPool();
        bot.OnInit();
        bot.SetSize(1);
        bot.isCanMove = false;
        if (CheckRamdomPosition(bot))
        {
            bot.gameObject.SetActive(true);
            LevelManager.Ins.characters.Add(bot);
        }
    }

    public void SpawnBot(bool isCanMove)
    {
        Bot bot = GetBotFormPool();
        bot.OnInit();
        bot.isCanMove = isCanMove;

        float randSize = Random.Range(Character.MAX_SIZE, Character.MIN_SIZE);
        bot.ATT_RANGE *= randSize;
        bot.SetSize(randSize);
        if (CheckRamdomPosition(bot))
        {
            bot.gameObject.SetActive(true);
            LevelManager.Ins.characters.Add(bot);
        }
        
    }

    public IEnumerator CoroutineSpawnBot()
    {
        yield return new WaitForSeconds(2f);
        SpawnBot(true);
    }


    public bool CheckRamdomPosition(Character character)
    {
        int currentLevel = LevelManager.Ins.currentLevel-1; 
        bool validPosition = false;
        while (!validPosition)
        {
            character.transform.position = new Vector3(Random.Range(-RangeX[currentLevel], RangeX[currentLevel]), 0, Random.Range(-RangeZ[currentLevel], RangeZ[currentLevel]));
            validPosition = true;
            foreach (Character otherCharacter in LevelManager.Ins.characters)
            {
                if (Vector3.Distance(character.transform.position, otherCharacter.transform.position) < minDistances[currentLevel])
                {
                    validPosition = false;
                    break;
                }
            }
        }
        return validPosition;
    }

}
        
