using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIExample

{
    public class MainMenu : UICanvas
    {
        public void PlayButton()
        {
            UIManager.Ins.OpenUI<GamePlay>();
            CloseDirectly();
        }
    }
}