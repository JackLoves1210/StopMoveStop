using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIExample
{
    public class Win : UICanvas
    {
        public Text score;

        public void Start()
        {
            UserData.Ins.coin += 100;
            PlayerPrefs.SetInt("Coin", UserData.Ins.coin);
            PlayerPrefs.Save();
           // LevelManager.Ins.canvasIndicator.gameObject.SetActive(false);
        }
        public void MainMenuButton()
        {
            //UIManager.Ins.OpenUI<MainMenu>();
            LevelManager.Ins.canvasIndicator.gameObject.SetActive(false);
            LevelManager.Ins.NextLevelGame();
            
            // CloseDirectly();
        }

    }
}