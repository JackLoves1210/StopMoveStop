using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
namespace UIExample
{
    public class Loses : UICanvas
    {
        [SerializeField] private TextMeshProUGUI text;
        public void Start()
        {
            UserData.Ins.coin += 100;
            PlayerPrefs.SetInt("Coin", UserData.Ins.coin);
            PlayerPrefs.Save();
            // LevelManager.Ins.canvasIndicator.gameObject.SetActive(false);
            text.text = "#" + LevelManager.Ins.alive.ToString();
        }

        public override void Open()
        {
            base.Open();
        }
        public void MainMenuButton()
        {
            LevelManager.Ins.LoseGame();
            LevelManager.Ins.canvasIndicator.gameObject.SetActive(false);
        }
    }
}