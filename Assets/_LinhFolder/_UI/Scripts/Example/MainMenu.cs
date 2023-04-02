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
            foreach (var item in BotManager._instance.bots)
            {
                item.isCanMove = true;
            }
            
            CloseDirectly();
        }
    }
}