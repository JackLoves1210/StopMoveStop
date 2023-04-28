using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIExample
{
    public class GamePlay : UICanvas
    {
        public Text numverLevel;
        public Text quantityCharacter;
        public GameObject panelGuide;

        public void ButtonSetting()
        {
            UIManager.Ins.OpenUI<Setting>();
            PauseGame();
        }
        private void FixedUpdate()
        {
            UIManager.Ins.OpenUI<GamePlay>().quantityCharacter.text = LevelManager.Ins.alive.ToString();
            UIManager.Ins.OpenUI<GamePlay>().numverLevel.text = LevelManager.Ins.currentLevel.ToString();
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }
        
        public void Resume()
        {
            Time.timeScale = 1;
        }
    }
}