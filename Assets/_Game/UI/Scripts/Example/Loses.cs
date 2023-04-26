using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace UIExample
{
    public class Loses : UICanvas
    {
        [SerializeField] Player player;
        public void Start()
        {
            UserData.Ins.coin += 100;
            PlayerPrefs.SetInt("Coin", UserData.Ins.coin);
            PlayerPrefs.Save();
           // LevelManager.Ins.canvasIndicator.gameObject.SetActive(false);
        }

        public override void Open()
        {
            base.Open();
            
        }
        public void MainMenuButton()
        {
            LevelManager.Ins.LoseGame();
            LevelManager.Ins.canvasIndicator.gameObject.SetActive(false);
           // this.CloseDirectly();
        }
    }
}