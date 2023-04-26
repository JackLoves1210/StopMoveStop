using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIExample

{
    public class MainMenu : UICanvas
    {
        [SerializeField] private Text textCoin;
        [SerializeField] private TMP_InputField inputField;

        private void Start()
        {
            //LevelManager.Ins.stateGame = LevelManager.StateGame.MainMenu;
            PlayerPrefs.GetString(UserData.Key_NamePlayer);
        }
        public override void Open()
        {
            base.Open();
            UserData.Ins.coin = PlayerPrefs.GetInt("Coin", 0);
            SetCoin(UserData.Ins.coin);
            CameraFollow.Ins.ChangeState(CameraFollow.State.MainMenu);
            if (LevelManager.Ins != null)
            {
                LevelManager.Ins.stateGame = LevelManager.StateGame.MainMenu;
            }
        }
        public void PlayButton()
        {
            LevelManager.Ins.canvasIndicator.gameObject.SetActive(true);
            LevelManager.Ins.player.isCanMove = true;
            UIManager.Ins.OpenUI<GamePlay>();
            foreach (var item in BotManager._instance.bots)
            {
                item.isCanMove = true;
            }
            CameraFollow.Ins.ChangeState(CameraFollow.State.Gameplay);
            CloseDirectly();
             LevelManager.Ins.stateGame = LevelManager.StateGame.GamePlay;
        }

        public void OpenWeaponShop()
        {
            UIManager.Ins.OpenUI<WeaponShop>();
            Close(0);
        }

        public void OpenSkinShop()
        {
            UIManager.Ins.OpenUI<UIShop>();
            Close(0);
        }

        public void SetCoin(int coin)
        {
            textCoin.text = coin.ToString();
        }

        public void SetName()
        {
            PlayerPrefs.SetString(UserData.Key_NamePlayer, inputField.text);
            PlayerPrefs.Save();
            LevelManager.Ins.player.SetName();
        }
    }
}