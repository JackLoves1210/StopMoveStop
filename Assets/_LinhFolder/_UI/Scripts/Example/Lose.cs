using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UIExample
{
    public class Lose : UICanvas
    {
        public Text score;

        public void MainMenuButton()
        {
            UIManager.Ins.OpenUI<MainMenu>();
            CloseDirectly();
        }
    }
}