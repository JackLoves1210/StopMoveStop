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
        LevelManager._instance.characters.Add(player);
        player.OnInit();
        for (int i = 0; i < totalBot; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(enemyPrefab, Vector3.zero, Quaternion.identity);
            bot.gameObject.SetActive(false);
            bots.Add(bot);
        }

        SpawnRealBot(realBot);
        LevelManager._instance.canvasIndicator.gameObject.SetActive(false);
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
        if (CheckRamdomPosition(bot))
        {
            bot.gameObject.SetActive(true);
            LevelManager._instance.characters.Add(bot);
        }
    }

    public IEnumerator CoroutineSpawnBot()
    {
        yield return new WaitForSeconds(2f);
        SpawnBot();
    }


    public bool CheckRamdomPosition(Character character)
    {
        bool validPosition = false;
        while (!validPosition)
        {
            character.transform.position = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));
            validPosition = true;
            foreach (Character otherCharacter in LevelManager._instance.characters)
            {
                if (Vector3.Distance(character.transform.position, otherCharacter.transform.position) < minDistance)
                {
                    validPosition = false;
                    break;
                }
            }
        }
        return validPosition;
    }

}
        
