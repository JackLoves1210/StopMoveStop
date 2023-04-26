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

        private void FixedUpdate()
        {
            UIManager.Ins.OpenUI<GamePlay>().quantityCharacter.text = LevelManager.Ins.alive.ToString();
            UIManager.Ins.OpenUI<GamePlay>().numverLevel.text = LevelManager.Ins.currentLevel.ToString();
        }
    
    }
}