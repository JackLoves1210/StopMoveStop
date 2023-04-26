using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotNameManager : MonoBehaviour
{
    public static BotNameManager _instance;
    public List<string> listNameBot = new List<string>();
    public List<BotName> botNames = new List<BotName>();
    public BotName botNamePrefab;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        Oninit();
    }

    private void Oninit()
    {
        for (int i = 0; i < 10; i++)
        {
            BotName bot = SimplePool.Spawn<BotName>(botNamePrefab, Vector3.zero, Quaternion.identity);
            bot.gameObject.SetActive(false);
            botNames.Add(bot);
        }
    }

    public BotName GetBotNameFormPool()
    {
        for (int i = 0; i < botNames.Count; i++)
        {
            if (!botNames[i].gameObject.activeInHierarchy)
            {
                return botNames[i];
            }
        }
        BotName botName = SimplePool.Spawn<BotName>(botNamePrefab, Vector3.zero, Quaternion.identity);
        botName.gameObject.SetActive(false);
        botNames.Add(botName);
        return botName;
    }

    public void SpawnBotName()
    {
        BotName botName = GetBotNameFormPool();
        botName.gameObject.SetActive(true);
    }
}
