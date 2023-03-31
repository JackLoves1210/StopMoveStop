using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public static LevelManager _instance;
    public List<Character> characters;

    public int alive ;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        //player.OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
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
