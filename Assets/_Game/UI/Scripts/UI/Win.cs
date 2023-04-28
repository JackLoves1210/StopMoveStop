using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIExample
{
    public class Win : UICanvas
    {
        public void Start()
        {
            UserData.Ins.coin += 100;
            PlayerPrefs.SetInt("Coin", UserData.Ins.coin);
            PlayerPrefs.Save();
        }
        public void MainMenuButton()
        {
            LevelManager.Ins.NextLevelGame();
            LevelManager.Ins.canvasIndicator.gameObject.SetActive(false);
        }

    }
}