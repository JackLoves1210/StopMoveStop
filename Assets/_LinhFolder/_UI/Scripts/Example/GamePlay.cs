using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIExample
{
    public class GamePlay : UICanvas
    {
        public Text quantityCharacter;

        private void FixedUpdate()
        {
            UIManager.Ins.OpenUI<GamePlay>().quantityCharacter.text = LevelManager._instance.alive.ToString();
        }
        public void WinButton()
        {
            UIManager.Ins.OpenUI<Win>().score.text = Random.Range(100, 200).ToString();
            CloseDirectly();
        }

        public void SettingButton()
        {
            UIManager.Ins.OpenUI<Setting>();
        }
    }
}