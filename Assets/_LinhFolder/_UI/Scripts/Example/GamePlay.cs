using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIExample
{
    public class GamePlay : UICanvas
    {
        public void WinButton()
        {
            UIManager.Ins.OpenUI<Win>().score.text = Random.Range(100, 200).ToString();
            CloseDirectly();
        }

        public void LoseButton()
        {
            UIManager.Ins.OpenUI<Lose>().score.text = Random.Range(0, 100).ToString();
            CloseDirectly();
        }

        public void SettingButton()
        {
            UIManager.Ins.OpenUI<Setting>();
        }
    }
}