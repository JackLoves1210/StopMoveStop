using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIExample

{
    public class MainMenu : UICanvas
    {
        public override void Open()
        {
            base.Open();
            CameraFollow._instance.ChangeState(CameraFollow.State.MainMenu);
        }
        public void PlayButton()
        {
            LevelManager._instance.canvasIndicator.gameObject.SetActive(true);
            LevelManager._instance.player.isCanMove = true;
            UIManager.Ins.OpenUI<GamePlay>();
            foreach (var item in BotManager._instance.bots)
            {
                item.isCanMove = true;
            }
            CameraFollow._instance.ChangeState(CameraFollow.State.Gameplay);
            CloseDirectly();
        }

        public void OpenWeaponShop()
        {
            UIManager.Ins.OpenUI<WeaponShop>();
            Close(0);
        }
    }
}