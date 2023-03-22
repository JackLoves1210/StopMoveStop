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
    public int currentBot;
    public int realBot;

    public float spawnRangeX;
    public float spawnRangeZ;
    public float minRange;

    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        Oninit();
    }

    void Update()
    {
       // SpawnBot();
    }

    private void Oninit()
    {
        totalBot = 20;
        currentBot = totalBot;
        realBot = 20;
        for (int i = 0; i < totalBot; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(enemyPrefab, RandomPosition(), Quaternion.identity);
            bot.gameObject.SetActive(false);
            bots.Add(bot);
        }
  
        for (int i = 0; i < realBot; i++)
        {
            bots[i] = GetBotFormPool(); ;
            bots[i].gameObject.SetActive(true);
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
        Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, RandomPosition(), Quaternion.identity);
        bot.gameObject.SetActive(false);
        bots.Add(bot);
        return bot;
    }


    public void SpawnBot()
    {
  
        for (int i = 0; i < realBot; i++)
        {
            bots[i] = GetBotFormPool(); ;
            bots[i].gameObject.SetActive(true);
        }
    }
    
    Vector3 RandomPosition()
    {
        Vector3 posPlayer = player.transform.position;
        Debug.Log(posPlayer);
        Vector3 randomPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));
        for (int i = 0; i < bots.Count; i++)
        {
            

            float distanceToPlayer = Vector3.Distance(posPlayer, randomPosition);

            while (distanceToPlayer < 5f)
            {
                randomPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), bots[i].transform.position.y, Random.Range(-spawnRangeZ, spawnRangeZ));
                distanceToPlayer = Vector3.Distance(randomPosition, posPlayer);
            }
            return randomPosition;
            
        }
        return randomPosition;
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
