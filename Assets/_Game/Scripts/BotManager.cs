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
        LevelManager._instance.characters.Add(player);
        player.OnInit();
        Oninit();
    }

    void Update()
    {
        // SpawnBot();
    }

    private void Oninit()
    {
        for (int i = 0; i < totalBot; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(enemyPrefab, Vector3.zero, Quaternion.identity);
            bot.gameObject.SetActive(false);
            bots.Add(bot);
        }

        for (int i = 0; i < realBot; i++)
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
        //bot.transform.position = RandomPosition();
        if (CheckRamdomPosition(bot))
        {
            bot.gameObject.SetActive(true);
            LevelManager._instance.characters.Add(bot);
        }


        //BotName botName = BotNameManager._instance.GetBotNameFormPool();
        //botName.transform.SetParent(Canvas);
        //botName.gameObject.SetActive(true);
        //botName.target = bot.transform;
      
       
    }

    public IEnumerator CoroutineSpawnBot()
    {
        yield return new WaitForSeconds(2f);
        SpawnBot();

    }
    //Vector3 RandomPosition()
    //{
    //    Vector3 posPlayer = player.transform.position;
    //    Vector3 randomPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));
    //    for (int i = 0; i < bots.Count; i++)
    //    {


    //        float distanceToPlayer = Vector3.Distance(posPlayer, randomPosition);

    //        while (distanceToPlayer < 5f)
    //        {
    //            randomPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), bots[i].transform.position.y, Random.Range(-spawnRangeZ, spawnRangeZ));
    //            distanceToPlayer = Vector3.Distance(randomPosition, posPlayer);
    //        }
    //        return randomPosition;

    //    }
    //    return randomPosition;
    //}

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
        
